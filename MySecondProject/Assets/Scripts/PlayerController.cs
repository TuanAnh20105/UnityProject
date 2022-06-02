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
    public int i = 1;
    GridManager grid;
    public List<int> list = new List<int>();
    ManagerEnemy managerEnemy;
    public Vector3 vt;
    ManagerMove managerMove;
    bool checkMove = false;
    int updatePosPlayer;
    public bool checkFind = false;
    public int save = 0;

    void Start()
    {
        vt = gameObject.transform.position;
        a = health;
        healthText.text = health.ToString();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerMove = FindObjectOfType<ManagerMove>();
        grid = FindObjectOfType<GridManager>();
        SetPosCharater();
        i = 1;
    }
    public void SetPosCharater()
    {
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[i]) < 0.5f)
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
            if (Vector2.Distance(transform.position, grid.listTiles[i]) ==0)
            {
                updatePosPlayer = i;
                break;
            }
        }
        return updatePosPlayer;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if (checkFind == true)
        {
            if (Vector2.Distance(vt, transform.position) != 0)
            {
                start = SetPosPlayerInTile();
                managerEnemy.listEnemy[save].FindCharater();               
            }
        }
    }
    void Update()
    {
        Debug.Log("this save " + save);
        if (checkStart == true)
        {
            //if (Vector2.Distance(transform.position, grid.listTiles[list[i]]) == 0)
            //{
            //    i++;
            //}
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
                i = 1;
            }

        }
    }
    public void Find()
    {
        list.Clear();
        
        for (int i = 0; i < managerEnemy.listEnemy.Count; i++)
        {
            start =  SetPosPlayerInTile();
            finish = managerEnemy.listEnemy[i].startEnemy;
            if (managerMove.Dijkstra(start, finish) == true)
            {
                save = i;
                checkStart = true;
                break;
            }
            else
            {
                continue;
            }
        }
    }

}
