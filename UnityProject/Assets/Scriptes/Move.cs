using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speedModifier = 1f;
    [SerializeField] private bool moveObject = true;

    private MainGame mainGame;
    private bool gameRunning = false;
    private float levelSpeed;

    private void Awake()
    {
        mainGame = FindObjectOfType<MainGame>();
    }
    
    private void Update()
    {
        levelSpeed = mainGame.levelSpeed * speedModifier;
    }
    
    private void FixedUpdate()
    {
        if (moveObject)
        {
            float xPos = transform.position.x - levelSpeed * Time.deltaTime;
            transform.position = new Vector2(xPos, transform.position.y);
        }
    }

    public void MoveObject(bool value)
    {
        moveObject = value;
    }
    public void SetSpeedModifier(float value)
    {
        speedModifier = value;
    }
}