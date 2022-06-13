using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject btnDestroy;
    public GameObject btnMenu;
    public GameObject btnSwap;
    public GameObject btnStartGame;
    public static CanvasController instance;
    public void Awake()
    {
        instance = this;
    }
}

