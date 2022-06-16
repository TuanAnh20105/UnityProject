using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int t = 0;
    bool CheckUpdateNode1 = false;
    public static ManagerNumber instance;
    public int temp1, temp2;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();
    public List<Number> ListTemp = new List<Number>();
    public Number temp = new Number();
    List<int> listIndex = new List<int>();
    List<int> listColumn = new List<int>();
    List<int> listRow = new List<int>();
    public List<int> listSpawn = new List<int>();
    bool checkAdd = true;
    int ran;
    public Image imageSpawn;
    private void Awake()
    {
        instance = this;
    }
    public void Algorithm()
    {
        for( int x = 0; x < GridManager.instance.width; x++)
        {
            for(int y = 0; y < GridManager.instance.hight; y++)
            {
                if(GridManager.instance.matrix[x,y] == 1 )
                {
                   listSpawn.Add(GetNumberWithPos(x, y).id);
                    break;
                }

            }
        }
        listSpawn.Sort();
        if(listSpawn[listSpawn.Count - 1] > 5)
        {
            listSpawn[listSpawn.Count - 1] = listSpawn[listSpawn.Count - 1] - 2;
        }
        else
        {
            listSpawn[listSpawn.Count - 1] = listSpawn[listSpawn.Count - 1] - 1;
        }
        if(listSpawn[0] > 1)
        {
            listSpawn[0] -= 1;
        }
    }
    public Number GetNumberWithPos(int x , int y )
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
    public void Spawn()
    {
        if(list.Count >3)
        {
            listSpawn.Clear();
            Algorithm();
        }
        if(listSpawn.Count>1 && list.Count > GridManager.instance.hight * GridManager.instance.width /2)
        {
            ran = Random.Range(listSpawn[0], listSpawn[listSpawn.Count-1]);
        }
        else
        {
            ran = Random.Range(0, listNumber.Count - 4);
        }
        obj = Instantiate(listNumber[ran], spawnPos.transform.position, spawnPos.transform.rotation);
        number = obj.GetComponent<Number>();
        number.transform.position = obj.transform.position;
        number.ma = t;
        imageSpawn.sprite = number.spriteRender.sprite;
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
    public void Check(Number number)
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
            }
        } 
        if (listIndex.Count > 0)
        {
            UpdateNode(number);
            DeleteAllNodeMix();
            UpdateListNumber();
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
                checkPos(listColumn[i], listRow[i]);
            }
        }
        listIndex.Clear();
        listColumn.Clear();
        listRow.Clear();
        if (ListTemp.Count != 0)
        {
            listIndex.Clear();
            for (int i = 0; i < ListTemp.Count; i++)
            {
                checkAdd = true;
                Check(ListTemp[i]);
            }
            ListTemp.Clear();
        }

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
            CanvasController.instance.SetScore(Mathf.Pow(2, number.id));
        }
        
    }    
    public void checkPos(int column, int row1)
    {
        //ListTemp.Clear();
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
}
