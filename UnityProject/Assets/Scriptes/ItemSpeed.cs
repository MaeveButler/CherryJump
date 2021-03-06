using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : Item
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float distanceToPlayer;
    [Range(0, 2)] [SerializeField] private float speed;

    private bool isActive = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((player.transform.childCount > 2 || player.GetComponent<Move>() != null) && collision.gameObject.tag == player.tag)
            return;
        
        base.OnTriggerEnter2D(collision);
    }

    protected override void ItemEffect()
    {
        Destroy(GetComponent<Move>());
        
        transform.parent = player.transform;
        transform.localPosition = new Vector2(0f, distanceToPlayer);

        player.gameObject.AddComponent<Move>().SetSpeedModifier(-speed);

        audioSource.clip = effectAud;
        audioSource.Play();

        StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(player.GetComponent<Move>());

        levelPattern.itemsList.Remove(gameObject);
        Destroy(gameObject);
        yield return null;
    }
}