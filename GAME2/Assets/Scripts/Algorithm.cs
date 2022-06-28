using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;

public class Algorithm : MonoBehaviour
{
    public int block = 10;
    public int lastRan = 6;
    public int firstRan = 1;
    private readonly int[,] matrixClone = new int[10, 10];
    public List<int> listNumberLastInColClone = new List<int>();
    public int countMerge = 0;
    int max = 0;
    public List<int> listCountMerge = new List<int>();
    public List<int> listColumn = new List<int>();
    public int merge = 0;
    private int count = 0;
    private int CountOfTileNull()
    {
        int count = 0;
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            for (int j = 0; j < GridManager.instance.hight; j++)
            {
                if (matrixClone[i, j] == 0)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void CreateMatrixClone()
    {
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            for (int j = 0; j < GridManager.instance.hight; j++)
            {
                matrixClone[i, j] = GridManager.instance.matrix[i, j];
            }
        }
    }

    private void ClearMatrixClone()
    {
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            for (int j = 0; j < GridManager.instance.hight; j++)
            {
                matrixClone[i, j] = 0;
            }
        }
    }

    public void Handle()
    {
        CreateMatrixClone();
        GetElementLastInCol();
        SpawnNumberLastColumn();
    }
    private void SpawnNumberLastColumn()
    {
        for (int j = 0; j < listNumberLastInColClone.Count; j++)
        {
            for (int i = 0; i < GridManager.instance.width; i++)
            {
                Vector2 temp = GetPosLastNumberInCol(i);
                matrixClone[i, (int)temp.y - 1] = listNumberLastInColClone[j];
                CheckMatrixClone(i , (int ) temp.y -1 );
                if (max <= countMerge)
                {
                    max = countMerge;
                }
                CreateMatrixClone();
                countMerge = 0;
            }
            listCountMerge.Add(max);
            max = 0;
        }
        CheckNumberWillSpawn();
    
    }
    public void AlgorithmNumber(ManagerNumber managerNumber,PlayerController player)
    {
        if (managerNumber.idTemp > block)
        {
            managerNumber.UpdateNumberBlock(firstRan,player);
            lastRan += 1;
            firstRan += 1;
            block = managerNumber.idTemp;           
            managerNumber.ran = Random.Range(firstRan, lastRan);
        }
        else
        {
            managerNumber.ran = Random.Range(firstRan, lastRan);
        }
        int partGrid = GridManager.instance.hight * GridManager.instance.width / 2;
        if (managerNumber.list.Count <=1)
        {
            managerNumber.ran = firstRan;
            return;
        }
        if(managerNumber.list.Count <4 && managerNumber.list.Count > 2)
        {
            managerNumber.ran = firstRan+2;
            return;
        }
        Handle();
        
    }
    
    private void CheckNumberWillSpawn()
    {
        int temp = 0;
        int max = listCountMerge[0];
        int a = listCountMerge.Max();
        for (int i = 0 ; i < listCountMerge.Count; i++)
        {
            if(max < listCountMerge[i])
            {
                temp = i;
                max = listCountMerge[i];
            }
            if (max == listCountMerge[i])
            {
                if (listNumberLastInColClone[i] > listNumberLastInColClone[temp])
                {
                    temp = i;
                    max = listCountMerge[i];
                }
            }
        }
        Debug.Log("this max " + max);
        Debug.Log("this temp" + temp);
        listCountMerge.Clear();
        ManagerNumber.instance.ran = listNumberLastInColClone[temp];
        int tileNull = CountOfTileNull();
        if (tileNull > 25)
        {
            Debug.Log("Tile Null > 25");
            ManagerNumber.instance.ran = Random.Range(firstRan, lastRan);
        }
        else
        {
            ManagerNumber.instance.ran = Random.Range(firstRan, listNumberLastInColClone[temp]+1);
        }
        if (tileNull < 10 && CanvasController.instance.score < 10000)
        {
            ManagerNumber.instance.ran = listNumberLastInColClone[temp];
        }
        if (ManagerNumber.instance.ran > lastRan)
        {
            ManagerNumber.instance.ran = Random.Range(firstRan, lastRan);
        }
        Debug.Log("This ran " + ManagerNumber.instance.ran);
        return;
    }
    private void CheckMatrixClone(int x , int y)
    {
        merge = 0;
        if( y < GridManager.instance.hight &&  matrixClone[x, y] == matrixClone[x, y + 1])
        {
            matrixClone[x, y + 1] = 0;
            listColumn.Add(x);
            count++;
            merge++;
        }
        if (x < GridManager.instance.width&& matrixClone[x, y] == matrixClone[x + 1, y])
        {
            matrixClone[x + 1, y] = 0;
            listColumn.Add(x+1);
            listColumn.Add(x);
            count++;
            merge++;
        }
        if (x> 0 && matrixClone[x, y] == matrixClone[x - 1, y])
        {
            matrixClone[x - 1, y] = 0;
            listColumn.Add(x-1);
            listColumn.Add(x);
            count++;
            merge++;
        }
        if (merge != 0)
        {
            matrixClone[x, y] += merge;
            UpdateMatrixClone(x);
            countMerge += count;
            count = 0;
        }
    }
    public void CheckElementInColumn()
    {
        if (listColumn.Count != 0)
        {
            for (int j = 0; j < GridManager.instance.hight;j++)
            {
                for (int i = 0; i < listColumn.Count; i++)
                {
                    if (matrixClone[listColumn[i], j] != 0)
                    {
                        CheckMatrixClone(listColumn[i],j);
                    }
                }
            }
        }
    }
    public void UpdateMatrixClone( int x )
    {
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            for (int j = GridManager.instance.hight -1 ; j >= 0; j--)
            {
                if (i == x - 1 || i == x + 1 || i == x)
                {
                    if (matrixClone[i, j] == 0 && j >=1)
                    {
                        SwapPosInMatrix(ref  matrixClone[i,j] , ref matrixClone[i,j-1]);
                    }
                }
                else
                {
                    break;
                }
            }
        }
        CheckElementInColumn();
    }
    public void SwapPosInMatrix( ref int a ,ref  int b)
    {
        int temp = 0;
        temp = a;
        a = b;
        b = temp;
    }
    private Vector2 GetPosLastNumberInCol( int  column)
    {
        for (int y = 0; y < GridManager.instance.hight; y++)
        {
            if (matrixClone[column, y] != 0 && y !=0)
            {
                return new Vector2(column, y);
            }
        }
        return new Vector2(column,GridManager.instance.hight);
    }
    private void GetElementLastInCol()
    {
        listNumberLastInColClone.Clear();
        for (int x = 0; x < GridManager.instance.width; x++)
        {
            for (int y = 0; y < GridManager.instance.hight; y++)
            {
                
                if ( matrixClone != null && matrixClone[x,y] !=0)
                {
                    if (listNumberLastInColClone.Count == 0)
                    {
                        listNumberLastInColClone.Add(matrixClone[x,y]);
                    }
                    else
                    {
                        for (int i = 0; i < listNumberLastInColClone.Count; i++)
                        {
              
                            if (matrixClone[x, y] != listNumberLastInColClone[i])
                            {
                                listNumberLastInColClone.Add(matrixClone[x,y]);
                            }
                        }
                    }
         
                    break;
                }
            }
        }
    }
}
  