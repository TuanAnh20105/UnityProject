using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int width, hight;
    [SerializeField] private Tile tilePref;
    [SerializeField] private Transform cam;
    public List<Vector2> listTiles = new List<Vector2>();
    public int[,] matrix = new int[200, 200];
    [TextArea(10, 20)]
    public string weight;
    public static GridManager Instance;
    public List<Tile> TileList = new List<Tile>();
    public List<int> listHead = new List<int>();
    public List<int> listLast = new List<int>();


    int id = 0;
    private void Awake()
    {
        Instance = this;
        GenerateGrid();
    }
    private void Start()
    {
    }
    public void Update()
    {
        //Debug.Log(Vector2.Distance(listTiles[0], listTiles[10]).ToString());
    }
    public void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < hight; y++)
            {
                Tile spawnTile = Instantiate(tilePref, new Vector3(x, y), Quaternion.identity);
                spawnTile.name = $"Tile {x} {y}";
                spawnTile.id = id;
              
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnTile.Init(isOffset);
                listTiles.Add(spawnTile.transform.position);
                TileList.Add(spawnTile);
                if( y == 0)
                {
                    listHead.Add(id);
                }
                if(y == hight-1)
                {
                    listLast.Add(id);
                }
                id++;
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
                if (Vector2.Distance(listTiles[x], listTiles[y]) >= 1 && Vector2.Distance(listTiles[x], listTiles[y])<=1.5f)
                {
                    matrix[x, y] = 1;
                }
                if (Vector2.Distance(listTiles[x], listTiles[y]) > 1.5f)
                {
                    matrix[x, y] = 0;
                }
            }
        }
    }
    public void WeightZero()
    {
        for (int x = 0; x < id; x++)
        {
            for (int y = 0; y < id; y++)
            {               
                matrix[x, y] = 0;
            }
        }
    }
    public void UpdateObstacle(int pos)
    {
        for (int x = 0; x < id; x++)
        {
            for (int y = 0; y < id; y++)
            {
                if (y == pos + 1 || y == pos - 1 || y == pos - hight || y == pos + hight 
                    || y == pos +hight +1 || y == pos + hight -1 || y == pos-hight -1 || y == pos -hight +1)
                {
                    matrix[x, pos] = 0;
                }           
            }
            if (x == pos)
            {
                if(pos == 0 )
                {
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos + hight] = 0;
                    matrix[x, pos + hight +1] = 0;
                    matrix[x, pos + hight - 1] = 0;

                }
                else if(pos <= hight)
                {
                    matrix[x, pos - 1] = 0;
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos + hight] = 0;
                    matrix[x, pos + hight+1] = 0;
                    matrix[x, pos + hight-1] = 0;
                }
                else if (pos >=width * (hight - 1))
                {
                    matrix[x, pos - 1] = 0;
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos - hight] = 0;
                    matrix[x, pos - hight + 1] = 0;
                    matrix[x, pos - hight - 1] = 0;

                }
                else
                {
                    matrix[x, pos - 1] = 0;
                    matrix[x, pos + 1] = 0;
                    matrix[x, pos - hight] = 0;
                    matrix[x, pos + hight] = 0;
                    matrix[x, pos + hight + 1] = 0;
                    matrix[x, pos + hight - 1] = 0;
                    matrix[x, pos - hight + 1] = 0;
                    matrix[x, pos - hight - 1] = 0;
                }
            }
        }
    }
    public void UpdateAfterDragObstacle(int pos) //9
    {
        for (int x = 0; x < id; x++)
        {
            for (int y = 0; y < id; y++)
            {
                if (y == pos + 1 || y == pos - 1 || y == pos - hight || y == pos + hight 
                    || y == pos +hight +1 || y == pos + hight -1 || y == pos-hight -1 || y == pos -hight +1)//10,
                {
                    matrix[ y,pos] = 1;
                }
            }              
            if (x == pos)
            {
                if (pos == 0)
                {
                    matrix[x, pos + 1] = 1;
                    matrix[x, pos + hight] = 1;
                    matrix[x, pos + hight + 1] = 1;                   
                }
                else if (pos <=hight)
                {
                    matrix[x, pos - 1] = 1;
                    matrix[x, pos + 1] = 1;
                    matrix[x, pos + hight] = 1;
                    matrix[x, pos + hight + 1] = 1;
                    matrix[x, pos + hight - 1] = 1;
                }
                else if(pos >= width*(hight-1))
                {
                    matrix[x, pos - 1] = 1;
                    matrix[x, pos + 1] = 1;
                    matrix[x, pos - hight] = 1;
                    matrix[x, pos - hight + 1] = 1;
                    matrix[x, pos - hight - 1] = 1;
                }
                else
                {
                    matrix[x, pos - 1] = 1;
                    matrix[x, pos + 1] = 1;
                    matrix[x, pos - hight] = 1;
                    matrix[x, pos + hight] = 1;
                    matrix[x, pos + hight + 1] = 1;
                    matrix[x, pos + hight - 1] = 1;
                    matrix[x, pos - hight + 1] = 1;
                    matrix[x, pos - hight - 1] = 1;
                }                                    
            }
            if(x==pos)
            {
                for(int i = 0; i < listHead.Count;i++)
                {
                    if(pos == listHead[i])
                    {
                        if(pos > hight && pos < width * (hight - 1))
                        {
                            matrix[x, pos + 1] = 1;
                            matrix[x, pos - 1] = 0;
                            matrix[x, pos - hight] = 1;
                            matrix[x, pos + hight] = 1;
                            matrix[x, pos + hight + 1] = 1;
                            matrix[x, pos + hight - 1] = 0;
                            matrix[x, pos - hight + 1] = 1;
                            matrix[x, pos - hight - 1] = 0;
                        }
                        if (pos == 0)
                        {
                            matrix[x, pos + 1] = 1;
                            matrix[x, pos + hight] = 1;
                            matrix[x, pos + hight + 1] = 1;
                        }
                        if(pos == hight)
                        {
                            matrix[x, pos + 1] = 1;
                            matrix[x, pos - 1] = 0;
                            matrix[x, pos - hight] = 1;
                            matrix[x, pos + hight] = 1;
                            matrix[x, pos + hight + 1] = 1;
                            matrix[x, pos + hight - 1] = 0;
                            matrix[x, pos - hight + 1] = 1;
                        }
                    }
                    if(pos == listHead[i])
                    {
   
                    }
                }
                for (int j = 0; j < listLast.Count; j++)
                {
                    if (pos == listLast[j] )
                    {
                        if( pos > hight - 1 && pos < width * hight - 1)
                        {
                            matrix[x, pos - 1] = 1;
                            matrix[x, pos + 1] = 0;
                            matrix[x, pos - hight] = 1;
                            matrix[x, pos + hight] = 1;
                            matrix[x, pos + hight - 1] = 1;
                            matrix[x, pos - hight - 1] = 1;
                            matrix[x, pos + hight + 1] = 0;
                            matrix[x, pos - hight + 1] = 0;
                        }
                    }
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
