using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public List<GameObject> lstTrans;
    public GridManager grid;
    public List<EnemyController> listEnemy = new List<EnemyController>();
    EnemyController enemy;
    private void Awake()
    {
        for (int i = 0; i < lstTrans.Count; i++)
        {
            GameObject enemies = Instantiate(Enemy, lstTrans[i].transform.position, Quaternion.identity)  ;
            enemies.transform.name = "Enemy" + i;
            enemy = enemies.GetComponent<EnemyController>();
            enemy.a = enemies.transform.position;
            listEnemy.Insert(i,enemy);            
        }
    }
    private void Start()
    {
    }

    public void SetPosOfEnemy()
    {       
        for (int i = 0; i < listEnemy.Count; i++)
        {
            for (int j  = 0; j < grid.listTiles.Count; j++)
            {
                if (Vector2.Distance(listEnemy[i].transform.position, grid.listTiles[j]) < 0.5f)
                {
                    listEnemy[i].transform.localPosition = grid.listTiles[j];
                    listEnemy[i].startEnemy = j;
                    //grid.UpdateObstacle(listEnemy[i].startEnemy);
                }
            }
        }
    }
    public void BlockPosEnemy()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            for (int j = 0; j < grid.listTiles.Count; j++)
            {
                if (Vector2.Distance(listEnemy[i].transform.position, grid.listTiles[j]) < 0.5f)
                {
                    grid.UpdateObstacle(listEnemy[i].startEnemy);
                }
            }
        }
    }

}
