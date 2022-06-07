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
    GridManager grid;
    ManagerEnemy managerEnemy;
    public bool checkStartEnemy = false;
    public int startEnemy, finishEnemy;
    public List<int> list1 = new List<int>();
    bool  checkDied = false;
    int updatePosEnemy;
    public bool checkFind = false;
    public LineRenderer line;

    void Start()
    {
        a = transform.position;
        player = FindObjectOfType<PlayerController>();
        managerMove = FindObjectOfType<ManagerMove>();
        managerEnemy=FindObjectOfType<ManagerEnemy>();
        grid = GridManager.Instance;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if (checkFind == true)
        {
            for(int i = 0; i < managerEnemy.listEnemy.Count; i ++)
            {
                if (Vector2.Distance(managerEnemy.listEnemy[i].transform.position, transform.position) == 0f)
                {
                    managerEnemy.listEnemy[i].startEnemy = SetPosEnemyInTile();
                    player.Find();
                }
            }
        }
    }
    void Update()
    {
        if (checkStartEnemy == true && list1.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, line.GetPosition(0), Time.deltaTime);
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
            managerEnemy.listEnemy[j].startEnemy = managerEnemy.listEnemy[j].SetPosEnemyInTile();//update pos after move 
            managerEnemy.listEnemy[j].list1.Clear(); // remove all list               
            managerEnemy.listEnemy[j].finishEnemy = player.start; // update finish
            if (managerMove.DijkstraForEnemy(managerEnemy.listEnemy[j].startEnemy, finishEnemy) == true) // check path of enemy  
            {
                managerEnemy.listEnemy[j].line.positionCount = managerEnemy.listEnemy[j].list1.Count;                   
                for (int a = 0; a < managerEnemy.listEnemy[j].list1.Count; a++)
                {
                    managerEnemy.listEnemy[j].line.SetPosition(a, grid.listTiles[managerEnemy.listEnemy[j].list1[a]]);
                }
                if(managerEnemy.listEnemy[j].list1.Count!= 0)
                {
                    managerEnemy.listEnemy[j].checkStartEnemy = true;
                    managerEnemy.listEnemy[j].checkFind = true;                   
                }
            }
            
        }
    }
    public void printPathForEnemy(int startEnemy, int finishEnemy, int[] backEnemy)
    {
        if (startEnemy == finishEnemy)
        {
            this.startEnemy = finishEnemy;
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
            player.checkFind = false;
            player.list.Clear();
            player.SetPosCharater();
            if (Damage(health) == true)
            {
                player.health += health;
                player.healthText.text = player.health.ToString();
  
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
