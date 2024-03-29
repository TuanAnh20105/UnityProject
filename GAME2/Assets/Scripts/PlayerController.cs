﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
       block, None, startGame,playGame,spawnNumber,touch,pushNumber,handleNumber, menu,stop,quit, destroy, swap, doingMerge
    }
    private State state;
    public StartGame startGame = new StartGame();
    public TouchWorld touch = new TouchWorld();
    public HandleMixNumber handleMix = new HandleMixNumber();
    public DestroyNumber destroy;
    public SwapNumber swap;
    public CanvasController canvas;
    public ManagerNumber managerNumber;
    public bool check = true,checkPushNumber = true;
    public GameObject a;
    public Algorithm algorithm;
    public void SetState( State state)
    {
        this.state = state;
    }
    void Start()
    {
        SetState(State.None);
    }
    void Update()
    {
        canvas.SetTime();
        if(check == true)
        {
            HandleRequest();
        } 
    }
    public void HandleRequest()
    {
        switch(state)
        {
            case State.block:
                break;
            case State.None:

                None();
                break;

            case State.startGame:
                startGame.GameStart();
                SetState(State.spawnNumber);
                break;

            case State.spawnNumber:
                HandlePushNumber.instance.SpawnNumber(managerNumber, this);
                if (ManagerGame .instance.gameOver == false)
                {
                    SetState(State.touch);
                }
                break;  
            case State.touch:
                if(Input.GetMouseButtonDown(0))
                {
                    touch.Touch();
                    if(touch.hitInformation.collider != null)
                    {                        
                        SetState(State.pushNumber);                        
                    }
                }     
                break;

            case State.pushNumber:
                if(checkPushNumber == true)
                {
                    HandlePushNumber.instance.HandlePush(touch,this);
                }              
                break;

            case State.handleNumber:
                handleMix.HandleMix(ManagerNumber.instance.number,this);
                if (ManagerNumber.instance.find == false)
                {
                    HandlePushNumber.instance.spawnNumber = true;
                    SetState(State.spawnNumber);
                }
                break;
            case State.doingMerge:
                managerNumber.CheckListTemp(this);
                if (managerNumber.doneMerge == true)
                {
                    HandlePushNumber.instance.spawnNumber = true;
                    SetState(State.spawnNumber);
                    managerNumber.doneMerge = false;
                }
                break;
            case State.destroy:
                destroy.DestoyNumber(this, managerNumber);
                if (destroy.checkDestroy == true)
                {
                    SetState(State.touch);
                    destroy.checkDestroy = false;
                }
                break; 


            case State.swap:
                swap.Swap(this,managerNumber);
                if (swap.checkSwap == true)
                {
                    SetState(State.touch);
                    swap.checkSwap = false;
                }
                break;
        }
    }
    public void None()
    {

    }

    public void StartGame()
    {
        ManagerGame.instance.gameOver = false;
        HandlePushNumber.instance.spawnNumber = true;
        SetState(State.startGame);
    }    
    public void Destroy()
    {
        SetState(State.destroy);
    }
    public void Swap()
    {
        SetState(State.swap);
    }
    public void Block()
    {
        SetState(State.block);
    } 
    public void Touch()
    {
        SetState(State.touch);
    }
    public void spawnNumber()
    {
        HandlePushNumber.instance.spawnNumber = true;
        SetState(State.spawnNumber);
    }

    public void RestartGame()
    {
        managerNumber.list.Clear();
        
    }
}
