using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
   public int a = 0;
   public bool checkForce = false,check = false;
   GridManager grid;
   public int temp1;
   public int temp2;
    ManagerNumber managerNumber;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();    
    public List<Number> ListTemp = new List<Number>();
    public List<Number> listNumberMix = new List<Number>();
    public List<int> listCulumnMix = new List<int>();
    public Number temp= new Number();
    void Start()
    {
        managerNumber = ManagerNumber.instance;
        grid = GridManager.instance;
        temp = FindObjectOfType<Number>();        
    }
    public void checkPos(int column , Number number,PlayerController player)
    {
        ListTemp.Clear();
        while(listNumsInCol.Count !=0)
        {
            for (int i = grid.hight - 1; i >= 0; i--)
            {
                if (grid.matrix[column, i] == 0)
                {
                    int row = (int)listNumsInCol[0].transform.position.x;
                    int column1 = (int)listNumsInCol[0].transform.position.y;
                    if(column1 < number.transform.position.y && column1 !=0)
                    {
                        listNumsInCol[0].transform.position = new Vector2(column, i);
                        grid.matrix[row, column1] = 0;
                        grid.matrix[column, i] = 1;
                        ListTemp.Add(listNumsInCol[0]);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
        if (ListTemp.Count != 0)
        {
            managerNumber.number.Check(managerNumber.number,player);
        }
    }
    public void CheckPosDestroy(int column, Number number)
    {
        ListTemp.Clear();
        while (listNumsInCol.Count != 0)
        {
            for (int i = grid.hight - 1; i >= 0; i--)
            {
                if (grid.matrix[column, i] == 0)
                {
                    int row = (int)listNumsInCol[0].transform.position.x;
                    int column1 = (int)listNumsInCol[0].transform.position.y;
                    if (column1 < number.transform.position.y && column1 != 0)
                    {
                        listNumsInCol[0].transform.position = new Vector2(column, i);
                        grid.matrix[row, column1] = 0;
                        grid.matrix[column, i] = 1;
                        ListTemp.Add(listNumsInCol[0]);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
    }
    public void CheckAferDestroy(List<Number> list,PlayerController player)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Check(list[i],player);
        }
    }
    public void GetElementsInColumn(float x)
    {
        listNumsInCol.Clear();
        for(int i = 0; i < managerNumber.list.Count; i++)
        {
            if (managerNumber.list[i].transform.position.x == x && managerNumber.list[i].transform.position.y !=0)    
            {
                listNumsInCol.Add(managerNumber.list[i]);
            }
        }
        
    }
}
