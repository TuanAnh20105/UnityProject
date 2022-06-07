using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    // Start is called before the first frame update
    ManagerNumber managerNumber;
    ManagerGame managerGame;
    public int id;
    GridManager grid;
    bool check = false;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
        grid = FindObjectOfType<GridManager>();
        managerNumber = FindObjectOfType<ManagerNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        if(check == true)
        {
            Check();
            check = false;
        }
    }
    public void Check()
    {         
        //if(managerNumber.list.Count >2)
        {
            for (int i = 0; i < managerNumber.list.Count - 1; i++)
            {
                if (Vector2.Distance(transform.position, managerNumber.list[i].transform.position) <= 1.4f)
                {
                    if (id == managerNumber.list[i].id)
                    {
                        id++;
                        GameObject a = Instantiate(managerNumber.listNumber[id], managerNumber.list[i].transform.position, managerNumber.list[i].transform.rotation);
                        Number num = a.GetComponent<Number>();
                        managerNumber.list.RemoveAt(i);
                        managerNumber.list.RemoveAt(managerNumber.list.Count - 1);
                        managerNumber.list.Add(num);
                        Destroy(gameObject);
                        Destroy(managerNumber.obj);
                    }
                break;
                }       
            }
        }
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        managerGame.checkForce = false;
        managerGame.checkSpawn = true;
        SetPosNumber();
        if(other.gameObject.tag == "Number")
        {
            check = true;
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
   
    }
}
