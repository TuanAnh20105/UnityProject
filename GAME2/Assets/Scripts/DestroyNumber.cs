using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNumber : MonoBehaviour
{
    public bool checkDestroy = false;
    public void DestoyNumber(PlayerController player , ManagerNumber managerNumber)
    {
            
            player.touch.Touch();
            if(player.touch.hitInformation.collider != null)
            {          
                for (int i = 0; i < managerNumber.list.Count; i++)
                {
                    if (Vector2.Distance(managerNumber.list[i].transform.position, player.touch.hitInformation.transform.position) < 0.5f)
                    {
                        Number temp = managerNumber.list[i];
                        int x = (int)managerNumber.list[i].transform.position.x;
                        int y = (int)managerNumber.list[i].transform.position.y;
                        GridManager.instance.matrix[x, y] = 0;                                                
                        Destroy(managerNumber.list[i]);
                        managerNumber.list.RemoveAt(i);
                        Destroy(managerNumber.listObject[i]);
                        managerNumber.listObject.RemoveAt(i);
                        managerNumber.temp1 = x;
                        managerNumber.temp2 = y;
                        managerNumber.GetElementsInColumn(x,y);
                        managerNumber.CheckPosDestroy(x, temp);
                        managerNumber.CheckAferDestroy(managerNumber.ListTemp, player) ;
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
