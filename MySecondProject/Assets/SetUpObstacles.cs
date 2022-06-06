using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpObstacles : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> listObstacle;
    GridManager grid;
    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        listObstacle = new List<int> { 1, 6, 7, 8, 14, 15, 4, 18, 19, 25, 20, 11, 12, 13, 22, 9, 10 };
        for (int i = 0; i < listObstacle.Count; i++)
        {
            grid.UpdateObstacle(listObstacle[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
