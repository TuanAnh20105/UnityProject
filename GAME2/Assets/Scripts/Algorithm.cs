using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    public int block = 10;
    public int lastRan = 6;
    public int firstRan = 1;

    //public List<int> listNumberInMatrix = new List<int>();
    public int[,] matrixVitual = new int[200, 200];
    public  List<Number> listNumberVitual = new List<Number>();
    public List<int> listNumberLastInCol = new List<int>();
    List<int> listIndex = new List<int>();
    public List<Number> listNumsInCol = new List<Number>();
    public List<Number> ListTemp = new List<Number>();
    public Number number = new Number();
    List<int> listColumn = new List<int>();
    List<int> listRow = new List<int>();
    bool checkAdd = true;
    bool CheckUpdateNode1 = false;
    public int temp1, temp2;
    public int countMerge = 0;
    int max = 0;
    public List<int> listCountMerge = new List<int>();
    private void CreateMatrixVitual()
    {
        for(int i = 0; i < GridManager.instance.width;i++)
        {
            for(int j = 0; j< GridManager.instance.hight;j++ )
            {
                matrixVitual[i, j] = GridManager.instance.matrix[i, j];
            }
        }
        listNumberVitual.Clear();
        for( int n = 0; n < ManagerNumber.instance.list.Count;n++)
        {
            listNumberVitual.Add(ManagerNumber.instance.list[n]);
        }
    }
    public void Handle()
    {
        CreateMatrixVitual();
        GetElementLastInCol();
        SpawnNumberLastColumn();
    }
    private void SpawnNumberLastColumn()
    {
        for(int j = 0; j < listNumberLastInCol.Count;j++)
        {           
            for (int i = 0; i < GridManager.instance.width; i++)
            {
                Vector2 temp = GetPosLastNumberInCol(i);
                number.transform.position = new Vector3(i, temp.y-1,0);
                number.id = listNumberVitual[listNumberLastInCol[j]].id;
                number.ma += 100;
                matrixVitual[i,(int)temp.y - 1] = 1;
                listNumberVitual.Add(number);
                CheckNumber(number);
                ListTemp.Clear();
                if (max <= countMerge)
                {
                    max = countMerge;
                }
                CreateMatrixVitual();
                countMerge = 0;
            }
            listCountMerge.Add(max);
            max = 0;
        }
        CheckNumberWillSpawn();
        listCountMerge.Clear();
    }
    private void CheckNumberWillSpawn()
    {
        int temp = 0;
        int max = listCountMerge[0];
       
        for (int i = 0 ; i < listCountMerge.Count; i++)
        {
            if(max <= listCountMerge[i])
            {
                max = listCountMerge[i];
                temp = i;
            }
        }
        Debug.Log("this max " + max);
        Debug.Log("this listCountMerge " + listCountMerge.Count);
        Debug.Log("this temp" + temp);
        ManagerNumber.instance.ran = listNumberVitual[listNumberLastInCol[temp]].id;
        if (ManagerNumber.instance.ran > lastRan)
        {
            ManagerNumber.instance.ran = Random.Range(firstRan, lastRan);
        }
        Debug.Log("This ran " + ManagerNumber.instance.ran);
    }
    private Vector2 GetPosLastNumberInCol( int  column)
    {
        for (int y = 0; y < GridManager.instance.hight; y++)
        {
            if (matrixVitual[column, y] == 1)
            {
                return new Vector2(column, y);
            }
        }
        return new Vector2(column,GridManager.instance.hight);
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
    private void UpdateNumberBlock(ManagerNumber managerNumber, int idBlock )
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

    private void GetElementLastInCol()
    {
        listNumberLastInCol.Clear();
        for (int x = 0; x < GridManager.instance.width; x++)
        {
            for (int y = 0; y < GridManager.instance.hight; y++)
            {
                if ( matrixVitual[x,y] == 1)
                {
                    listNumberLastInCol.Add(GetIndexInList(x, y));
                    break;
                }
            }
        }
    }
    private int GetIndexInList(int x, int y)
    {
        for (int i = 0; i < listNumberVitual.Count; i++)
        {
            Vector3 a = new Vector3(x, y, 0);
            if (listNumberVitual[i].transform.position == a)
            {
                return i;
            }
        }
        return -1;
    }

    private void CheckNumber(Number number )
    {
        CheckNumbersMix(number);
        if (listIndex.Count > 0)
        {
            UpdateNode(number);
            DeleteAllNodeMix();
            UpdateListNumber();
            listIndex.Clear();
        }
    }
    IEnumerator Test(Number number)
    {
        yield return new WaitForSeconds(1);
        UpdateNode(number);
    }
    private void CheckNumbersMix(Number number)
    {
        for (int i = listNumberVitual.Count - 1; i >= 0; i--)
        {
            if (Vector2.Distance(number.transform.position, listNumberVitual[i].transform.position) <= 1.1f
                && number.id == listNumberVitual[i].id && listNumberVitual.Count > 1 && number.ma != listNumberVitual[i].ma)
            {
                listIndex.Add(i);
                UpdateMatrix(number, listNumberVitual[i]);
                CheckUpdateNode1 = true;
            }
        }
    }
    private void UpdateMatrix(Number number, Number list)
    {
        temp1 = (int)number.transform.position.x;
        temp2 = (int)number.transform.position.y;

        matrixVitual[temp1, temp2] = 0;
        int x = (int)list.transform.position.x;
        int y = (int)list.transform.position.y;
        matrixVitual[x, y] = 0;
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
    private void UpdateListNumber()
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
        CheckListTemp();
    }
    private void CheckListTemp()
    {
        if (ListTemp.Count != 0)
        {
            for (int i = 0; i < ListTemp.Count; i++)
            {
                checkAdd = true;
                CheckNumber(ListTemp[i]);
            }
            ListTemp.Clear();
        }
    }
    private void DeleteAllNodeMix()
    {
        for (int i = 0; i < listIndex.Count; i++)
        {
            listNumberVitual.RemoveAt(listIndex[i]);
        }
    }
    private void UpdateNode(Number number)
    {
        countMerge += listIndex.Count;
        if (CheckUpdateNode1 == true)
        {
            int count = listIndex.Count;
            number.id += count;
            CheckUpdateNode1 = false;
        }
    }
    private void UpdateNodeInColumn(int column, int row1)
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
                        listNumsInCol[0].transform.position = new Vector3(column, i,0);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
    }
    private void GetElementsInColumn(float x, float y)
    {
        listNumsInCol.Clear();
        for (int i = 0; i < listNumberVitual.Count; i++)
        {
            if (listNumberVitual[i].transform.position.x == x && listNumberVitual[i].transform.position.y <= y)
            {
                listNumsInCol.Add(listNumberVitual[i]);
            }
        }

    }
}
