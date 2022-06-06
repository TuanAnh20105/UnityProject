using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpObstaclesGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> listObstacle;
    GridManager grid;
    ManagerEnemy managerEnemy;
    void Start()
    {
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        grid = FindObjectOfType<GridManager>();
        listObstacle = new List<int> {0,  1, 2, 3, 4, 9,18, 27,36,8,16,24,32,25,11};
        SetColor();
        //grid.WeightZero();
        managerEnemy.BlockPosEnemy();
        grid.printMatrix();
    }
    public void SetColor()
    {
        for (int i = 0; i < listObstacle.Count; i++)
        {
            grid.UpdateAfterDragObstacle(listObstacle[i]);
            grid.TileList[listObstacle[i]]._renderer.color = Color.yellow;
        }
    }
    public void SetColorInTile(int i)
    {

            grid.TileList[listObstacle[i]]._renderer.color = Color.red;
        
    }
    public void SetWeight()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
