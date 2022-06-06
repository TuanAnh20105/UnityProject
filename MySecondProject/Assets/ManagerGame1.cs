using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame1 : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit2D hitInformation;
    public PlayerController player;
    public bool CheckFind = false;
    ManagerMove managerMove;
    ManagerEnemy managerEnemy;
    SetUpObstaclesGame2 setUp;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        managerMove = FindObjectOfType<ManagerMove>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerEnemy.SetPosOfEnemy();
         setUp = FindObjectOfType<SetUpObstaclesGame2>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchWorld.z = 0;
        if(Input.GetMouseButtonDown(0))
        {
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
            for(int i = 0; i < GridManager.Instance.listTiles.Count;i++)
            {
                if(Vector2.Distance(hitInformation.transform.position , GridManager.Instance.listTiles[i]) <=0.5f && hitInformation.collider.tag == "Enemy")
                {
                    player.finish = i;               
                    GridManager.Instance.UpdateAfterDragObstacle(player.finish);
                    CheckFind = true;
                    break;
                }
            }
        }
        if(CheckFind == true)
        {
            if (managerMove.Dijkstra(player.start, player.finish) == true)
            {
                player.FindGame2();
                GridManager.Instance.UpdateObstacle(player.start);              
            }
            
        }
    }
}
