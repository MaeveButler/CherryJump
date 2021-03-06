using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected AudioClip effectAud;

    protected MainGame mainGame;
    protected LevelPattern levelPattern;
    protected Player player;
    protected AudioSource audioSource;

    private void Awake()
    {
        mainGame = FindObjectOfType<MainGame>();
        levelPattern = FindObjectOfType<LevelPattern>();
        player = FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player.tag)
            ItemEffect();
    }

    protected virtual void ItemEffect() { }
}