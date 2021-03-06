using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoints : Item
{
    [SerializeField] private int points;
    
    protected override void ItemEffect()
    {
        mainGame.Score(points);
        
        audioSource.clip = effectAud;
        audioSource.Play();

        SpriteRenderer pointsSR = GetComponent<SpriteRenderer>();
        pointsSR.sprite = null;

        StartCoroutine(DestroyItem());
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        levelPattern.itemsList.Remove(gameObject);
        Destroy(gameObject);
    }
}