﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePushNumber : MonoBehaviour
{
    // Start is called before the first frame update
    public bool checkPush = true;
    public bool spawnNumber = true;
    public bool checkLastCol = true;
    public bool gameOver = false;
    public Vector2 temp;
    public static HandlePushNumber instance;
    void Awake()
    {
        instance = this;
    }
    
    public void SpawnNumber(ManagerNumber managerNumber, PlayerController player)
    {
        if (spawnNumber == true)
        {
            if(managerNumber.list.Count > 34)
            {
                ManagerGame.instance.GameOver(player);
                player.SetState(PlayerController.State.block);
                ManagerGame.instance.gameOver = true;
                spawnNumber = false;
                return;
            }
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

                ManagerNumber.instance.number.transform.position = new Vector2(i, -3);
                touch.temp1 = i;
                CheckColoume(ManagerNumber.instance.number, player);
                return;
            }
        }
    }
    public void CheckColoume(Number number, PlayerController player)
    {

        for (int i = GridManager.instance.hight - 1; i >= 1; i--)
        {
            if (GridManager.instance.matrix[player.touch.temp1, i] == 0)
            {
                temp = new Vector2(player.touch.temp1, i);
                GridManager.instance.matrix[player.touch.temp1, (int)temp.y] = number.id;
                number.transform.DOMove(temp, 0.2f).SetEase(Ease.Flash).OnComplete(()=>
                {
                    player.SetState(PlayerController.State.handleNumber);
                    player.checkPushNumber = true;
                    checkLastCol = false;
                });
                player.checkPushNumber = false;                
                break;
            }
            else if (GridManager.instance.matrix[player.touch.temp1, 1] != 0 && GridManager.instance.matrix[player.touch.temp1, 1] == number.id )
            {
                temp = new Vector2(player.touch.temp1, 0);
                GridManager.instance.matrix[player.touch.temp1, (int)temp.y] = number.id;
                number.transform.DOMove(temp, 0.2f).SetEase(Ease.Flash).OnComplete(() =>
                {
                    player.SetState(PlayerController.State.handleNumber);
                    player.checkPushNumber = true;
                    checkLastCol = false;
                });
                player.checkPushNumber = false;
                break;
            }
            else
            {
                player.SetState(PlayerController.State.touch);
            }
            

        }
    }
}
