using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public int id;
    public int ma;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public TextMeshPro txtNumber;
    public Sprite effect;
    public GameObject EffectRight;
    public GameObject EffectLeft;
    public GameObject EffectButtom;
    public GameObject EffectTop;
    public Transform trans;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trans = gameObject.transform;
    }
    public void SetTxtNumber(string txt)
    {
        txtNumber.text = txt;
    }
    public void SetSpriteNumber(Sprite sprite)
    {
        this.sprite = sprite;
        spriteRenderer.sprite = this.sprite;
    }
    public void SetEffect()
    {
        spriteRenderer.sprite = effect;
        Invoke("SetImg", 0.5f);
    }
    public void SetImg()
    {
        spriteRenderer.sprite = sprite;
    }
    public void CheckDirectEffect(Number number)
    {
        if(number.transform.position.x > gameObject.transform.position.x)
        {
           EffectRight.SetActive(true);

        }
        if(number.transform.position.x < gameObject.transform.position.x)
        {
            EffectLeft.SetActive(true);

        }
        if(number.transform.position.y > gameObject.transform.position.y)
        {
            EffectButtom.SetActive(true);

        } 
        if(number.transform.position.y < gameObject.transform.position.y)
        {
            EffectTop.SetActive(true);
        }
        Invoke("SetActiveEffect", .5f);
    }
    public void SetActiveEffect()
    {
         EffectRight.SetActive(false);
         EffectLeft.SetActive(false);
         EffectButtom.SetActive(false);
         EffectTop.SetActive(false);
    }

}


