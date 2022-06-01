using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public List<GameObject> lstTrans;
    public GridManager grid;
    public List<GameObject> listEnemies = new List<GameObject>();
    public List<int> listPosEnemy = new List<int>();
    private void Awake()
    {
        for (int i = 0; i < lstTrans.Count; i++)
        {
            GameObject enemies = Instantiate(Enemy, lstTrans[i].transform.position, Quaternion.identity)  ;
            enemies.transform.name = "Enemy" + i;
            listEnemies.Add(enemies);
        }
    }
    void Start()
    {
            
    }
    public void SetPosOfEnemy()
    {       
        for (int i = 0; i < listEnemies.Count; i++)
        {
            for (int j = 0; j < grid.listTiles.Count; j++)
            {
                if (Vector2.Distance(listEnemies[i].transform.position, grid.listTiles[j]) < 1)
                {
                    listEnemies[i].transform.localPosition = grid.listTiles[j];
                    listPosEnemy.Add(j);
                }
            }
        }
    }

}
