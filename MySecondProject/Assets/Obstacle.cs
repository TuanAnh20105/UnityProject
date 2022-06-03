using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Obstacle : MonoBehaviour
{
    RaycastHit2D hitInformation;
    ManagerDragAndDrop manager;
    public UnityEvent unityEvent;
    ManagerEnemy managerEnemy;
    GridManager grid;  
    public GameObject obstacle7;
    public GameObject obstacle6;
    public GameObject obstacle1, obstacle22, obstacle25, obstacle4, obstacle9;
    public VatCan vatCan;
    PlayerController player;



    void Start()
    {
        manager = ManagerDragAndDrop.Instance;
        grid = FindObjectOfType<GridManager>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        player = FindObjectOfType<PlayerController>();
        managerEnemy.SetPosOfEnemy();
    }
   

    // Update is called once per frame
    void Update()
    {
        TouchWorld();
        CheckInput();
    }
    public void TouchWorld()
    {
        var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchWorld.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);

            if (hitInformation.collider != null && hitInformation.collider.gameObject.tag != "Tile" && hitInformation.collider.gameObject.tag != "Enemy")
            {
                GameObject touchObject = hitInformation.transform.gameObject;
                manager.moveObject = touchObject;
            }
        }
    }
    public void CheckInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (hitInformation.transform.name == "Obstacle1")
            {
                Destroy(obstacle1);
                grid.UpdateAfterDragObstacle(1);
            }
            if (hitInformation.transform.name == "Obstacle4")
            {
                Destroy(obstacle4);
                grid.UpdateAfterDragObstacle(4);

            }
            if (hitInformation.transform.name == "Obstacle22")
            {
                Destroy(obstacle22);
                grid.UpdateAfterDragObstacle(22);
            }
            if (hitInformation.transform.name == "Obstacle9")
            {
                Destroy(obstacle9);
                grid.UpdateAfterDragObstacle(9);
                grid.UpdateAfterDragObstacle(10);
            }
            if (hitInformation.transform.name == "Obstacle25")
            {
                Destroy(obstacle25);
                grid.UpdateAfterDragObstacle(25);
            }
            if (hitInformation.transform.name == "Obstacle1" || hitInformation.transform.name == "Obstacle4" ||
                hitInformation.transform.name == "Obstacle22" || hitInformation.transform.name == "Obstacle9" ||
                hitInformation.transform.name == "Obstacle25")
            {
                player.Find();
                for (int i = 0; i < managerEnemy.listEnemy.Count; i++)
                {
                    managerEnemy.listEnemy[i].FindCharater();
                }
            }
        }
    }
}
