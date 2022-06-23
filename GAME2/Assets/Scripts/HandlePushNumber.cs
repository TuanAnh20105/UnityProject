using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePushNumber
{
    // Start is called before the first frame update
    public bool checkPush = true;
    public bool spawnNumber = true;
    Vector2 temp;
    public void SpawnNumber(ManagerNumber managerNumber)
    {
        if (spawnNumber == true)
        {
            managerNumber.Spawn();
            spawnNumber = false;
        }
    }
    public void HandlePush(TouchWorld touch, PlayerController player)
    {
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            if (touch.x == i)
            {

                ManagerNumber.instance.number.transform.position = new Vector2(i, -1);
                touch.temp1 = i;
                CheckColoume(ManagerNumber.instance.number, player);
                return;
            }
        }
    }
    public void CheckColoume(Number number, PlayerController player)
    {
        for (int i = GridManager.instance.hight - 1; i >= 0; i--)
        {
            if (GridManager.instance.matrix[player.touch.temp1, i] == 0 )
            {
                temp = new Vector2(player.touch.temp1, i);
                GridManager.instance.matrix[player.touch.temp1, (int)temp.y] = 1;
                ManagerNumber.instance.PrintfTest();
                number.transform.DOMove(temp, 0.2f).SetEase(Ease.Flash).OnComplete(()=>
                {
                    player.SetState(PlayerController.State.handleNumber);
                    player.checkPushNumber = true;
                });
                player.checkPushNumber = false;                
                break;
            }
        }
    }
}
