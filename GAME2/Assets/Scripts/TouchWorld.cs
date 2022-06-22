using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWorld 
{
    // Start is called before the first frame update
    public RaycastHit2D hitInformation;
    public int a = 0;
    public int x = 0;
    public int temp1;
    public Collider2D Touch()
    {             
        var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchWorld.z = 0;
        hitInformation = Physics2D.Raycast(touchWorld, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {
            x = a / GridManager.instance.hight;
        }
        return hitInformation.collider;
    }
}
