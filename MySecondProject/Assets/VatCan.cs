using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VatCan : MonoBehaviour , IVatCan
{
    // Start is called before the first frame update
    PlayerController player;
    EnemyController enemy;
    public UnityEvent unityEvent = new UnityEvent();
    Vector2 Pos;

    public void SetEvent()
    {
        unityEvent.AddListener(player.Find);
        unityEvent.AddListener(enemy.FindCharater);
    }

    public void SetInvoke()
    {
        unityEvent.Invoke();
    }

    void Start()
    {
        Pos = transform.position;       
    }
    public void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = FindObjectOfType<EnemyController>();
        //SetEvent();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        //SetInvoke();
    }

}
