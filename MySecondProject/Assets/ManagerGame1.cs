using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame1 : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit2D hitInformation;
    public PlayerController player;
    public bool CheckFind = true;
    ManagerMove managerMove;
    ManagerEnemy managerEnemy;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        managerMove = FindObjectOfType<ManagerMove>();
        managerEnemy = FindObjectOfType<ManagerEnemy>();
        managerEnemy.SetPosOfEnemy();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchWorld.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            //hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
            //for (int i = 0; i < GridManager.Instance.listTiles.Count; i++)
            //{
            //    if (Vector2.Distance(hitInformation.transform.position, GridManager.Instance.listTiles[i]) <= 0.5f && hitInformation.collider.tag == "Enemy")
            //    {
            //        player.finish = i;
            //        GridManager.Instance.UpdateAfterDragObstacle(player.finish);
            //        CheckFind = true;
            //        break;
            //    }
            //}
            CheckFind = true;
        }
        if (CheckFind == true)
        {
            player.FindGame2();
            //CheckFind = false;
            //for(int i = 0; i < managerEnemy.listEnemy.Count;i++)
            //{
            //    player.finish = managerEnemy.listEnemy[i].startEnemy;
            //    if (managerMove.Dijkstra(player.start, player.finish) == true)
            //    {
            //        player.FindGame2();
            //        GridManager.Instance.UpdateObstacle(player.start);              
            //    }
            //}
            
        }
    }

}
