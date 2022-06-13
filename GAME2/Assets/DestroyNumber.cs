using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNumber : MonoBehaviour
{
    public bool checkDestroy = false;
    public void DestoyNumber(PlayerController player , ManagerGame managerGame)
    {
            
            player.touch.Touch();
            if(player.touch.hitInformation.collider != null)
            {          
                for (int i = 0; i < ManagerNumber.instance.list.Count; i++)
                {
                    if (Vector2.Distance(ManagerNumber.instance.list[i].transform.position, player.touch.hitInformation.transform.position) < 1f)
                    {
                        Number temp = ManagerNumber.instance.list[i];
                        int x = (int)ManagerNumber.instance.list[i].transform.position.x;
                        int y = (int)ManagerNumber.instance.list[i].transform.position.y;
                        GridManager.instance.matrix[x, y] = 0;                                                
                        Destroy(ManagerNumber.instance.list[i]);
                        ManagerNumber.instance.list.RemoveAt(i);
                        Destroy(ManagerNumber.instance.listObject[i]);
                        ManagerNumber.instance.listObject.RemoveAt(i);
                        managerGame.temp1 = x;
                        managerGame.temp2 = y;
                        managerGame.GetElementsInColumn(x);
                        managerGame.CheckPosDestroy(x, temp);
                        managerGame.CheckAferDestroy(managerGame.ListTemp, player) ;
                        checkDestroy = true;
                        break;
                    }
                    else
                    {
                        checkDestroy = false;
                    }
                }
            }
            
        
    }
}
