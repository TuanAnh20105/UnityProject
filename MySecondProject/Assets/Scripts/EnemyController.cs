using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public TMP_Text healthText ;
    public int health;
    PlayerController player;
    ManagerMove managerMove;
    Vector2 a;
    bool checkMove = false;
    GridManager grid;
    ManagerEnemy managerEnemy;
    int j = 0;
    int temp, temp1;
    public bool checkStartEnemy = false;
    public int startEnemy, finishEnemy;
    public List<int> list1 = new List<int>();
    bool checkPos = false, checkDied = false;
    int updatePosEnemy;

    void Start()
    {
        a = transform.position;
        player = FindObjectOfType<PlayerController>();
        managerMove = FindObjectOfType<ManagerMove>();
        managerEnemy=FindObjectOfType<ManagerEnemy>();
        grid = FindObjectOfType<GridManager>();   
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Vector2.Distance(a, transform.position) != 0)
        {
            Debug.Log("Hello");
            startEnemy = SetPosEnemyInTile();
            player.Find();
        }
    }
    void Update()
    {

        if (checkPos == true)
        {
            for (int x = 0; x < managerEnemy.listEnemies.Count; x++)
            {
                if (Vector2.Distance(managerEnemy.listEnemies[x].transform.position, grid.listTiles[temp]) == 0)
                {
                    temp1 = x;

                }
            }
            checkPos = false;
        }
        if (this.checkStartEnemy == true)
        {
            if (Vector2.Distance(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[j]]) == 0)
            {
                j++;
            }
            if (Vector2.Distance(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[j]]) != 0)
            {
                checkMove = true;
            }
            if (checkMove == true)
            {
                managerEnemy.listEnemies[temp1].transform.position = Vector3.MoveTowards(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[j]], Time.deltaTime);
                checkMove = false;
            }
            if (Vector2.Distance(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[list1.Count - 1]]) == 0)
            {
                startEnemy = finishEnemy;
                checkStartEnemy = false;
               this.list1.Clear();
                j = 0;
            }
        }
        if(checkDied == true)
        {
            startEnemy = finishEnemy;
            checkStartEnemy = false;
            this.list1.Clear();
            j = 0;        
            checkDied = false;
        }
        UpdateTextBox();  
    }
    public int SetPosEnemyInTile()
    {
        for (int b = 0; b < grid.listTiles.Count; b++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[b]) < 0.5)
            {
                updatePosEnemy = b;
                break;
            }     
        }
        return updatePosEnemy;
    }
    public void FindCharater()
    {
        list1.Clear();
        this.startEnemy = SetPosEnemyInTile();

        for (int i = 0; i < managerEnemy.listPosEnemy.Count; i++)
        {
             finishEnemy =  player.start;

            if (managerMove.DijkstraForEnemy(managerEnemy.listPosEnemy[i], finishEnemy) == true)
            {
                temp = managerEnemy.listPosEnemy[i];
                checkStartEnemy = true;
                checkPos = true;
                break;
            }
            else
            {
                continue;
            }
        }
    }
    public void printPathForEnemy(int startEnemy, int finishEnemy, int[] backEnemy)
    {


        if (startEnemy == finishEnemy)
        {
            this.startEnemy = finishEnemy;
            list1.Add(finishEnemy);
            return;
        }
        else
        {
            printPathForEnemy(startEnemy, backEnemy[finishEnemy], backEnemy);
            list1.Add(finishEnemy);
        }

    }
    void UpdateTextBox()
    {
        healthText.text = health.ToString();
    }
    private void OnMouseDown() 
    {
        healthText.text = health.ToString();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {         
            player.checkStart = false;        
            gameObject.SetActive(false);
            player.list.Clear();
            player.SetPosCharater();
            player.i = 0;
            managerEnemy.listEnemies.Remove(gameObject);
            managerEnemy.listPosEnemy.Clear();
            managerEnemy.SetPosOfEnemy();
            checkDied = true;
            if (Damage(health) == true)
            {
                player.health += health;
  
                Debug.Log("da thang");
            }   
            else
            {
              
                player.health = 0;
                Debug.Log("da thua");
            }
        }
    }
    public bool Damage(int damage)
    {
        if (player.health > damage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
