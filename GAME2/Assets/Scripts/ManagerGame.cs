using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
   
    void Start()
    { 

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
        
        
    }
}
