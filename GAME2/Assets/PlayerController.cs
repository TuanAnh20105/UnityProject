using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        None, startGame,playGame,spawnNumber,touch,pushNumber,handleNumber, menu,stop,quit, destroy, swap
    }
    private State state;
    public StartGame startGame = new StartGame();
    public HandlePushNumber handlePush = new HandlePushNumber();
    public TouchWorld touch = new TouchWorld();
    public HandleMixNumber handleMix = new HandleMixNumber();
    public DestroyNumber destroy;
    public SwapNumber swap;
    public CanvasController canvas;
    public ManagerNumber managerNumber;
    public bool check = true;
    public static event Action<bool> findNumber;
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
        if(check == true)
        {
            HandleRequest();
        }
    }
    public void HandleRequest()
    {
        switch(state)
        {
            case State.None:

                None();
                break;

            case State.startGame:
                startGame.GameStart(canvas);
                SetState(State.spawnNumber);
                break;

            case State.spawnNumber:
                handlePush.SpawnNumber(managerNumber);
                SetState(State.touch);
                break;  

            case State.touch:
                touch.Touch();
                if(Input.GetMouseButtonDown(0))
                {
                    if(touch.hitInformation.collider != null)
                    {                        
                        SetState(State.pushNumber);                        
                    }
                }     
                break;

            case State.pushNumber:
                handlePush.HandlePush(touch,this);
                SetState(State.handleNumber);
                break;

            case State.handleNumber:
                handleMix.HandleMix(ManagerNumber.instance.number,this);
                if (ManagerNumber.instance.find == false)
                {
                    handlePush.spawnNumber = true;
                    SetState(State.spawnNumber);
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
                swap.Swap(this , managerNumber);
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
        CanvasController.instance.btnStartGame.SetActive(true);
        CanvasController.instance.btnDestroy.SetActive(false);
        CanvasController.instance.btnSwap.SetActive(false);
        CanvasController.instance.btnMenu.SetActive(false);
    }
    public void StartGame()
    {
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
}
