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
    int temp1;
   public bool checkSpawn = true;
    public int count = 0;
    public ManagerNumber managerNumber;


    void Start()
    {
        grid = GridManager.instance;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (checkSpawn == true)
        {
            managerNumber.Spawn();
            checkSpawn = false;
        }
        if (checkForce == true)
        {
            AddForce();
        }
        TouchWorld();
    }
    public void AddForce()
    {
        managerNumber.number.transform.position = Vector2.MoveTowards(managerNumber.number.transform.position, grid.listTiles[(temp1 + 1) * grid.hight - 1], Time.deltaTime * 8f);
        if (Vector2.Distance(managerNumber.number.transform.position, grid.listTiles[(temp1 + 1) * grid.hight - 1]) == 0)
        {
            checkForce = false;
            checkSpawn = true;
        }
    }
    public void TouchWorld()
    {
         
        if (Input.GetMouseButtonDown(0))
        {
            var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchWorld.z = 0;
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                int x = a /GridManager.instance.hight;
                for(int i = 0; i < GridManager.instance.width; i++)
                {
                    if(x == i)
                    {
                        managerNumber.number.transform.position = new Vector2(i, 0);
                        temp1 = i;
                        checkForce = true;
                    }
                }
            }
        }
    }
}
