using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
    public GameObject gameObjectPanel;
    public Text txtScoreEndGame;
    public static ManagerGame instance;
    public bool gameOver = false;
    public Algorithm algorithm;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    { 

    }

    public void ResumeGame()
    {
        ManagerNumber.instance.ListTemp.Clear();
        for (int i = 0; i < ManagerNumber.instance.list.Count; i++)
        {
            if (ManagerNumber.instance.list[i].id == algorithm.firstRan)
            {
                ManagerNumber.instance.ListTemp.Add(ManagerNumber.instance.list[i]);
                ManagerNumber.instance.list[i].id += 1;
                ManagerNumber.instance.list[i].SetSpriteNumber(ManagerNumber.instance.listSprite[ ManagerNumber.instance.list[i].id - 1]);
                ManagerNumber.instance.list[i].transform.name = Mathf.Pow(2, ManagerNumber.instance.list[i].id).ToString();
                ManagerNumber.instance.list[i].SetTxtNumber(Mathf.Pow(2, ManagerNumber.instance.list[i].id).ToString());
            }
        }
        ManagerNumber.instance.CheckListTemp();
    }
    public void Replay()
    {
        ManagerNumber.instance.list.Clear();
        for(int i = 0; i < ManagerNumber.instance.listObject.Count; i++)
        {
            Destroy(ManagerNumber.instance.listObject[i]);
        }
        ManagerNumber.instance.listObject.Clear();
        ManagerNumber.instance.imageSpawn.sprite = null;
        GridManager.instance.SetMatrixZero();
        CanvasController.instance.score = 0;
        CanvasController.instance.SetScore(0);
        gameOver = false;
    }

    public void GameOver(PlayerController player)
    {
        gameObjectPanel.SetActive(true);
        txtScoreEndGame.text = CanvasController.instance.score.ToString();
        player.SetState(PlayerController.State.block);
        gameOver = true;
    }
}
