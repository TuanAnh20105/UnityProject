using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    private Transform[] points;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame

    public void SetUpLine(Transform[] list)
    {
        lr.positionCount = list.Length;
        points = list;
    }
}
