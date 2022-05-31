using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text healthText;
    public int health = 20;
    public int a;
    public List<EnemyController> enemies;
    bool checkHealth = true;
    public int count = 0;
    Vector2 vt;
    void Start()
    {
        a = health;
        healthText.text = health.ToString();


    }
    // Update is called once per frame
    void Update()
    {       
        if (a <= 0)
        {
            healthText.text = "0";
            ManagerGame.Instance.GameOver();
        }
        if (count == 3)
        {
            count++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        
    }
    void SetCheckHealth()
    {
        checkHealth = true;
    }

}
