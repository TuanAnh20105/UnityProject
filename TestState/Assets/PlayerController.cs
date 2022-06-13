
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
   public enum State
    {
        play,
        stop,
        menu

    }
    private State state;
    PlayGame playGame = new PlayGame();
    public void SetState(State state)
    {
        this.state = state;
    }
    public void HandleRequest()
    {
        switch(state)
        {
            case State.play:
                playGame.PlayGameCol();
                break;
            case State.stop:
                Debug.Log("stop game");
                break;
            case State.menu:
                Debug.Log("menu game");
                break;
        }
    }
    public void Start()
    {
       
        SetState(State.play);
        //HandleRequest();
    }
    public void Update()
    {
        HandleRequest();
    }
    public void SetStop()
    {
        state = State.stop;


    }
}
