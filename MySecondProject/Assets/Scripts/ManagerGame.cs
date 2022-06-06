using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    // Start is called before the first frame update\
    public GameObject btnGameOver;
    public GameObject btnGameWin;
    public static ManagerGame Instance;
    private void Awake() {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        btnGameOver.gameObject.SetActive(true);
    }
        public void GameWin()
    {
        btnGameWin.gameObject.SetActive(true);
    }

}
