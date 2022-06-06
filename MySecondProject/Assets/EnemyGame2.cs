using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyGame2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text healthText;
    public int health;
    PlayerCharaterGame2 player;
    ManagerMoveGame2 managerMove;
    [HideInInspector] public Vector2 a;
    GridManager grid;
    ManagerEnemyGame2 managerEnemy;
    public bool checkStartEnemy = false;
    public int startEnemy, finishEnemy;
    public List<int> list1 = new List<int>();
    bool checkDied = false;
    int updatePosEnemy;
    public bool checkFind = false;
    public LineRenderer line;
    void Start()
    {
        a = transform.position;
        player = FindObjectOfType<PlayerCharaterGame2>();
        managerMove = FindObjectOfType<ManagerMoveGame2>();
        managerEnemy = FindObjectOfType<ManagerEnemyGame2>();
        grid = GridManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
