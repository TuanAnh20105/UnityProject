using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 girdWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    Node[,] grid;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(girdWorldSize.x, girdWorldSize.y));
    }
    private void Start() {
        
    }
}
