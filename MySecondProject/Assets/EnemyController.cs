using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text healthText ;
    public int health;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     UpdateTextBox();   
    }
    void UpdateTextBox()
    {
        healthText.text = health.ToString();
    }
    private void OnMouseDown() {
        healthText.text = health.ToString();
    }
}
