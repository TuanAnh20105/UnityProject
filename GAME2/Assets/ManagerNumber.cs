using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNumber : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    // Start is called before the first frame update
    public List<GameObject> listNumber;
    public List<GameObject> listObject;
    public List<Number> list;
     public GameObject temp;
     public Number number;
     public GameObject obj;
    public List<Sprite> listSprite = new List<Sprite>();
    public void Spawn()
    {
        int ran = Random.Range(0, listNumber.Count);
        obj = Instantiate(listNumber[ran], temp.transform.position, temp.transform.rotation);
        listObject.Add(obj);
        number = obj.GetComponent<Number>();
        number.transform.position = obj.transform.position;
        list.Add(number);
        for (int i = 0; i < listNumber.Count; i++)
        {
            if (ran == i)
            {
                number.id = i;
                break;
            }
        }
    }
}
