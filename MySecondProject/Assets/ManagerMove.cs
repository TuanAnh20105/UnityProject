using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMove : MonoBehaviour
{
    // Start is called before the first frame update
    GridManager grid;
    public int[,] matrix = new int[30,30];
    int min;
    public int start;
    public int finish;
    public GameObject player;
    public Tile tile;
    public List<int> list = new List<int>();
    bool  checkMove = false;
    bool checkStart = false;
    public List<int> list1 = new List<int>();
    ManagerEnemy managerEnemy;
    EnemyController enemy;
    public int startEnemy, finishEnemy;
    public List<EnemyController> listEnemy;
    bool checkStartEnemy = false;
    int temp,temp1;

    int i =0 ;
    void Start()
    {
        enemy = FindObjectOfType<EnemyController>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        grid = FindObjectOfType<GridManager>();
        matrix = grid.matrix;
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if(Vector2.Distance(player.transform.position,grid.listTiles[i])<1)
            {
                player.transform.position = grid.listTiles[i];
                start = i;
                break;
            }
        }     
    }   
    public void FixedUpdate()
    {
        if (checkStart == true)
        {


            for(int i = 0; i < managerEnemy.listEnemies.Count; i ++)
            {
                if(Vector2.Distance(managerEnemy.listEnemies[i].transform.position, grid.listTiles[temp]) ==0)
                {
                    temp1 = i;
                }
            }
            if (Vector2.Distance(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[i]]) == 0)
            {
                i++;
            }
            if (Vector2.Distance(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[i]]) != 0)
            {
                checkMove = true;
            }
            if (checkMove == true)
            {
                managerEnemy.listEnemies[temp1].transform.position = Vector3.MoveTowards(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[i]], Time.deltaTime);
                checkMove = false;
            }
            if (Vector2.Distance(managerEnemy.listEnemies[temp1].transform.position, grid.listTiles[list1[list1.Count - 1]]) == 0)
            {
                //start = finish;
                //checkStart = false;
                //list.Clear();
                //i = 0;
            }
        }
        if(checkStartEnemy == true)
        {
            if (Vector2.Distance(player.transform.position, grid.listTiles[list[i]]) == 0)
            {
                i++;
            }
            if (Vector2.Distance(player.transform.position, grid.listTiles[list[i]]) != 0)
            {
                checkMove = true;
            }
            if (checkMove == true)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, grid.listTiles[list[i]], Time.deltaTime);
                checkMove = false;
            }
            if (Vector2.Distance(player.transform.position, grid.listTiles[list[list.Count - 1]]) == 0)
            {
                start = finish;
                checkStart = false;
                list.Clear();
                i = 0;
            }
        }
       
    }


    public bool Dijkstra(int start, int finish)
    {
        int temp = start;
        int[] back = new int[grid.hight * grid.width];
        int[] weight = new int[grid.hight * grid.width];
        int[] mask = new int[grid.hight * grid.width];
        for (int i = 0; i < grid.hight * grid.width; i++)
        {
            back[i] = -1;
            weight[i] = int.MaxValue;
            mask[i] = 0;
        }
        back[start] = 0;
        weight[start] = 0;
        mask[start] = 1;
        
        int connect = start;
        int z = 0;
        //for (int i = 0; i < grid.hight * grid.width; i++)
        while(connect !=finish)
        {
            min = int.MaxValue;
            for (int j = 0; j < grid.width * grid.hight; j++)
            {
                if (mask[j] == 0)
                {
                    if (matrix[start, j] != 0 && weight[j] > weight[start] + matrix[start, j])
                    {
                        weight[j] = weight[start] + matrix[start, j];
                        back[j] = start;
                    }
                    if (min > weight[j] && weight[j] != 0)
                    {
                        min = weight[j];
                        connect = j;
                    }
                }
                z++;
            }
            start = connect;
            mask[start] = 1;
            if (connect == finish)
            {
                break;
            }
            if(z>=1000)
            {
                break;
            }
        }
        if (connect == finish)
        {
            printPath(temp, finish, back);
            return true;

        }
        else
            return false;

    }
    public void printPath(int start, int finish, int[] back)
    {


        if (start == finish)
        {
            this.start = finish;
            list.Add(finish);
            return;
        }
        else
        {
            printPath(start, back[finish], back);
            list.Add(finish);
        }

    }


    public bool DijkstraForEnemy(int start, int finish)
    {
        int temp = start;
        int[] back = new int[grid.hight * grid.width];
        int[] weight = new int[grid.hight * grid.width];
        int[] mask = new int[grid.hight * grid.width];
        for (int i = 0; i < grid.hight * grid.width; i++)
        {
            back[i] = -1;
            weight[i] = int.MaxValue;
            mask[i] = 0;
        }
        back[start] = 0;
        weight[start] = 0;
        mask[start] = 1;

        int connect = start;
        int z = 0;
        //for (int i = 0; i < grid.hight * grid.width; i++)
        while (connect != finish)
        {
            min = int.MaxValue;
            for (int j = 0; j < grid.width * grid.hight; j++)
            {
                if (mask[j] == 0)
                {
                    if (matrix[start, j] != 0 && weight[j] > weight[start] + matrix[start, j])
                    {
                        weight[j] = weight[start] + matrix[start, j];
                        back[j] = start;
                    }
                    if (min > weight[j] && weight[j] != 0)
                    {
                        min = weight[j];
                        connect = j;
                    }
                }
                z++;
            }
            start = connect;
            mask[start] = 1;
            if (connect == finish)
            {
                break;
            }
            if (z >= 1000)
            {
                break;
            }
        }
        if (connect == finish)
        {
            printPathForEnemy(temp, finish, back);
            return true;

        }
        else
            return false;

    }
    public void printPathForEnemy(int start, int finish, int[] back)
    {


        if (start == finish)
        {
            this.start = finish;
            list1.Add(finish);
            return;
        }
        else
        {
            printPathForEnemy(start, back[finish], back);
            list1.Add(finish);
        }

    }


    public void Find()
    {
        for(int i = 0; i < managerEnemy.listPosEnemy.Count; i ++)
        {
            finish = managerEnemy.listPosEnemy[i];
            
            if(Dijkstra(start, finish) == true)
            {
                checkStart = true;
                managerEnemy.listPosEnemy.RemoveAt(i);
                break;
            }
            else
            {
                continue;
            }
        }
    }
    public void FindCharater()
    {
       
        
        for (int i = 0; i < managerEnemy.listPosEnemy.Count; i++)
        {
            finishEnemy = start;
          
            if (DijkstraForEnemy(managerEnemy.listPosEnemy[i], finishEnemy) == true)
            {
                startEnemy = managerEnemy.listPosEnemy[i];
                temp = i;
                checkStartEnemy = true;
                break;
            }
            else
            {
                continue;
            }
        }
    }


}
