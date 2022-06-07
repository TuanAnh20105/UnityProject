using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text healthText;
    [HideInInspector] public int health = 20;
    [HideInInspector] public int a;
    public int start, finish;
    public bool checkStart = false;
    public int i = 1;
    GridManager grid;
    public List<int> list = new List<int>();
    ManagerEnemy managerEnemy;
    [HideInInspector] public Vector3 vt;
    ManagerMove managerMove;
    int updatePosPlayer;
    public bool checkFind = false;
     public List<int> save = new List<int>();
    public LineRenderer line;
    public bool checkStartGame2 = false;
    ManagerGame1 managerGame1;
    int count = 0;
    bool checkPath = false;

    void Start()
    {
        vt = gameObject.transform.position;
        a = health;
        healthText.text = health.ToString();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerMove = FindObjectOfType<ManagerMove>();
        grid = FindObjectOfType<GridManager>();
        managerGame1 = FindObjectOfType<ManagerGame1>();
        SetPosCharater();
        i = 1;
    }
    public void SetPosCharater()
    {
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[i]) < 0.3f)
            {
                transform.position = grid.listTiles[i];
                start = i;         
                break;
            }
        }
     
    }
    public int SetPosPlayerInTile()
    {
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[i]) ==0f)
            {
                updatePosPlayer = i;
                vt = grid.listTiles[i];
                break;               
            }
        }
        return updatePosPlayer;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if (checkFind == true)
        {
            if (Vector2.Distance(vt, transform.position) != 0)
            {
                start = SetPosPlayerInTile();
                for(int y = 0; y < save.Count; y++)
                {
                    managerEnemy.listEnemy[save[y]].FindCharater();               
                }
            }
        }

    }
    void Update()
    {
        if (checkStart == true)
        {
            start = SetPosPlayerInTile();
            if(line.positionCount!=0)
            {
                transform.position = Vector2.MoveTowards(transform.position, line.GetPosition(0), Time.deltaTime);               
            }
        }
        if (checkStartGame2 == true)
        {
            grid.UpdateObstacle(start);
            start = SetPosPlayerInTile();
            if (line.positionCount != 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, line.GetPosition(0), Time.deltaTime);
            }
        }
    }
    public void FindGame2()
    {
        for(int i = 0; i < managerEnemy.listEnemy.Count; i++)
        {
            finish = managerEnemy.listEnemy[i].startEnemy;
            list.Clear();
            if (managerMove.Dijkstra(start, finish) == true && managerEnemy.listEnemy[i].health < health)
            {
                checkPath = true;
                line.positionCount = list.Count;
                for (int j = 0; j < list.Count; j++)
                {
                    line.SetPosition(j, grid.listTiles[list[j]]);
                }
                checkStartGame2 = true;
                if (save.Count == 0)
                {
                    save.Add(finish);
                }
                if (save.Count != 0)
                {
                    if (finish != save[save.Count - 1])
                    {
                        save.Add(finish);
                    }
                }
                //managerGame1.CheckFind = false;
                break;
            }
            if(checkPath == false)
            {

            }
            
        }
    }
    public void Find()
    {
        list.Clear();
        save.Clear();
        for (int i = 0; i < managerEnemy.listEnemy.Count; i++)
        {
            start =  SetPosPlayerInTile();
            finish = managerEnemy.listEnemy[i].startEnemy;
            if (managerMove.Dijkstra(start, finish) == true)
            {
                line.positionCount = list.Count;
                for(int j = 0; j < list.Count; j++)
                {
                    line.SetPosition(j, grid.listTiles[list[j]]);
                }
                if(save.Count ==0)
                {
                    save.Add(i);
                }
                if(save.Count!=0)
                {
                    for(int z = 0; z < save.Count; z++)
                    {
                        if(i!= save[z])
                        {
                            save.Add(i);
                        }

                    }
                }
                checkStart = true;
                //break;
            }
        }
        if(list.Count != 0)
        {
            checkFind = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            //checkStartGame2 = false;
        }
    }

}
