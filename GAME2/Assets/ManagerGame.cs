using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit2D hitInformation;
     public int a = 0;
    public bool checkForce = false,check = false;
    GridManager grid;
   public int temp1;
   public int temp2;
   public bool checkSpawn = true;
   
    public ManagerNumber managerNumber;
    bool checkClickInGame = true;
    bool checkClickFunction1 = false;
    bool checkClickFunction2 = false;
    public List<Number> listSwap = new List<Number>();
    public List<Number> listNumsInCol = new List<Number>();
    Number temp= new Number();
    void Start()
    {
        grid = GridManager.instance;
        temp = FindObjectOfType<Number>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (checkSpawn == true)
        {
            managerNumber.Spawn();
            checkSpawn = false;
        }
        if(checkClickInGame ==true)
        {
            TouchWorld();
        }
        if(checkClickFunction1 == true)
        {
            DestoyNumber();
        }
        if(checkClickFunction2 == true)
        {
            SwapNumber();
        }
    }
    
    public void CheckColoume()
    {
        for(int i = grid.hight-1; i >=0 ; i--)
        {
            if(grid.matrix[temp1,i] == 0)
            {
                managerNumber.number.transform.position = new Vector2(temp1, i);
                checkSpawn = true;
                grid.matrix[temp1, i] = 1;
                temp2 = i;
                managerNumber.number.Check();
                break;
            }
        }
    }
    public void checkPos(int column)
    {
        for (int j = 0; j < listNumsInCol.Count; j++)
        {
            for (int i = grid.hight - 1; i >= 0; i--)
            {
                if (grid.matrix[column, i] == 0)
                {
                    int row = (int)listNumsInCol[j].transform.position.x;
                    int column1 = (int)listNumsInCol[j].transform.position.y;
                    listNumsInCol[j].transform.position = new Vector2(column, i);
                    grid.matrix[row, column1] = 0;
                    checkSpawn = true;
                    grid.matrix[column, i] = 1;
                    temp2 = i;
                    //managerNumber.number.Check();
                    break;
                }
            }
        }
    }
    public void GetElementsInColumn(float x)
    {
        listNumsInCol.Clear();
        for(int i = 0; i < managerNumber.list.Count; i++)
        {
            if (managerNumber.list[i].transform.position.x == x)
            {
                listNumsInCol.Add(managerNumber.list[i]);
            }
        }       
    }
    public void TouchWorld()
    {      
        if (Input.GetMouseButtonDown(0))
        {
            managerNumber.number.check = false;
            var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchWorld.z = 0;
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
            if (hitInformation.collider != null )
            {
                int x = a /GridManager.instance.hight;
                for(int i = 0; i < GridManager.instance.width; i++)
                {
                    if(x == i)
                    {
                        managerNumber.number.transform.position = new Vector2(i, 0);
                        temp1 = i;
                    }
                }
                CheckColoume();
            }
        }
    }
    public void DestoyNumber()
    {
        checkClickInGame = false;
        checkClickFunction1 = true;
        if (Input.GetMouseButtonDown(0))
        {
            var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchWorld.z = 0;
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
            if (hitInformation.collider.tag== "Number")
            {
                for(int i = 0; i < managerNumber.list.Count;i++)
                {
                    if (Vector2.Distance(managerNumber.list[i].transform.position, hitInformation.transform.position) <= 0.3f)
                    {
                        int x = (int)managerNumber.list[i].transform.position.x;
                        int y = (int)managerNumber.list[i].transform.position.y;
                        grid.matrix[x, y] = 0;
                        Destroy(managerNumber.list[i]);
                        managerNumber.list.RemoveAt(i);
                        Destroy(managerNumber.listObject[i]);
                        managerNumber.listObject.RemoveAt(i);
                        temp1 = x;
                        temp2 = y;
                        GetElementsInColumn(x);        
                        checkPos(x);
                        checkClickInGame = true;
                        checkClickFunction1 = false;
                    }
                }
            }
        }
    }
    public void SwapNumber()
    {
        checkClickInGame = false;
        checkClickFunction2 = true;       
            if (Input.GetMouseButtonDown(0))
            {
                var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchWorld.z = 0;
                hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
                
                    if (hitInformation.collider.tag == "Number")
                    {                    
                        for (int i = 0; i < managerNumber.list.Count; i++)
                        {                            
                            if (Vector2.Distance(managerNumber.list[i].transform.position, hitInformation.transform.position) <= 0.5f)
                            {
                                if(listSwap.Count ==0)
                                {
                                    listSwap.Add(managerNumber.list[i]);
                                }
                                if(listSwap.Count!=0)
                                {
                                    if(managerNumber.list[i]!=listSwap[listSwap.Count-1])
                                    {
                                        listSwap.Add(managerNumber.list[i]);
                                    }
                                }
                                break;
                            }                           
                        }
                    }                                    
                if(listSwap.Count == 2)
                {
                    var a = Instantiate(managerNumber.listNumber[1]);
                    temp = a.GetComponent<Number>();
                    temp.spriteRender.sprite = listSwap[0].spriteRender.sprite;
                    temp.id = listSwap[0].id;
                    temp.ma = listSwap[0].ma;// luu thg 0 ra thg khac
                    listSwap[0].spriteRender.sprite = listSwap[1].spriteRender.sprite;
                    listSwap[0].id = listSwap[1].id;
                    listSwap[0].ma = listSwap[1].ma;
                    listSwap[1].spriteRender.sprite = temp.spriteRender.sprite;
                    listSwap[1].id = temp.id;
                    listSwap[1].ma = temp.ma;
                    Destroy(temp);
                    Destroy(a);
                    listSwap.Clear();    
                    checkClickInGame = true;
                    checkClickFunction2 = false;
                }
            }


    }

}
