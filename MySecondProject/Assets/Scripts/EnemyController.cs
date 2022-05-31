using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public TMP_Text healthText ;
    public int health;
    PlayerController player;
    ManagerMove managerMove;
    Vector2 a;
    public bool checkStart = false;
    bool checkMove = false;
    GridManager grid;
    int i = 1;

    void Start()
    {
        a = transform.position;
        player = FindObjectOfType<PlayerController>();
        managerMove = FindObjectOfType<ManagerMove>();
    }

    // Update is called once per frame
    void Update()
    {    
        if(checkStart == true)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[managerMove.list1[i]]) == 0)
            {
                i++;
            }
            if (Vector2.Distance(transform.position, grid.listTiles[managerMove.list1[i]]) != 0)
            {
                checkMove = true;
            }
            if (checkMove == true)
            {
               transform.position = Vector3.MoveTowards(transform.position, grid.listTiles[managerMove.list1[i]], Time.deltaTime);
                checkMove = false;
            }
            if (Vector2.Distance(transform.position, grid.listTiles[managerMove.list1[managerMove.list1.Count - 1]]) == 0)
            {
                 managerMove.startEnemy = managerMove.finishEnemy;
                checkStart = false;
                managerMove.list1.Clear();
                i = 0;
            }
        }
        else
        {

        }
        UpdateTextBox();  

    }
    void UpdateTextBox()
    {
        healthText.text = health.ToString();
    }
    private void OnMouseDown() 
    {
        healthText.text = health.ToString();
    }

    
    
}
