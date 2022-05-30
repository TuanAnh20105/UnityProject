using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDragAndDrop : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public GameObject moveObject;
    public static ManagerDragAndDrop Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveObject != null && Input.mousePosition!= null)
        {
            var touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 0;
            moveObject.transform.position = touchPos;
        }
    }
}
