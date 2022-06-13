using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    // Start is called before the first frame update
    ManagerNumber managerNumber;
    ManagerGame managerGame;
    public SpriteRenderer spriteRender;
    public int id;
    public int ma;
    GridManager grid;
    public bool check = false,checkDelete = false;
    public bool find = false;
    Number number;
    int y, x;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
        grid = FindObjectOfType<GridManager>();
        managerNumber = FindObjectOfType<ManagerNumber>();

    }
    public void Check(Number number,PlayerController player)
    {
        this.number = number;
        for(int i = managerNumber.list.Count - 1; i >= 0;i--)// co 1 loi -1 o day 
        {

            if (Vector2.Distance(number.transform.position, managerNumber.list[i].transform.position) <= 1.1f 
                && number.id == managerNumber.list[i].id && managerNumber.list.Count > 1 && number.ma != managerNumber.list[i].ma)
            {
                find = true;
                UpdatePos();
                CheckNumberMix(i);

                //CheckNodeMix(number,i);
                //CheckDirectNodeMix(i);
                //RemoveNodeMix(i);
                //CheckUpdateNode(player);
                //return;
            }
            else
            {
                find = false;
            }
        }
        if(find == true)
        {
            RemoveAllNumberMix();
            UpdateNumber(number);
            UpdatePosAfterMix(managerGame.listCulumnMix,player);
            managerGame.listCulumnMix.Clear();

        }
    }
    public void UpdatePos()
    {
        managerGame.temp1 = (int)number.transform.position.x;
        managerGame.temp2 = (int)number.transform.position.y;
        grid.matrix[managerGame.temp1, managerGame.temp2] = 0;
    }
    public void UpdateNumber(Number number)
    {
        int count = managerGame.listNumberMix.Count;
        number.spriteRender.sprite = managerNumber.listSprite[id- 1 + count];
        number.id = number.id + count;
        number.transform.name = Mathf.Pow(2, number.id -1 + count).ToString();
        managerGame.listNumberMix.Clear();
    }
    public void CheckNumberMix(int node)
    {
        managerGame.listNumberMix.Add(managerNumber.list[node]);
    }
    public void UpdatePosAfterMix(List<int> list, PlayerController player)
    {
        for(int i = 0; i < list.Count;i++)
        {
            managerGame.GetElementsInColumn(list[i]);
            managerGame.checkPos(list[i], managerNumber.number, player);
        }
    }
    public void RemoveAllNumberMix()
    {
        if (managerGame.listNumberMix.Count > 0)
        {
            for (int j = 0; j < managerGame.listNumberMix.Count; j++)
            {
                x = (int)Mathf.Round(managerGame.listNumberMix[j].transform.position.x);
                y = (int)Mathf.Round(managerGame.listNumberMix[j].transform.position.y);
                managerGame.listCulumnMix.Add(x);
                grid.matrix[x, y] = 0;
                managerNumber.list.RemoveAt(j);
                Destroy(managerNumber.listObject[j]);
                managerNumber.listObject.RemoveAt(j);
            }

        }
    }

    public void CheckNodeMix( Number number ,int node) // update node++
    {
        managerGame.temp1 = (int)number.transform.position.x; 
        managerGame.temp2 = (int)number.transform.position.y;
        number.spriteRender.sprite = managerNumber.listSprite[id];
        number.id = managerNumber.list[node].id + 1;
        number.transform.name = Mathf.Pow(2, managerNumber.list[node].id + 1).ToString();
        grid.matrix[managerGame.temp1, managerGame.temp2] = 0;
    }
    public void CheckDirectNodeMix(int node) // update pos of node
    {
        x = (int)Mathf.Round(managerNumber.list[node].transform.position.x);
        y = (int)Mathf.Round(managerNumber.list[node].transform.position.y);
        if (y == managerGame.temp2 + 1)
        {
            grid.matrix[managerGame.temp1, managerGame.temp2 + 1] = 0;
        }
        else if (x == managerGame.temp1 - 1)
        {
            grid.matrix[managerGame.temp1 - 1, managerGame.temp2] = 0;
        }
        else if (x == managerGame.temp1 + 1)
        {
            grid.matrix[managerGame.temp1 + 1, managerGame.temp2] = 0;
        }
    }
    public void RemoveNodeMix(int i) // remove node mix
    {
        managerNumber.list.RemoveAt(i);
        Destroy(managerNumber.listObject[i]);
        managerNumber.listObject.RemoveAt(i);
    }
    public void CheckUpdateNode(PlayerController player) // check 
    {
        player.handlePush.CheckColoume(number, player); ;
        if (x == managerGame.temp1 - 1 || x == managerGame.temp1 + 1)
        {
            managerGame.GetElementsInColumn(x);
            managerGame.checkPos(x, managerNumber.number,player);
            managerGame.CheckAferDestroy(managerGame.ListTemp,player);
        }
    }
}
