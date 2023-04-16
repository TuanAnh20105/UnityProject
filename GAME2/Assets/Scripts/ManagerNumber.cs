using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ManagerNumber : MonoBehaviour
{
    public List<GameObject> listObject;
    public List<Sprite> listSprite = new List<Sprite>();
    public List<Number> list;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();
    public List<Number> ListTemp = new List<Number>();
    List<int> listIndex = new List<int>();
    List<int> listColumn = new List<int>();
    List<int> listRow = new List<int>();
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
    private int[,] matrixVisit = new int[10, 10];
    [SerializeField] bool check = false;
    [SerializeField] private bool checkUpdatePos = false;
    private bool duplicateCol = false;
    private bool checkUpdateList = true;
    public bool DoingMerge = false;
    public int z = 0;
    public bool doneMerge = false;
    private void Awake()
    {
        instance = this;
    }
    public void CreateMatrixVisit()
    {
        for (int i = 0; i < GridManager.instance.width; i++)
        {
            for (int j = 0; j < GridManager.instance.hight; j++)
            {
                matrixVisit[i, j] = 0;
            }
        }
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
    public enum State
    {
        Find , UpdateNode , DelelteNode , UpdateListNode , Spawn , checkListTemp,
    }
    private State state;
    public void SetState( State state)
    {
        this.state = state;
    }
    public void Handle(Number number , PlayerController player)
    {
        Debug.Log(state);
        switch (state)
        {
            case State.Find:
                break;
            case State.UpdateNode:
                UpdateNode(number,player);
                break;

            case State.DelelteNode:
                DeleteAllNodeMix(player);
                break;
            case State.UpdateListNode:
                UpdateListNumber(player);
                break;
            case State.checkListTemp:
                CheckListTemp(player);
                break;
        }
    }
    public void Spawn()
    {
        CreateMatrixVisit();
        //algorithm.AlgorithmNumber(this, player);
        ran = Random.Range(1, 3);
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
        ListTemp.Clear();
        checkUpdateList = true;
    }
    public void CheckNumber(Number number ,PlayerController player)
    {
        CheckNumbersMix(number);
        if (listIndex.Count > 0)
        {
            for(int i = 0; i < listIndex.Count;i++)
            {
                UpdateMatrix(number, list[listIndex[i]]);
            }
            SetState(State.UpdateNode);
            Handle(number,player);
        }
        else
        {
            find = false;
        }
    }
    public void CheckNumbersMix(Number number)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (Vector2.Distance(number.transform.position, list[i].transform.position) <= 1f
                && number.id == list[i].id && list.Count > 1 && number.ma != list[i].ma)
            {
                if (matrixVisit[(int)list[i].transform.position.x, (int)list[i].transform.position.y] == 0)
                {
                    find = true;
                    listIndex.Add(i);
                    CheckUpdateNode1 = true;
                }
            }
        }
    }
    public void UpdateMatrix(Number number, Number list) 
    {
        duplicateCol = false;
        temp1 = (int)number.transform.position.x;
        temp2 = (int)number.transform.position.y;
        GridManager.instance.matrix[temp1, temp2] = 0;
        int x = (int)list.transform.position.x;
        int y = (int)list.transform.position.y;
        GridManager.instance.matrix[x, y] = 0;
        if (listColumn.Count == 0)
        {
            if (x != temp1)
            {
                listColumn.Add(x);
                listRow.Add(temp2);
                listColumn.Add(temp1);
                listRow.Add(y);
            }
            else
            {
                if (y < temp2)
                {
                    listColumn.Add(temp1);
                    listRow.Add(temp2);
                }
                else
                {
                    listColumn.Add(x);
                    listRow.Add(y);
                }
            }
        }
        else
        {
            for (int i = 0; i < listColumn.Count; i++)
            {
                if (listColumn[i] == x)
                {
                    duplicateCol = true;
                    if (listRow[i] < y)
                    {
                        listRow.RemoveAt(i);
                        listRow.Add(y);
                    }
                    break;
                }
            }

            if (duplicateCol == false)
            {
                listColumn.Add(x);
                listRow.Add(y);
            }
        }
        if (matrixVisit != null)
        {
            matrixVisit[temp1, temp2] = 1;
            matrixVisit[x, y] = 1;
        }
    }
    public void UpdateListNumber(PlayerController player)
    {
        for( int i = 0; i < listColumn.Count;i++)
        {            
            GetElementsInColumn(listColumn[i],listRow[i]);
            for(int j = 0; j < listNumsInCol.Count;j++)
            {
                UpdateNodeInColumn(listColumn[i], listRow[i],player);
            }    
        }
        listIndex.Clear();
        listColumn.Clear();
        listRow.Clear();

    }
    public void CheckListTemp(PlayerController player)
    {
        if (DoingMerge == true)
        {
            DoingMerge = false;
            z = 0;
            if (ListTemp.Count != 0)
            {
                for (int i = 0; i < ListTemp.Count; i++)
                {
                    checkAdd = true;
                    CheckNumber(ListTemp[i], player);
                    if (find == true)
                    {
                        i = 0;
                    }
                }
                //ListTemp.Clear();
              
            }
            
                find = false;
                SetState(State.Find);
                doneMerge = true;
            
           
        }
    }
    public void DeleteAllNodeMix(PlayerController player)
    {      

        for(int i = 0; i < listIndex.Count; i ++)
        {
            list.RemoveAt(listIndex[i]);
            Destroy(listObject[listIndex[i]]);
            listObject.RemoveAt(listIndex[i]);
        }
        SetState(State.UpdateListNode);
        Handle(number,player);
    }
    public void UpdateNode(Number number,PlayerController player)
    {
            //bool doulicateListTemp = false;
            //for (int i = 0; i < ListTemp.Count; i++)
            //{
            //    if (number.ma == ListTemp[i].ma)
            //    {
            //        doulicateListTemp = true;
            //        break;
            //    }
            //}

            //if (doulicateListTemp == false)
            //{
            //    ListTemp.Add(number);
            //}
        
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
            SetState(State.DelelteNode);
            Handle(number,player);
        }
    }
    public void UpdateNodeInColumn(int column, int row1 , PlayerController player)
    {
        bool dublicate = false;

        {
            int x = listNumsInCol.Count;
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
                            if (ListTemp.Count == 0)
                            {
                                ListTemp.Add(listNumsInCol[0]);
                            }
                            else
                            {
                                for (int j = 0; j < ListTemp.Count; j++)
                                {
                                    if (listNumsInCol[0].ma == ListTemp[j].ma)
                                    {
                                        dublicate = true;
                                        break;
                                    }
                                }
                                if (dublicate == false)
                                {
                                    ListTemp.Add(listNumsInCol[0]);
                                }
                            }
                            GridManager.instance.matrix[row, column1] = 0;
                            GridManager.instance.matrix[column, i] = listNumsInCol[0].id;
                            Vector3 temp = new Vector3(column, i, 0);
                            if (listNumsInCol[0].transform.position != temp)
                            {
                                player.SetState(PlayerController.State.doingMerge);
                                listNumsInCol[0].transform.DOMove(temp, .3f).SetEase(Ease.Flash).OnComplete(() =>
                                {
                                    z++;
                                    if (z == x)
                                    {
                                        DoingMerge = true;
                                        z = 0;
                                    }
                                });
                            }
                            //listNumsInCol[0].transform.position = new Vector2(column, i);
                        }
                        listNumsInCol.RemoveAt(0);
                        break;
                    }
                }
            }
            CreateMatrixVisit();
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
            CheckNumber(list[i],player);
        }
    }
    public void UpdateNumberBlock(int idBlock , PlayerController player)
    {
        for(int i = 0; i < list.Count;i++)
        {
            if(list[i].id == idBlock)
            {
                list[i].SetSpriteNumber(listSprite[list[i].id]);
                list[i].id += 1;
                list[i].SetTxtNumber(Mathf.Pow(2, list[i].id).ToString());
                list[i].transform.name = Mathf.Pow(2, list[i].id).ToString();
                CheckNumber(list[i],player);
            }
        }
    }
}
