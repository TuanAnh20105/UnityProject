using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMoveGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    GridManager grid;
    public int[,] matrix = new int[200, 200];
    int min;
    public PlayerCharaterGame2 player;
    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        matrix = grid.matrix;
        player = FindObjectOfType<PlayerCharaterGame2>();
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
            if (z >= (grid.hight * grid.width) * (grid.hight * grid.width))
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
            player.start = finish;
            //player.list.Add(finish);
            return;
        }
        else
        {
            printPath(start, back[finish], back);
            player.list.Add(finish);
        }

    }
}
