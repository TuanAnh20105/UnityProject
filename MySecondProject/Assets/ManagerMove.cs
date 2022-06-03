using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMove : MonoBehaviour
{
    // Start is called before the first frame update
    GridManager grid;
    public int[,] matrix = new int[30,30];
    int min;
    public PlayerController player;
    public Tile tile;
    ManagerEnemy managerEnemy;
    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        matrix = grid.matrix;
        player = FindObjectOfType<PlayerController>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
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
            if(z>= (grid.hight * grid.width) * (grid.hight * grid.width))
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
    public bool DijkstraForEnemy(int startEnemy, int finishEnemy)
    {
        int temp = startEnemy;
        int[] backEnemy = new int[grid.hight * grid.width];
        int[] weightEnemy = new int[grid.hight * grid.width];
        int[] maskEnemy = new int[grid.hight * grid.width];
        for (int i = 0; i < grid.hight * grid.width; i++)
        {
            backEnemy[i] = -1;
            weightEnemy[i] = int.MaxValue;
            maskEnemy[i] = 0;
        }
        backEnemy[startEnemy] = 0;
        weightEnemy[startEnemy] = 0;
        maskEnemy[startEnemy] = 1;

        int connect = startEnemy;
        int z = 0;
        while (connect != finishEnemy)
        {
            min = int.MaxValue;
            for (int j = 0; j < grid.width * grid.hight; j++)
            {
                if (maskEnemy[j] == 0)
                {
                    if (matrix[startEnemy, j] != 0 && weightEnemy[j] > weightEnemy[startEnemy] + matrix[startEnemy, j])
                    {
                        weightEnemy[j] = weightEnemy[startEnemy] + matrix[startEnemy, j];
                        backEnemy[j] = startEnemy;
                    }
                    if (min > weightEnemy[j] && weightEnemy[j] != 0)
                    {
                        min = weightEnemy[j];
                        connect = j;
                    }
                }
                z++;
            }
            startEnemy = connect;
            maskEnemy[startEnemy] = 1;
            if (connect == finishEnemy)
            {
                break;
            }
            if (z >= (grid.hight * grid.width)*(grid.hight * grid.width))
            {
                break;
            }
        }
        if (connect == finishEnemy)
        {
            //for(int i = 0; i < player.save.Count; i++)
            {
                managerEnemy.listEnemy[player.save].printPathForEnemy(temp, finishEnemy, backEnemy);
            }
            return true;
        }
        else
            return false;

    }
}
