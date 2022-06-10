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
    Number number;
    int y, x;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
        grid = FindObjectOfType<GridManager>();
        managerNumber = FindObjectOfType<ManagerNumber>();

    }
    public void Check(Number number)
    {
        this.number = number;
        for(int i = managerNumber.list.Count - 1; i >= 0;i--)// co 1 loi -1 o day 
        {
            if (Vector2.Distance(number.transform.position, managerNumber.list[i].transform.position) <= 1.1f 
                && number.id == managerNumber.list[i].id && managerNumber.list.Count>1 && number.ma != managerNumber.list[i].ma)
            {
                CheckNodeMix(number,i);
                CheckDirectNodeMix(i);
                RemoveNodeMix(i);               
                CheckUpdateNode();
                return;
            }
        }        
    }

    public void CheckNodeMix( Number number ,int i) // update node++
    {
        managerGame.temp1 = (int)number.transform.position.x; 
        managerGame.temp2 = (int)number.transform.position.y; 
        number.spriteRender.sprite = managerNumber.listSprite[id];
        number.id = managerNumber.list[i].id + 1;
        number.transform.name = Mathf.Pow(2, managerNumber.list[i].id + 1).ToString();
        grid.matrix[managerGame.temp1, managerGame.temp2] = 0;
    }
    public void CheckDirectNodeMix(int i) // update pos of node
    {
        x = (int)Mathf.Round(managerNumber.list[i].transform.position.x);
        y = (int)Mathf.Round(managerNumber.list[i].transform.position.y);
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
    public void CheckUpdateNode() // check 
    {
        managerGame.CheckColoume(number);
        if (x == managerGame.temp1 - 1 || x == managerGame.temp1 + 1)
        {
            managerGame.GetElementsInColumn(x);
            managerGame.checkPos(x, managerNumber.number);
            managerGame.CheckAferDestroy(managerGame.ListTemp);
        }
    }
}
