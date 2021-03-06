using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : Item
{
    [SerializeField] private float waitTime = 1f;

    private Move movePlayer;
    private bool isActive = false;
    
    protected override void ItemEffect()
    {
        SpriteRenderer bombSR = GetComponent<SpriteRenderer>();

        if (player.GetComponent<Move>() == null)
            movePlayer = player.gameObject.AddComponent<Move>();
        else
            waitTime *= 2f;

        bombSR.sprite = null;
        player.IdleAnim();

        isActive = true;

        StartCoroutine(Move());

        audioSource.clip = effectAud;
        audioSource.Play();
    }

    private void Update()
    {
        if (isActive)
        {
            if (player.grounded == true)
                player.IdleAnim();
        }
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(player.GetComponent<Move>());
        player.RunAnim();

        levelPattern.itemsList.Remove(gameObject);
        Destroy(gameObject);
        yield return null;
    }
}