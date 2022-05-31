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



    void Start()
    {
        manager = ManagerDragAndDrop.Instance;
        grid = FindObjectOfType<GridManager>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerMove = FindObjectOfType<ManagerMove>();
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
                managerEnemy.SetPosOfEnemy();
                obstacle1.gameObject.SetActive(false);
                grid.UpdateAfterDragObstacle(1);             
                managerMove.Find();
                managerMove.FindCharater();
              
                
            }
            if (hitInformation.transform.name == "Obstacle4")
            {
                managerEnemy.SetPosOfEnemy();
                obstacle4.gameObject.SetActive(false);
                grid.UpdateAfterDragObstacle(4);
                managerMove.Find();
                managerMove.FindCharater();

            }
            if (hitInformation.transform.name == "Obstacle22")
            {
                obstacle22.gameObject.SetActive(false);
                grid.UpdateAfterDragObstacle(22);
                managerMove.Find();
                managerMove.FindCharater();

            }
            if (hitInformation.transform.name == "Obstacle25")
            {
                obstacle25.gameObject.SetActive(false);
                grid.UpdateAfterDragObstacle(25);
                managerMove.Find();
                managerMove.FindCharater();

            }
        }
    }

}
