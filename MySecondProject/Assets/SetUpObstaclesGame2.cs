using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpObstaclesGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> listObstacle;
    public List<int> listObstacleUnWalkable;
    GridManager grid;
    ManagerEnemy managerEnemy;
    void Start()
    {
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        grid = FindObjectOfType<GridManager>();
        listObstacle = new List<int> {0,  1, 2, 3, 4, 9,18, 27,36,8,16,24,32,25,11, 33, 34, 35 };
        listObstacleUnWalkable = new List<int> { 10, 12, 17, 19, 20, 28, 26};
        SetColor();
        ObstableUnwalkable();
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
    public void ObstableUnwalkable()
    {
        for(int i = 0; i < listObstacleUnWalkable.Count; i++)
        {
            grid.UpdateObstacle(listObstacleUnWalkable[i]);
        }
    }
    public void SetColorInTile(int i)
    {

            grid.TileList[listObstacle[i]]._renderer.color = Color.red;
        
    }
    public void SetWeight()
    {

    }
    public void UpdateDirObstacle()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
