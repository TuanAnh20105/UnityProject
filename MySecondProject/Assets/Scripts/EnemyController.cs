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
    [HideInInspector] public Vector2 a;
    bool checkMove = false;
    GridManager grid;
    ManagerEnemy managerEnemy;

    int temp, temp1;
    public bool checkStartEnemy = false;
    public int startEnemy, finishEnemy;
    public List<int> list1 = new List<int>();
    bool checkPos = false, checkDied = false;
    int updatePosEnemy;
    public int id;
    public bool checkFind = false;
    int temp2;
    int j = 1;
    Obstacle obstacle;
    bool checkout = false;

    void Start()
    {
        a = transform.position;
        player = FindObjectOfType<PlayerController>();
        managerMove = FindObjectOfType<ManagerMove>();
        managerEnemy=FindObjectOfType<ManagerEnemy>();
        grid = GridManager.Instance;
        temp2 = startEnemy;
        obstacle = FindObjectOfType<Obstacle>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (checkFind == true)
        {
            for(int i = 0; i < managerEnemy.listEnemy.Count; i ++)
            {
                if (Vector2.Distance(managerEnemy.listEnemy[i].transform.position, transform.position) >= 1)
                {
                    startEnemy = SetPosEnemyInTile();
                    player.Find();
                    //j = 0;
                }
            }
        }
    }
    void Update()
    {

        if (checkPos == true)
        {
            for (int x = 0; x < managerEnemy.listEnemy.Count; x++)
            {
                if (Vector2.Distance(managerEnemy.listEnemy[x].transform.position, grid.listTiles[temp]) == 0)
                {
                    temp1 = x;
                }
            }
            checkPos = false;
        }
        //****************************************************************

        if (checkStartEnemy == true && list1.Count != 0)
        {
            
            if(Vector2.Distance(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[1]]) !=0)
            {
                checkMove = true;
            }
            if (checkMove == true)
            {
                managerEnemy.listEnemy[temp1].transform.position = Vector3.MoveTowards(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[j]], Time.deltaTime);
                checkMove = false;
            }

            //if (Vector2.Distance(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[j]]) == 0)
            //{
            //    j = 0;
            //}
            //if (list1.Count == 1)
            //{
            //    j = 0;
            //}
            //if (Vector2.Distance(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[j]]) == 0)
            //{
            //    j++;
            //}
            //if (Vector2.Distance(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[j]]) != 0)
            //{
            //    checkMove = true;
            //}
            //if (checkMove == true)
            //{
            //    managerEnemy.listEnemy[temp1].transform.position = Vector3.MoveTowards(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[j]], Time.deltaTime);
            //    checkMove = false;
            //}
            if (Vector2.Distance(managerEnemy.listEnemy[temp1].transform.position, grid.listTiles[list1[list1.Count - 1]]) == 0)
            {
                startEnemy = finishEnemy;
                checkStartEnemy = false;
                this.list1.Clear();
                j = 1;
            }
        }
        if (checkDied == true)
        {
            startEnemy = finishEnemy;
            checkStartEnemy = false;
            this.list1.Clear();
            for (int i = 0; i < managerEnemy.listEnemy.Count; i++)
            {
                managerEnemy.listEnemy[i].finishEnemy = 0;
                managerEnemy.listEnemy[i].checkStartEnemy = false;
            }
            //j = 0;
            checkFind = false;
            checkDied = false;

        }
        UpdateTextBox();
    }
    public int SetPosEnemyInTile()
    {
        for (int b = 0; b < grid.listTiles.Count; b++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[b]) ==0)
            {
                updatePosEnemy = b;
                break;
            }     
        }
        return updatePosEnemy;
    }
    public void FindCharater()
    {
        for (int j = 0; j < managerEnemy.listEnemy.Count; j++)
        {
            managerEnemy.listEnemy[j].startEnemy = managerEnemy.listEnemy[j].SetPosEnemyInTile();
            if (managerEnemy.listEnemy[j].startEnemy == player.finish)
            {
                list1.Clear();
                managerEnemy.listEnemy[j].startEnemy = SetPosEnemyInTile();
               for (int i = 0; i < managerEnemy.listEnemy.Count; i++)
               {
                    managerEnemy.listEnemy[j].finishEnemy =  player.start;

                   if (managerMove.DijkstraForEnemy(managerEnemy.listEnemy[i].startEnemy, finishEnemy) == true)
                   {
                       temp = managerEnemy.listEnemy[i].startEnemy;
                        managerEnemy.listEnemy[j].checkStartEnemy = true;
                       checkPos = true;
                        checkout = true;
                       break;
                   }
                   else
                   {
                       continue;
                   }
               }
               if(checkout == true)
                {
                    checkout = false;
                    break;
                }
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
            Destroy(gameObject);
            managerEnemy.listEnemy.Remove(this);
            player.list.Clear();
            player.SetPosCharater();
            player.i = 1;
            player.checkFind = false;
            managerEnemy.SetPosOfEnemy();
            startEnemy = temp2;
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
