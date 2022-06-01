using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text healthText;
    public int health = 20;
    public int a;
    bool checkHealth = true;
    public int count = 0;
    public int start, finish;
    public bool checkStart = false;
     public int i = 0;
    GridManager grid;
    public List<int> list = new List<int>();
    ManagerEnemy managerEnemy;
    public Vector3 vt;
    ManagerMove managerMove;
    bool checkMove = false;
    EnemyController enemy;
    int updatePosPlayer;

    void Start()
    {
        vt = gameObject.transform.position;
        a = health;
        healthText.text = health.ToString();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerMove = FindObjectOfType<ManagerMove>();
        grid = FindObjectOfType<GridManager>();
        SetPosCharater();
        enemy = FindObjectOfType<EnemyController>();


    }
    public void SetPosCharater()
    {
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[i]) < 1)
            {
                transform.position = grid.listTiles[i];
                start = i;
                break;
            }
        }
    }
    public int SetPosPlayerInTile()
    {
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[i]) < 0.5)
            {
                updatePosPlayer = i;
                break;
            }
        }
        return updatePosPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(vt,transform.position)!= 0)
        {
            start = SetPosPlayerInTile();
            enemy.FindCharater();
            
        }
        if (checkStart == true)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[list[i]]) == 0)
            {
                i++;
            }
            if (Vector2.Distance(transform.position, grid.listTiles[list[i]]) != 0)
            {
                checkMove = true;
            }
            if (checkMove == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, grid.listTiles[list[i]], Time.deltaTime);
                checkMove = false;
            }
            if (Vector2.Distance(transform.position, grid.listTiles[list[list.Count - 1]]) == 0)
            {
                start = finish;
                checkStart = false;
                list.Clear();
                i = 0;
            }
        }
    }
    public void Find()
    {
        list.Clear();
        start = SetPosPlayerInTile();
        finish = enemy.startEnemy;
        for (int i = 0; i < managerEnemy.listPosEnemy.Count; i++)
        {
            if (managerMove.Dijkstra(start, finish) == true)
            {
                checkStart = true;
                break;
            }
            else
            {
                continue;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        
    }
    void SetCheckHealth()
    {
        checkHealth = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }

}
