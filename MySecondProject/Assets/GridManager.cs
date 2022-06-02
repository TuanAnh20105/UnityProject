using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int width, hight;
    [SerializeField] private Tile tilePref;
    [SerializeField] private Transform cam;
    private Dictionary<Vector2, Tile> tiles;
    public List<Vector2> listTiles = new List<Vector2>();
    public int[,] matrix = new int[30, 30];
    [TextArea(10, 20)]
    public string weight;
    public ManagerEnemy managerEnemy;
    public List<int> listObstacle;
    public static GridManager Instance;

    int id = 0;
    private void Awake()
    {
        Instance = this;
        listObstacle = new List<int> { 1,6, 7, 8, 14, 15, 4, 18, 19, 25, 20, 11, 12, 13, 22 };
        GenerateGrid();
        Weight();
        for (int i = 0; i < listObstacle.Count; i++)
        {
            UpdateObstacle(listObstacle[i]);
        }
    }
    private void Start()
    {

        managerEnemy = FindObjectOfType<ManagerEnemy>();

        printMatrix();
    }

    public void Update()
    {

    }
    public void GenerateGrid()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < hight; y++)
            {
                var spawnTile = Instantiate(tilePref, new Vector3(x, y), Quaternion.identity);
                spawnTile.name = $"Tile {x} {y}";
                spawnTile.id = id;
                id++;
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnTile.Init(isOffset);
                listTiles.Add(spawnTile.transform.position);
            }
        }
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)hight / 2 - 0.5f, -10);
    }
    public void Weight()
    {
        for (int x = 0; x < id; x++)
        {
            for (int y = 0; y < id; y++)
            {
                if (Vector2.Distance(listTiles[x], listTiles[y]) >= 1 && Vector2.Distance(listTiles[x], listTiles[y])<=1.5)
                {
                    matrix[x, y] = 1;
                }
                if (Vector2.Distance(listTiles[x], listTiles[y]) > 1)
                {
                    matrix[x, y] = 0;
                }
            }
        }
    }
    
    public void UpdateObstacle(int pos)
    {
        for (int x = 0; x < id; x++)
        {
            for (int y = 0; y < id; y++)
            {
                if (y == pos + 1 || y == pos - 1 || y == pos - hight || y == pos + hight)
                {
                    matrix[x, pos] = 0;
                }           
            }
            if (x == pos)
            {
                if(pos < hight)
                {
                    matrix[x, pos - 1] = 0;
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos + hight] = 0;
                }
                else if (pos >=width * (hight - 1))
                {
                    matrix[x, pos - 1] = 0;
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos - hight] = 0;
                 
                }
                else
                {
                    matrix[x, pos - 1] = 0;
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos - hight] = 0;
                    matrix[x, pos + hight] = 0;
                }

            }
        }
    }
    public void UpdateAfterDragObstacle(int pos)
    {
        for (int x = 0; x < id; x++)
        {
            for (int y = 0; y < id; y++)
            {
                if (y == pos + 1 || y == pos - 1 || y == pos - hight || y == pos + hight)
                {
                    matrix[ y,pos] = 1;
                }
            }
            if (x == pos)
            {
                if(pos <hight)
                {
                    matrix[x, pos - 1] = 1;
                    matrix[x, pos + 1] = 1;                   
                    matrix[x, pos + hight] = 1;
                }
                else if(pos>hight)
                {
                    matrix[x, pos - 1] = 1;
                    matrix[x, pos + 1] = 1;
                    matrix[x, pos - hight] = 1;
                }
                else
                {
                     matrix[x, pos - 1] = 1;
                     matrix[x, pos + 1] = 1;
                     matrix[x, pos - hight] = 1;
                     matrix[x, pos + hight] = 1;
                }
            }
        }
    }
    public void printMatrix()
    {
        for (int i = 0; i < id; i++)
        {
            for (int j = 0; j < id; j++)
            {
                weight += matrix[i, j] + " ";
            }
            weight += "\n";
        }
    }

    // Update is called once per frame

}
