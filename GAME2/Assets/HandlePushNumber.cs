using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePushNumber 
{
    // Start is called before the first frame update
    
    public bool spawnNumber = true;
    int temp2;        
    public void SpawnNumber(ManagerNumber managerNumber)
    {
        if(spawnNumber == true)
        {
            managerNumber.Spawn();
            spawnNumber = false;
        }
    }
    public void HandlePush(TouchWorld touch,PlayerController player)
    {
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            if (touch.x == i)
            {
                ManagerNumber.instance.number.transform.position = new Vector2(i, 0);
                touch.temp1 = i;
                CheckColoume(ManagerNumber.instance.number, player);
            }
        }
    }
    public void CheckColoume(Number number,PlayerController player)
    {
        for (int i = GridManager.instance.hight - 1; i >= 0; i--)
        {
            if (GridManager.instance.matrix[player.touch.temp1, i] == 0)
            {
                number.transform.position = new Vector2(player.touch.temp1, i);
                GridManager.instance.matrix[player.touch.temp1, i] = 1;
                temp2 = i;
                return;
            }
        }
    }
}
