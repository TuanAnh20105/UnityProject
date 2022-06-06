using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    public int id;
    public GameObject hightLight;
    public SpriteRenderer _renderer;
    public SetUpObstacles setUpObstacles;
    private void Start()
    {

    }
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : baseColor;
    }
    private void OnMouseEnter()
    {
        hightLight.SetActive(true);
       
    }
    private void OnMouseExit()
    {
        hightLight.SetActive(false);
    }
}