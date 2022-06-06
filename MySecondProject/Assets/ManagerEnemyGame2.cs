using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemyGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public List<GameObject> lstTrans;
    public GridManager grid;
    public List<EnemyGame2> listEnemy = new List<EnemyGame2>();
    EnemyGame2 enemy;
    private void Awake()
    {
        for (int i = 0; i < lstTrans.Count; i++)
        {
            GameObject enemies = Instantiate(Enemy, lstTrans[i].transform.position, Quaternion.identity);
            enemies.transform.name = "Enemy" + i;
            enemy = enemies.GetComponent<EnemyGame2>();
            enemy.a = enemies.transform.position;
            listEnemy.Insert(i, enemy);
        }
    }
    private void Start()
    {

    }

    public void SetPosOfEnemy()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            for (int j = 0; j < grid.listTiles.Count; j++)
            {
                if (Vector2.Distance(listEnemy[i].transform.position, grid.listTiles[j]) < 1)
                {
                    listEnemy[i].transform.localPosition = grid.listTiles[j];
                    listEnemy[i].startEnemy = j;
                }
            }
        }
    }

}
