using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ManagerNumber : MonoBehaviour
{
    //public List<GameObject> listNumber;
    public List<GameObject> listObject;
    public List<Sprite> listSprite = new List<Sprite>();
    public List<Number> list;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();
    public List<Number> ListTemp = new List<Number>();
    List<int> listIndex = new List<int>();
    List<int> listColumn = new List<int>();
    List<int> listRow = new List<int>();
    public List<int> listSpawn = new List<int>();
    public List<Number> listDestroy = new List<Number>();
    bool checkAdd = true;
    public Number temp;
    public GameObject spawnPos;
    public GameObject numberSample;
    public GameObject obj;
    public Number number;
    public Text numberSamp;
    public bool find = false;
    int t = 0;
    bool CheckUpdateNode1 = false;
    public static ManagerNumber instance;
    public int temp1, temp2;
    public int ran;
    public Image imageSpawn;
    public Algorithm algorithm;
    public int idTemp = 0;
    [TextArea(5,7)]
    public string test= "";
    bool check = false;
    private void Awake()
    {
        instance = this;
    }
    public Number GetNumberWithPos(int x , int y)
    {
        Vector3 temp = new Vector3(x, y,0);
        for(int i = 0; i < list.Count;i++)
        {
            if(list[i].transform.position == temp)
            {
                return list[i];
            }
        }
        return null;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Spawn()
    {
        if (list.Count > 2)
        {
            algorithm.Handle();
        }
        else
        {
            algorithm.AlgorithmNumber(this);
        }
        obj = Instantiate(numberSample, spawnPos.transform.position, spawnPos.transform.rotation);
        number = obj.GetComponent<Number>();
        number.SetTxtNumber(Mathf.Pow(2, ran).ToString());
        number.SetSpriteNumber(listSprite[ran-1]);
        number.transform.position = obj.transform.position;
        number.transform.name = Mathf.Pow(2, ran).ToString();
        number.ma = t;
        numberSamp.text = number.txtNumber.text.ToString();
        imageSpawn.sprite = number.sprite;
        t++;
        listObject.Add(obj);
        list.Add(number);
        number.id = ran;
        if(idTemp <ran)
        {
            idTemp = ran;
        }
    }
    public void CheckNumber(Number number )
    {
        CheckNumbersMix(number);
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
    public void CheckNumbersMix(Number number)
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
        if(temp1 != x)
        {
            if(checkAdd == true)
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
        for( int i = 0; i < listColumn.Count;i++)
        {            
            GetElementsInColumn(listColumn[i],listRow[i]);
            for(int j = 0; j < listNumsInCol.Count;j++)
            {
                UpdateNodeInColumn(listColumn[i], listRow[i]);
            }    
        }
        listIndex.Clear();
        listColumn.Clear();
        listRow.Clear();
        CheckListTemp();
    }
    public void CheckListTemp()
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
    public void DeleteAllNodeMix()
    {        
        for(int i = 0; i < listIndex.Count; i ++)
        {
            //number.CheckDirectEffect(list[listIndex[i]]);
            list.RemoveAt(listIndex[i]);
            Destroy(listObject[listIndex[i]]);
            listObject.RemoveAt(listIndex[i]);
        }
    }
    public void UpdateNode(Number number)
    {
        
        if (CheckUpdateNode1 == true)
        {
            int count = listIndex.Count;
            
            number.id += count;
            if(idTemp < number.id)
            {
                idTemp = number.id;
            }
            number.SetSpriteNumber(listSprite[ number.id - 1]);
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
                if (GridManager.instance.matrix[column, i] == 0)
                {
                    int row = (int)listNumsInCol[0].transform.position.x;
                    int column1 = (int)listNumsInCol[0].transform.position.y;
                    if (column1 <= row1)
                    {
                        ListTemp.Add(listNumsInCol[0]);
                        GridManager.instance.matrix[row, column1] = 0;
                        GridManager.instance.matrix[column, i] = listNumsInCol[0].id;
                        //listNumsInCol[0].transform.DOMove(new Vector2(column, i), 0.2f).SetEase(Ease.Flash).OnComplete(() =>
                        //{
                        //    //CheckNumber(listNumsInCol[0]);
                        //});
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
            if (list[i].transform.position.x == x  && list[i].transform.position.y <= y)
            {
                listNumsInCol.Add(list[i]);
            }
        }

    }
    public void CheckPosDestroy(int column, Number number)
    {
        Debug.Log(number.transform.name);
        ListTemp.Clear();
        while (listNumsInCol.Count != 0)
        {
            for (int i = GridManager.instance.hight - 1; i >= 0; i--)
            {
                if (GridManager.instance.matrix[column, i] == 0)
                {
                    int row = (int)listNumsInCol[0].transform.position.x;
                    int column1 = (int)listNumsInCol[0].transform.position.y;
                    if (column1 < number.transform.position.y && column1 != 0)
                    {
                        listNumsInCol[0].transform.position = new Vector2(column, i);
                        GridManager.instance.matrix[row, column1] = 0;
                        GridManager.instance.matrix[column, i] = listNumsInCol[0].id;
                        ListTemp.Add(listNumsInCol[0]);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
    }
    public int GetIndexInList(int x , int y, List<Number> list)
    {
        for(int i = 0; i < list.Count;i++)
        {
            Vector3 a = new Vector3(x, y,0);
            if (list[i].transform.position == a)
            {
                return i;
            }
        }
        return -1;
    }
    public void CheckAferDestroy(List<Number> list, PlayerController player)
    {
        for (int i = 0; i < list.Count; i++)
        {
            CheckNumber(list[i]);
        }
    }
    
    public void UpdateNumberBlock(int idBlock)
    {
        for(int i = 0; i < list.Count;i++)
        {
            if(list[i].id == idBlock)
            {
                list[i].SetSpriteNumber(listSprite[list[i].id]);
                list[i].id += 1;
                list[i].SetTxtNumber(Mathf.Pow(2, list[i].id).ToString());
                list[i].transform.name = Mathf.Pow(2, list[i].id).ToString();
                CheckNumber(list[i]);
            }
        }
    }
}
