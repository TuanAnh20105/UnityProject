using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apathding : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    bool checkMove = false, checkMoveToPlayer = false,checkMoveRight;
    public Transform castPoint;
    RaycastHit2D hit;
        RaycastHit2D hit1;
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {


        if (checkMove == true)
        {
            checkMoveRight = false;
            transform.position = Vector2.MoveTowards(transform.position, hit.transform.position, Time.deltaTime * 2f);
        }
        if (CanSeePlayer(5))
        {
            checkMove = true;
        }
        else
        {
            checkMove = false;
        }

    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        Vector2 endPos = castPoint.position + Vector3.down * castDist;

        hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }

            else
            {
                val = false;

            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return val;
    }

}
