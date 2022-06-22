using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm :MonoBehaviour
{
    public int block = 10;
    public int lastRan = 6;
    public int firstRan = 1;
    public List<int> listNumberInMatrix = new List<int>();
    public int[,] matrixVitual = new int[200, 200];
    public  List<Number> listNumberVitual = new List<Number>();
    HandlePushNumber handlePush = new HandlePushNumber();
    public List<int> listNumberLastInCol = new List<int>();
    int min = 0;
    public List<Number> listNumberSpawn = new List<Number>();
    List<int> listIndex = new List<int>();
    public List<Number> list;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();
    public List<Number> ListTemp = new List<Number>();



    List<int> listColumn = new List<int>();
    List<int> listRow = new List<int>();
    bool checkAdd = true;
    public Number temp = new Number();
    public bool find = false;
    int t = 0;
    bool CheckUpdateNode1 = false;
    public static ManagerNumber instance;
    public int temp1, temp2;
    public int ran;
    public int idTemp = 0;
    public int countMerge = 0;
    public void CreateMatrixVitual()
    {
        for(int i = 0; i < GridManager.instance.width;i++)
        {
            for(int j = 0; j< GridManager.instance.hight;j++ )
            {
                matrixVitual[i, j] = GridManager.instance.matrix[i, j];
            }
        }
        for( int n = 0; n < ManagerNumber.instance.list.Count;n++)
        {
            listNumberVitual.Add(ManagerNumber.instance.list[n]);
        }
    }
    public void Handle(ManagerNumber managerNumber, PlayerController player)
    {
        if(managerNumber.list.Count > 1)
        {
            CreateMatrixVitual();
            GetElementLastInCol(managerNumber);
        }

        for (int i = 0; i < listNumberLastInCol.Count; i++)
        {
            listNumberVitual[listNumberLastInCol[i]].id += 1;
            listNumberVitual[listNumberLastInCol[i]].ma += 1;
        }

    }
    public void checkNumber(Number number)
    {
        for(int i = 0; i< listNumberVitual.Count;i++)
        {
            if(Vector2.Distance(number.transform.position, listNumberVitual[i].transform.position) <= 1.1f
                && number.id == listNumberVitual[i].id && listNumberVitual.Count > 1 && number.ma != listNumberVitual[i].ma)
            {
                listIndex.Add(i);
            }
        }
    }
    public void AlgorithmNumber(ManagerNumber managerNumber)
    {

        if (managerNumber.idTemp > block)
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
        //CheckNumberInMatrix(managerNumber);
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
                if ( matrixVitual[x,y] == 1)
                {
                    listNumberLastInCol.Add (managerNumber.GetIndexInList(x, y, listNumberVitual));
                    break;
                }
            }
        }
        //managerNumber.listSpawn.Sort();
    }
    public void CheckNumberInMatrix(ManagerNumber managerNumber)
    {
        for (int x = 0; x < GridManager.instance.width; x++)
        {
            for (int y = 0; y < GridManager.instance.hight; y++)
            {
                if (GridManager.instance.matrix[x, y] == 1)
                {
                    listNumberInMatrix.Add(managerNumber.GetNumberWithPos(x, y).id - 1);
                }

            }
        }
        listNumberInMatrix.Sort();
    }






    public void CheckNumber(Number number, List<Number> list)
    {
        CheckNumbersMix(number, list);
        if (listIndex.Count > 0)
        {
            UpdateNode(number);
            //StartCoroutine(Test(number));
            DeleteAllNodeMix();
            UpdateListNumber();

        }
        else
        {
            find = false;
        }
    }
    IEnumerator Test(Number number)
    {
        yield return new WaitForSeconds(1);
        UpdateNode(number);
    }
    public void CheckNumbersMix(Number number, List<Number> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (Vector2.Distance(number.transform.position, list[i].transform.position) <= 1.1f
                && number.id == list[i].id && list.Count > 1 && number.ma != list[i].ma)
            {
                find = true;
                listIndex.Add(i);
                UpdateMatrix(number, list[i]);
                CheckUpdateNode1 = true;
            }
        }
    }
    public void UpdateMatrix(Number number, Number list)
    {
        temp1 = (int)number.transform.position.x;
        temp2 = (int)number.transform.position.y;

        GridManager.instance.matrix[temp1, temp2] = 0;
        int x = (int)list.transform.position.x;
        int y = (int)list.transform.position.y;
        GridManager.instance.matrix[x, y] = 0;
        listColumn.Add(x);
        if (temp1 != x)
        {
            if (checkAdd == true)
            {
                listColumn.Add(temp1);
                listRow.Add(temp2);
                checkAdd = false;
            }
        }
        listRow.Add(temp2);
    }
    public void UpdateListNumber()
    {
        for (int i = 0; i < listColumn.Count; i++)
        {
            GetElementsInColumn(listColumn[i], listRow[i]);
            for (int j = 0; j < listNumsInCol.Count; j++)
            {
                UpdateNodeInColumn(listColumn[i], listRow[i]);
            }
        }
        listIndex.Clear();
        listColumn.Clear();
        listRow.Clear();
        Debug.Log(ListTemp.Count);
        CheckListTemp();
    }
    public void CheckListTemp()

    {
        if (ListTemp.Count != 0)
        {
            for (int i = 0; i < ListTemp.Count; i++)
            {
                checkAdd = true;
                CheckNumber(ListTemp[i], list);
            }
            ListTemp.Clear();
        }
    }
    public void DeleteAllNodeMix()
    {
        for (int i = 0; i < listIndex.Count; i++)
        {
            list.RemoveAt(listIndex[i]);
        }
    }
    public void UpdateNode(Number number)
    {
        countMerge += listIndex.Count;
        if (CheckUpdateNode1 == true)
        {
            int count = listIndex.Count;

            number.id += count;
            if (idTemp < number.id)
            {
                idTemp = number.id;
            }
            number.transform.name = Mathf.Pow(2, number.id).ToString();
            number.SetTxtNumber(Mathf.Pow(2, number.id).ToString());
            CheckUpdateNode1 = false;
            CanvasController.instance.SetScore(Mathf.Pow(2, number.id));
        }
    }
    public void UpdateNodeInColumn(int column, int row1)
    {
        while (listNumsInCol.Count != 0)
        {
            for (int i = GridManager.instance.hight - 1; i >= 0; i--)
            {
                if (matrixVitual[column, i] == 0)
                {
                    int row = (int)listNumsInCol[0].transform.position.x;
                    int column1 = (int)listNumsInCol[0].transform.position.y;
                    if (column1 <= row1)
                    {
                        ListTemp.Add(listNumsInCol[0]);
                        matrixVitual[row, column1] = 0;
                        matrixVitual[column, i] = 1;
                        listNumsInCol[0].transform.position = new Vector2(column, i);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
    }
    public void GetElementsInColumn(float x, float y)
    {
        listNumsInCol.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position.x == x && list[i].transform.position.y <= y)
            {
                listNumsInCol.Add(list[i]);
            }
        }

    }
}
