using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMove : MonoBehaviour
{
    // Start is called before the first frame update
    GridManager grid;
    public int[,] matrix;
    int min;
    public int start;
    public int finish;
    public GameObject player;
    public Tile tile;
    public List<int> list = new List<int>();
    bool checkIncrease = true,checkMove = true;
    int i;
    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        matrix = grid.matrix;
        player.transform.position = grid.listTiles[0];
    }
    private void Update()
    {
        i = 1;
        if(checkMove == true)
        {
            //player.transform.position = Vector3.MoveTowards(player.transform.position, grid.listTiles[list[i]], Time.deltaTime);
        }
        //if(Vector2.Distance(player.transform.position, grid.listTiles[list[i]])== 0)
        //{
        //    if(checkIncrease==true)
        //    {
        //        checkMove = false;
        //        i++;
        //        checkIncrease = false;
        //    }
        //}
        //if(Vector2.Distance(player.transform.position, grid.listTiles[list[i]]) != 0)
        //{
        //    checkMove = true;
        //    checkIncrease = false;
        //}
    }
    public void OnMouseDown()
    {
        Dijkstra(start, finish);
    }
    private void OnMouseEnter()
    {
        Debug.Log("Hello");
    }
    public void Dijkstra(int start, int finish)
    {
        int[] back = new int[grid.hight * grid.hight];
        int[] weight = new int[grid.hight * grid.hight];
        int[] mask = new int[grid.hight * grid.hight];

        for (int i = 0; i < grid.hight * grid.hight; i++)
        {
            back[i] = -1;
            weight[i] = int.MaxValue;
            mask[i] = 0;
        }
        back[start] = 0;
        weight[start] = 0;
        mask[start] = 1;
        int connect = start;
        while (connect != finish)
        {
            min = int.MaxValue;
            for (int j = 0; j < grid.width * grid.hight; j++)
            {
                if (mask[j] == 0)
                {
                    if (matrix[start, j] != 0 && weight[j] >= weight[start] + matrix[start, j])
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
            }
            start = connect;
            mask[start] = 1;
        }
        printPath(0, finish, back);
    }
    public void printPath(int start, int finish, int[] back)
    {
        if (start == finish)
        {
            //Debug.Log(finish);
        }
        else
        {
            printPath(start, back[finish], back);
            Debug.Log(finish);
            list.Add(finish);
        }
    }


}
