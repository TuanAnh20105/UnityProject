using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    public int id;
    public SpriteRenderer _renderer;
    public PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : baseColor;
    }
    private void OnMouseDown()
    {
      player.touch.a = id;
    }
}
