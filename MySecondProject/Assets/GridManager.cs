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
    public int[,] matrix = new int[200, 200];
    [TextArea(3, 5)]
    public string weight;
    int id = 0;
    void Start()
    {
        GenerateGrid();
        Weight();
        printMatrix();         
    }  
    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
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
            int i = x;
            for (int y = 0; y < id; y++)
            {
                if (Vector2.Distance(listTiles[i], listTiles[y]) == 1)
                {
                    matrix[x, y] = 1;
                }
                else
                {
                    matrix[x, y] = 0;
                }
            }
        }
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        {
            if (tiles.TryGetValue(pos, out var tile))
            {
                return tile;
            }
            return null;
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
