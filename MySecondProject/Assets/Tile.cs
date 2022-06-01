using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    public int id;
    public GameObject hightLight;
    [SerializeField] private SpriteRenderer _renderer;
    public PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
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
    //private void OnMouseDown()
    //{
    //    if (player.start == player.finish)
    //    {
    //        player.finish = id;
    //    }
    //}
}