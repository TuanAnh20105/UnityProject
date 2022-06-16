using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePushNumber 
{
    // Start is called before the first frame update
    
    public bool spawnNumber = true;
    public bool checkPush = false;
    Vector2 temp;
    int temp2;
    Rigidbody2D rb;
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
        //rb = ManagerNumber.instance.number.GetComponent<Rigidbody2D>();
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            if (touch.x == i)
            {
                ManagerNumber.instance.number.transform.position = new Vector2(i, 0);
                touch.temp1 = i;
                CheckColoume(ManagerNumber.instance.number, player);     
                //while(checkPush == false)
                //{
                //    ManagerNumber.instance.number.transform.position = Vector2.MoveTowards(ManagerNumber.instance.number.transform.position, temp, Time.deltaTime);
                //    if (Vector2.Distance(ManagerNumber.instance.number.transform.position, temp) == 0)
                //    {
                //        GridManager.instance.matrix[player.touch.temp1, (int)temp.y] = 1;
                //        temp2 = (int)temp.y;
                //        checkPush = true;
                //        //return true;
                //    }
                //}
            }
        }
    }
    public void CheckColoume(Number number,PlayerController player)
    {
        for (int i = GridManager.instance.hight - 1; i >= 0; i--)
        {
            if (GridManager.instance.matrix[player.touch.temp1, i] == 0)
            {      
                temp = new Vector2(player.touch.temp1, i);
                ManagerNumber.instance.number.transform.position = temp;
                GridManager.instance.matrix[player.touch.temp1, (int)temp.y] = 1;
                checkPush = true;
                return;
            }
        }
    }
    public bool Push(Number number, PlayerController player)
    {
        if (Vector2.Distance(number.transform.position, temp) == 0)
        {
            GridManager.instance.matrix[player.touch.temp1, (int)temp.y] = 1;
            temp2 = (int)temp.y;
            checkPush = true;
            return true;
        }
        return false;
    }
}
