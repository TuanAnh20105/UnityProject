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
    public Color color;
    public TextMeshPro txtNumber;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetTxtNumber(string txt)
    {
        txtNumber.text = txt;
    }
    public void SetColorNumber(Color color)
    {
        this.color = color;
        spriteRenderer.color = this.color;
    }
}


