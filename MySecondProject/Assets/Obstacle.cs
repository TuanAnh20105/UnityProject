using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Obstacle : MonoBehaviour
{
    RaycastHit2D hitInformation;
    ManagerDragAndDrop manager;
    ManagerMove managerMove;
    public UnityEvent unityEvent;
    ManagerEnemy managerEnemy;
    GridManager grid;  
    public GameObject obstacle7;
    public GameObject obstacle6;
    public GameObject obstacle1, obstacle22, obstacle25, obstacle4;
    public VatCan vatCan;
    EnemyController enemy;
    PlayerController player;



    void Start()
    {
        manager = ManagerDragAndDrop.Instance;
        grid = FindObjectOfType<GridManager>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerMove = FindObjectOfType<ManagerMove>();
        player = FindObjectOfType<PlayerController>();
        enemy = FindObjectOfType<EnemyController>();
        managerEnemy.SetPosOfEnemy();
    }
   

    // Update is called once per frame
    void Update()
    {

        var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchWorld.z = 0;
        if(Input.GetMouseButtonDown(0))
        {
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);

            if(hitInformation.collider != null && hitInformation.collider.gameObject.tag != "Tile" && hitInformation.collider.gameObject.tag != "Enemy")
            {
                GameObject touchObject = hitInformation.transform.gameObject;
                manager.moveObject = touchObject;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(hitInformation.transform.name =="Obstacle1")
            {
                Destroy(obstacle1);
                grid.UpdateAfterDragObstacle(1);
                enemy.FindCharater();
                player.Find();
            }
            if (hitInformation.transform.name == "Obstacle4")
            {
                Destroy(obstacle4);
                grid.UpdateAfterDragObstacle(4);
                enemy.FindCharater();
                player.Find();

            }
            if (hitInformation.transform.name == "Obstacle22")
            {
                Destroy(obstacle22);
                grid.UpdateAfterDragObstacle(22);
                enemy.FindCharater();
                player.Find();

            }
            if (hitInformation.transform.name == "Obstacle25")
            {
                Destroy(obstacle25);
                grid.UpdateAfterDragObstacle(25);
                enemy.FindCharater();
                player.Find();
            }
        }
    }
}
