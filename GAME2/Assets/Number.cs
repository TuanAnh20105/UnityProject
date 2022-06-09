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
    private int temp;
    public bool check = false,checkDelete = false;
    int y, x;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
        grid = FindObjectOfType<GridManager>();
        managerNumber = FindObjectOfType<ManagerNumber>();

    }
    public void Check()
    {      
        for(int i = managerNumber.list.Count - 2; i >= 0;i--)
        {
            if (Vector2.Distance(managerNumber.number.transform.position, managerNumber.list[i].transform.position) <= 1.4f 
                && managerNumber.number.id == managerNumber.list[i].id && managerNumber.list.Count>1)
            {
                CheckNodeMix(i);
                CheckDirectNodeMix(i);
                RemoveNodeMix(i);
                
                CheckUpdateNode();
                return;
            }
        }

        
    }
    public void CheckNodeMix(int i)
    {
        this.temp = i;
        managerNumber.number.spriteRender.sprite = managerNumber.listSprite[id];
        managerNumber.number.id = managerNumber.list[i].id + 1;
        managerNumber.number.transform.name = Mathf.Pow(2, managerNumber.list[i].id + 1).ToString();
        grid.matrix[managerGame.temp1, managerGame.temp2] = 0;
    }
    public void CheckDirectNodeMix(int i)
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
    public void RemoveNodeMix(int i)
    {
        managerNumber.list.RemoveAt(i);
        Destroy(managerNumber.listObject[i]);
        managerNumber.listObject.RemoveAt(i);
    }
    public void CheckUpdateNode()
    {
        managerGame.CheckColoume();
        if (x == managerGame.temp1 - 1 || x == managerGame.temp1 + 1)
        {
            //grid.matrix[(int)managerNumber.number.transform.position.x, (int)managerNumber.number.transform.position.y] = 1;
            managerGame.GetElementsInColumn(x);
            managerGame.checkPos(x);
            //managerGame.CheckMixColumn();
        }

        
        
    }
}
