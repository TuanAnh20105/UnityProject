using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit2D hitInformation;
    public TMP_Text healthText;
    public int health = 20;
    bool checkMove = false;
    public int a;
    public GameObject princess;
    public List<EnemyController> enemies;
    public Transform[] listTrans ;
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
        if (checkMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, vt, Time.deltaTime * 5f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var touchWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchWorld2D = new Vector2(touchWorld.x, touchWorld.y);
            hitInformation = Physics2D.Raycast(touchWorld2D, Camera.main.transform.forward);
             vt = hitInformation.transform.position;
            if (hitInformation.transform.name != "back")
            {
                checkMove = true;
            }

            else checkMove = false;
        }
        if (a <= 0)
        {
            healthText.text = "0";
            ManagerGame.Instance.GameOver();
        }
        if (count == 4)
        {
            princess.AddComponent<CircleCollider2D>();
            count++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Princess")
        {
            ManagerGame.Instance.GameOver();
        }
        if (other.gameObject.tag == "Enemy1")
        {
            a = health;
            a = health - enemies[0].health;
            if (a > 0)
            {
                health += enemies[0].health;
                healthText.text = health.ToString();
                Destroy(other.gameObject, 2);
                count++;
            }
        }
        else if (other.gameObject.tag == "Enemy2")
        {
            a = health;
            a = health - enemies[1].health;
            if (a > 0)
            {
                health += enemies[1].health;
                healthText.text = health.ToString();
                Destroy(other.gameObject, 2);
                count++;
            }
        }
        else if (other.gameObject.tag == "Enemy3")
        {
            a = health;
            a = health - enemies[2].health;
            if (a > 0)
            {
                health += enemies[2].health;
                healthText.text = health.ToString();
                Destroy(other.gameObject, 2);
                count++;
            }
        }
        else if (other.gameObject.tag == "Enemy4")
        {
            a = health;
            a = health - enemies[3].health;
            if (a > 0)
            {
                health += enemies[3].health;
                healthText.text = health.ToString();
                Destroy(other.gameObject, 2);
                count++;
            }
        }
    }
    void SetCheckHealth()
    {
        checkHealth = true;
    }

}
