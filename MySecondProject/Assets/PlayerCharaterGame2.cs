using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharaterGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text healthText;
    [HideInInspector] public int health = 20;
    [HideInInspector] public int a;
    public int start, finish;
    public bool checkStart = false;
    public int i = 1;
    GridManager grid;
    public List<int> list = new List<int>();
    ManagerEnemyGame2 managerEnemy;
    [HideInInspector] public Vector3 vt;
    ManagerMoveGame2 managerMove;
    int updatePosPlayer;
    public bool checkFind = false;
    public List<int> save = new List<int>();
    public LineRenderer line;
    Tile tile;

    void Start()
    {
        vt = gameObject.transform.position;
        a = health;
        healthText.text = health.ToString();
        managerEnemy = FindObjectOfType<ManagerEnemyGame2>();
        managerMove = FindObjectOfType<ManagerMoveGame2>();
        grid = FindObjectOfType<GridManager>();
        tile = FindObjectOfType<Tile>();
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
            if (Vector2.Distance(transform.position, grid.listTiles[i]) <= 0.4f)
            {
                updatePosPlayer = i;
                vt = grid.listTiles[i];
                break;
            }
        }
        return updatePosPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        if (checkStart == true)
        {
            if(line.positionCount!=0)
            {
                transform.position = Vector2.MoveTowards(transform.position, line.GetPosition(0), Time.deltaTime);

            }
            Find();
        }
    }
    public void Find()
    {
        list.Clear();
        save.Clear();        
        start = SetPosPlayerInTile();
        if (managerMove.Dijkstra(start, finish) == true)
        {
            line.positionCount = list.Count;
            for (int j = 0; j < list.Count; j++)
            {
                line.SetPosition(j, grid.listTiles[list[j]]);
            }
            if (save.Count == 0)
            {
                save.Add(i);
            }
            if (save.Count != 0)
            {
                for (int z = 0; z < save.Count; z++)
                {
                    if (i != save[z])
                    {
                        save.Add(i);
                    }

                }
            }
            checkStart = true;
            checkFind = true;
            //break;
            
        }
        if (list.Count != 0)
        {
            checkFind = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            checkFind = false;
            Destroy(other);
        }
    }

}
