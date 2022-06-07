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
    GridManager grid;
    GameObject a;
    public bool check = false;

    void Start()
    {
        managerGame = FindObjectOfType<ManagerGame>();
        grid = FindObjectOfType<GridManager>();
        managerNumber = FindObjectOfType<ManagerNumber>();
    }
    public void Check()
    {
        for(int i = 0; i < managerNumber.list.Count-1;i++)
        {
            if (Vector2.Distance(managerNumber.number.transform.position, managerNumber.list[i].transform.position) <= 1.4f)
            {
                if (managerNumber.number.id == managerNumber.list[i].id)
                {
                    managerNumber.list[i].spriteRender.sprite = managerNumber.listSprite[id + 1];
                    managerGame.checkForce = false;
                    managerNumber.list[i].id = managerNumber.number.id + 1;
                    managerNumber.list[i].transform.name = Mathf.Pow(2, managerNumber.list[i].id + 1).ToString();
                    Destroy(managerNumber.obj);
                    managerNumber.list.RemoveAt(i);

                }
            }
        }
        managerGame.checkSpawn = true;
    }
    private void Update()
    {
        if(check ==true)
        {
            Check();
            check = false;
        }
    }

    // Update is called once per frame
    //public void Check()
    //{        
    //    if(managerNumber.list.Count > 1)
    //    {
    //        // kiem tra thg vua tao ra voi cac thang con lai
    //        for (int i = 0; i < managerNumber.list.Count; i++)
    //        {
    //            if (Vector2.Distance(managerNumber.number.transform.position, managerNumber.list[i].transform.position) <= 1.4f)
    //            {
    //                if (managerNumber.number.id == managerNumber.list[i].id)
    //                {
    //                    int x = managerNumber.number.id + 1 ;
    //                    GameObject a = Instantiate(managerNumber.listNumber[x], managerNumber.list[i].transform.position, managerNumber.list[i].transform.rotation);
    //                    Number number = a.GetComponent<Number>();
    //                    number.transform.position = a.transform.position;
    //                    number.id = x;
    //                    managerNumber.list.Add(number);
    //                    managerNumber.listObject.Add(a);
    //                    managerNumber.list.RemoveAt(i);
    //                    managerNumber.listObject.RemoveAt(i);
    //                    managerNumber.listObject.RemoveAt(managerNumber.listObject.Count - 1);
    //                    Destroy(managerNumber.listObject[i]);
    //                    //Destroy(managerNumber.number);
    //                    Destroy(managerNumber.obj);

    //                    break;
    //                }

    //            }       
    //        }
    //        //managerGame.checkSpawn = true;
    //    }

    //}

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
        //if(other.gameObject.tag == "Number")
        
        managerGame.checkForce = false;
        //managerGame.checkSpawn = true;
        check = true;
        SetPosNumber();
        //managerNumber.list.Add(managerNumber.number);

    }
}
