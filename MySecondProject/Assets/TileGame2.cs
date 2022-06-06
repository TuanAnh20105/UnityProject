using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGame2 : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    public int id;
    public GameObject hightLight;
    [SerializeField] private SpriteRenderer _renderer;
    public PlayerCharaterGame2 player;
    public SetUpObstaclesGame2 setUpObstacles;
    private void Start()
    {
        setUpObstacles = FindObjectOfType<SetUpObstaclesGame2>();
        player = FindObjectOfType<PlayerCharaterGame2>();
        for (int i = 0; i < setUpObstacles.listObstacle.Count; i++)
        {
            if (id == setUpObstacles.listObstacle[i])
            {
                _renderer.color = Color.red;
            }
        }
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
        if (player.start == player.finish)
        {
            player.finish = id;
        }
    }
}