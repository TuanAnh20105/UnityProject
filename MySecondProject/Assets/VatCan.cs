using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VatCan : MonoBehaviour , IVatCan
{
    // Start is called before the first frame update
    public ManagerMove managerMove;
    public UnityEvent unityEvent = new UnityEvent();
    Vector2 Pos;

    public void SetEvent()
    {
        managerMove = FindObjectOfType<ManagerMove>();
        unityEvent.AddListener(managerMove.Find);
    }

    public void SetInvoke()
    {
        unityEvent.Invoke();
    }

    void Start()
    {
        Pos = transform.position;
        SetEvent();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        SetInvoke();
    }

}
