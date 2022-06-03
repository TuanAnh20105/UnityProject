using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
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
    ManagerEnemy managerEnemy;
    [HideInInspector] public Vector3 vt;
    ManagerMove managerMove;
    int updatePosPlayer;
    public bool checkFind = false;
    [HideInInspector] public int save = 0;
    public LineRenderer line;

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
            if (Vector2.Distance(transform.position, grid.listTiles[i]) <=0.4f)
            {
                updatePosPlayer = i;
                vt = grid.listTiles[i];
                break;               
            }
        }
        return updatePosPlayer;
    }
    // Update is called once per frame
    private void FixedUpdate()
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
        if(checkStart == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, line.GetPosition(0), Time.deltaTime);
            Find();
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
                line.positionCount = list.Count;
                for(int j = 0; j < list.Count; j++)
                {
                    line.SetPosition(j, grid.listTiles[list[j]]);
                }
                save = i;
                checkStart = true;
                break;
            }
        }
        if(list.Count ==0)
        {
            finish = -1;
        }
        if(list.Count != 0)
        {
            checkFind = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            checkFind = false;
        }
    }

}
