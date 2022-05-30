using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    public int id;
    public GameObject hightLight;
    [SerializeField] private SpriteRenderer _renderer;
    public ManagerMove managerMove;
    private void Start()
    {
        managerMove = FindObjectOfType<ManagerMove>();
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
    private void OnMouseDown()
    {
        if (managerMove.start == managerMove.finish)
        {
            managerMove.finish = id;
        }
    }
}