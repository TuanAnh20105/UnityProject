using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public Transform[] lstTrans;
    public GridManager grid;
    public List<GameObject> listEnemies = new List<GameObject>();
    public List<int> listPosEnemy = new List<int>();
    private void Awake()
    {
        for (int i = 0; i < lstTrans.Length; i++)
        {
            GameObject enemies = Instantiate(Enemy, lstTrans[i].transform.position, Quaternion.identity)  ;
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
                if (Vector2.Distance(lstTrans[i].transform.position, grid.listTiles[j]) < 1)
                {
                    listEnemies[i].transform.position = grid.listTiles[j];
                    listPosEnemy.Add(j);
                }
            }
        }
    }
    public void SetPosEnemy(int i)
    {
        listPosEnemy.Remove(i);
        for (int j = 0; j < grid.listTiles.Count; j++)
        {
            if (Vector2.Distance(lstTrans[i].transform.position, grid.listTiles[j]) < 1)
            {
                listEnemies[i].transform.position = grid.listTiles[j];
                listPosEnemy.Add(j);
            }
        }
    }
    // Update is called once per frame

}
