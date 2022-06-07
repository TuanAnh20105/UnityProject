using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNumber : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    // Start is called before the first frame update
    public List<GameObject> listNumber;
    public List<Number> list;
    public GameObject temp;
    [HideInInspector] public Number number;
    public GameObject obj;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        int ran = Random.Range(0, listNumber.Count);
        obj = Instantiate(listNumber[ran], temp.transform.position, temp.transform.rotation);
        number = obj.GetComponent<Number>();
        list.Add(number);
        number.transform.position = obj.transform.position;
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
