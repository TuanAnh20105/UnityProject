using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] listTrans;
    public LineRenderer line;
    int i = 0;
    RaycastHit2D hitInformation;
    ManagerDragAndDrop manager;
    void Start()
    {
        manager = FindObjectOfType<ManagerDragAndDrop>();
        SetUpLine(listTrans[0].position, listTrans[1].position);
    }
    void Update()
    {
        var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchWorld.z = 0;
        if(Input.GetMouseButton(0))
        {
            SetUpLine(listTrans[0].position, listTrans[1].position);
            hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
            if(hitInformation.collider!=null)
            {
                manager.moveObject = hitInformation.transform.gameObject;              
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Da vao day");
            if(hitInformation.collider.name == "Point")
            {
                  
            }
        }
 
    }
    public void SetUpLine(Vector2 start, Vector2 end)
    {
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
}
