using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnim : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteSheet;
    [SerializeField] private float frameTime;

    private Image image;
    private Queue<Sprite> spriteQueue = new Queue<Sprite>();

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartAnim()
    {
        spriteQueue.Clear();
        foreach (Sprite sprite in spriteSheet)
            spriteQueue.Enqueue(sprite);
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        for (int i = 0; i < spriteSheet.Length; i++)
        {
            yield return new WaitForSeconds(frameTime);
            Sprite currentSprite = spriteQueue.Dequeue();
            spriteQueue.Enqueue(currentSprite);
            image.sprite = currentSprite;

            if (i == spriteSheet.Length - 1)
                StartCoroutine(Anim());
        }

        yield return null;
    }
}