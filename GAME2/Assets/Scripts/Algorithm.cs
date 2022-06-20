using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm 
{
    public int block = 10;
    public int lastRan = 6;
    public int firstRan = 1;
    public void AlgorithmNumber(ManagerNumber managerNumber)
    {
        if(managerNumber.idTemp > block)
        {
            UpdateNumberBlock(managerNumber,firstRan);
            lastRan += 1;
            firstRan += 1;
            block = managerNumber.idTemp;            
            managerNumber.ran = Random.Range(firstRan, lastRan);
        }
        else
        {
            managerNumber.ran = Random.Range(firstRan, lastRan);
        }       
        //if (managerNumber.listSpawn.Count > 1 && managerNumber.list.Count >= GridManager.instance.hight * GridManager.instance.width *2/ 3)
        //{
        //    managerNumber.ran = Random.Range(managerNumber.listSpawn[0], managerNumber.listSpawn[managerNumber.listSpawn.Count - 1]);
        //}
    }
    public void UpdateNumberBlock(ManagerNumber managerNumber, int idBlock )
    {
        for(int i = 0; i < managerNumber.list.Count;i++)
        {
            if(managerNumber.list[i].id == idBlock)
            {
                managerNumber.list[i].SetSpriteNumber(managerNumber.listSprite[managerNumber.list[i].id]);
                managerNumber.list[i].id += 1;
                managerNumber.list[i].SetTxtNumber(Mathf.Pow(2, managerNumber.list[i].id).ToString());
                managerNumber.list[i].transform.name = Mathf.Pow(2, managerNumber.list[i].id).ToString();
                managerNumber.CheckNumber(managerNumber.list[i]);
            }
        }
    }

    public void GetElementLastInCol(ManagerNumber managerNumber)
    {
        for (int x = 0; x < GridManager.instance.width; x++)
        {
            for (int y = 0; y < GridManager.instance.hight; y++)
            {
                if (GridManager.instance.matrix[x, y] == 1)
                {
                    managerNumber.listSpawn.Add(managerNumber.GetNumberWithPos(x, y).id);
                    break;
                }

            }
        }
        managerNumber.listSpawn.Sort();
    }
}
