using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame 
{
    // Start is called before the first frame update
    
    
    public void GameStart()
    {   
        GridManager.instance.GenerateGrid();       
    }
}
