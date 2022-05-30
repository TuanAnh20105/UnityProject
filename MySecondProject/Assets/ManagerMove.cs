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
    public bool  checkStart = false, checkPath = false;
    public List<int> list1 = new List<int>();

    
    int i =0 ,a =0;
    void Start()
    {
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
    private void Update()
    {
        if (list.Count != 0)
        {
            checkStart = true;
        }
        else
        {
            checkStart = false;
        }

        if (checkStart == true)
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
                checkPath = false;
                list.Clear();
                i = 0;
            }
        }
    }

    public void Dijkstra(int start, int finish)
    {
        Debug.Log("this start" + start);
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
        //while (connect != finish)
        for(int i = 0; i < grid.hight * grid.width; i ++)
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

            
        }
       
        printPath(temp, finish, back);

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


}
