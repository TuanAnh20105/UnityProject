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
    int temp;
    public bool check = false;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
        grid = FindObjectOfType<GridManager>();
        managerNumber = FindObjectOfType<ManagerNumber>();

    }
    public void Check()
    {
        if(managerNumber.list.Count > 1)
        {
            for(int i = managerNumber.list.Count - 2; i >= 0;i--)
            {
                if (Vector2.Distance(managerNumber.number.transform.position, managerNumber.list[i].transform.position) <= 1.4f 
                    && managerNumber.number.ma != managerNumber.list[i].ma && managerNumber.number.id == managerNumber.list[i].id)
                {
                    temp = i;
                    managerNumber.number.spriteRender.sprite = managerNumber.listSprite[id];
                    managerNumber.number.id = managerNumber.list[i].id  +1 ;
                    managerNumber.number.transform.name = Mathf.Pow(2, managerNumber.list[i].id + 1).ToString();
                    grid.matrix[managerGame.temp1, managerGame.temp2] = 0;
                    float x = managerNumber.list[i].transform.position.x;    
                    float y  = managerNumber.list[i].transform.position.y;                          
                    if (y == managerGame.temp2 + 1)
                    {
                        grid.matrix[managerGame.temp1, managerGame.temp2 +1] = 0;
                    }
                    if(x == managerGame.temp1 - 1)
                    {
                        grid.matrix[managerGame.temp1-1, managerGame.temp2] = 0;
                    }         
                    if(x == managerGame.temp1+1)
                    {
                        grid.matrix[managerGame.temp1+1, managerGame.temp2] = 0;
                    }
                    managerNumber.list.RemoveAt(i);                  
                    Destroy(managerNumber.listObject[i]);
                    managerNumber.listObject.RemoveAt(i);                        
                    managerGame.CheckColoume();
                    managerGame.GetElementsInColumn(x);
                    managerGame.checkPos((int)x);
                   
                }
            }

        }
        //managerGame.checkSpawn = true;
    }
    private void Update()
    {

    }
    public void SetPosNumber()
    {
        for (int i = 0; i < grid.listTiles.Count; i++)
        {
            if (Vector2.Distance(transform.position, grid.listTiles[i]) < 0.3f)
            {
                transform.position = grid.listTiles[i];
                break;
            }
        }

    }
}
