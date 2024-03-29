﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int width;
    public int hight;
    [SerializeField] private Tile tilePref;
    public List<Vector2> listTiles = new List<Vector2>();
    public int[,] matrix = new int[10, 10];
    [TextArea(10, 20)]
    public string weight;
    public static GridManager Instance;
    public List<Tile> TileList = new List<Tile>();
    [SerializeField] private Transform cam;
    int id;
    public static GridManager instance;
    private void Awake()
    {
        instance = this;
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
                matrix[x, y] = 0;
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnTile.Init(isOffset);
                listTiles.Add(spawnTile.transform.position);
                TileList.Add(spawnTile);
                id++;
            }
        }
    }
    public void SetMatrixZero()
    {
      
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < hight; y++)
                {
                    matrix[x, y] = 0;
                }
            }
      
    }
    public void Classic()
    {
        width = 4;
        hight = 4;
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)hight / 2 -1.5f, -10);
        Camera a =  cam.GetComponent<Camera>();
        a.orthographicSize = 4.5f ;
    }
    public void Infinity()
    {
        width = 5;
        hight = 8;
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)hight / 2 - 0.9f, -10);
        Camera a = cam.GetComponent<Camera>();
        a.orthographicSize = 5.5f;
    }
}
