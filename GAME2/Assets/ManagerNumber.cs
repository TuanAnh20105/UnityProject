using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNumber : MonoBehaviour
{
    public List<GameObject> listNumber;
    public List<GameObject> listObject;
    public List<Sprite> listSprite = new List<Sprite>();
    public List<Number> list;
    public GameObject spawnPos;
    public Number number;
    public GameObject obj;
    public bool find = false;
    int t = 0, x = 0 , y = 0;
    bool CheckUpdateNode1 = false;
    public static ManagerNumber instance;
    public int temp1, temp2;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();
    public List<Number> ListTemp = new List<Number>();
    public Number temp = new Number();
    public List<int> listIndex = new List<int>();
    public List<int> listColumn = new List<int>();
    private void Awake()
    {
        instance = this;
    }
    public void Spawn()
    {
        int ran = Random.Range(0, listNumber.Count-4);
        obj = Instantiate(listNumber[ran], spawnPos.transform.position, spawnPos.transform.rotation);
        number = obj.GetComponent<Number>();
        number.transform.position = obj.transform.position;
        number.ma = t;
        t++;
        listObject.Add(obj);
        list.Add(number);
        for (int i = 0; i < listNumber.Count; i++)
        {
            if (ran == i)
            {
                number.id = i+1;            
                break;
            }       
        }
    }
    public void Check(Number number,PlayerController player)
    {
        for (int i = list.Count - 1; i >= 0; i--)// co 1 loi -1 o day 
        {
            if (Vector2.Distance(number.transform.position,list[i].transform.position) <= 1.1f
                && number.id == list[i].id && list.Count > 1 && number.ma !=list[i].ma)
            {
                find = true;
                listIndex.Add(i);
                UpdateMatrix(number, list[i]);
                CheckUpdateNode1 = true;
                //CheckNodeMix(number, i);
                //CheckDirectNodeMix(i);
                //RemoveNodeMix(i);
                //CheckUpdateNode(player,number);
                //return;
            }
        }
        
        if (listIndex.Count > 0)
        {
            UpdateNode(number);
            DeleteAllNodeMix();
            UpdateListNumber(number,player);   
        }
        else
        {
            find = false;
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
    }
    public void UpdateListNumber(Number number, PlayerController player)
    {
        for( int i = 0; i < listColumn.Count;i++)
        {            
            GetElementsInColumn(listColumn[i]);
            for(int j = 0; j < listNumsInCol.Count;j++)
            {
                checkPos(listColumn[i], number, player);
            }
        }
        listIndex.Clear();
        listColumn.Clear();
    }
    public void DeleteAllNodeMix()
    {
        for(int i = 0; i < listIndex.Count; i ++)
        {
            list.RemoveAt(listIndex[i]);
            Destroy(listObject[listIndex[i]]);
            listObject.RemoveAt(listIndex[i]);
        }
        
    }
    public void UpdateNode(Number number)
    {
        if(CheckUpdateNode1 == true)
        {
            int count = listIndex.Count;
            number.spriteRender.sprite = listSprite[count + number.id-1];
            number.id += count;
            number.transform.name = Mathf.Pow(2,number.id).ToString();
            CheckUpdateNode1 = false;
        }
        
    }    

    public void CheckNodeMix(Number number, int pos) // update node++
    {
        temp1 = (int)number.transform.position.x;
        temp2 = (int)number.transform.position.y;
        number.spriteRender.sprite = listSprite[number.id];
        number.id = list[pos].id + 1;
        number.transform.name = Mathf.Pow(2,list[pos].id + 1).ToString();
        GridManager.instance.matrix[temp1,temp2] = 0;
    }
    public void CheckDirectNodeMix(int pos) // update pos of node
    {
        x = (int)Mathf.Round(list[pos].transform.position.x);
        y = (int)Mathf.Round(list[pos].transform.position.y);
        if (y == temp2 + 1)
        {
            GridManager.instance.matrix[temp1, temp2 + 1] = 0;
        }
        else if (x == temp1 - 1)
        {
            GridManager.instance.matrix[temp1 - 1, temp2] = 0;
        }
        else if (x == temp1 + 1)
        {
            GridManager.instance.matrix[temp1 + 1,temp2] = 0;
        }
    }
    public void RemoveNodeMix(int i) // remove node mix
    {
        list.RemoveAt(i);
        Destroy(listObject[i]);
        listObject.RemoveAt(i);
    }
    public void CheckUpdateNode(PlayerController player , Number number) // check 
    {
        player.handlePush.CheckColoume(number, player); ;
        if (x == temp1 - 1 || x == temp1 + 1)
        {
            GetElementsInColumn(x);
            checkPos(x,number, player);
            CheckAferDestroy(ListTemp, player);
        }
    }
    public void checkPos(int column, Number number, PlayerController player)
    {
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
                        GridManager.instance.matrix[column, i] = 1;
                        ListTemp.Add(listNumsInCol[0]);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
        if (ListTemp.Count != 0)
        {
            Check(number,player);
        }
    }
    public void CheckPosDestroy(int column, Number number)
    {
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
                        GridManager.instance.matrix[column, i] = 1;
                        ListTemp.Add(listNumsInCol[0]);
                    }
                    listNumsInCol.RemoveAt(0);
                    break;
                }
            }
        }
    }
    public void CheckAferDestroy(List<Number> list, PlayerController player)
    {
        for (int i = 0; i < list.Count; i++)
        {
            player.handlePush.CheckColoume(list[i], player); ;
        }
    }
    public void GetElementsInColumn(float x)
    {
        listNumsInCol.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position.x == x && list[i].transform.position.y != 0)
            {
                listNumsInCol.Add(list[i]);
            }
        }

    }
}
