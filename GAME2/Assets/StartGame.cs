using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame 
{
    // Start is called before the first frame update
    
    
    public void GameStart(CanvasController canvas)
    {
        canvas.btnDestroy.SetActive(true);
        canvas.btnStartGame.SetActive(false);
        canvas.btnSwap.SetActive(true);
        canvas.btnMenu.SetActive(true);
        GridManager.instance.GenerateGrid();
        
    }
}
