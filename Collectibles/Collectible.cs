using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemTypes;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    public ItemType type;
    public int numberValue = 1;
    public float lifetime = 20f;

    private bool isTemporary = false;

    SpriteRenderer sr;
    Color clear = new Color(0, 0, 0, 0);
    public float blinkSpeed = .1f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Inventory inventory))
        {
            Debug.Log(type + " picked up!");
            inventory.ChangeItemsHeld(this, numberValue);
            if (isTemporary)
            {
                StopAllCoroutines();
                sr.color = Color.white;
            }
            gameObject.SetActive(false);
        }
        else return;
    }

    public void StartLifeCountdown()
    {
        isTemporary = true;
        sr.color = Color.white;
        StartCoroutine(LifeCountdown());    
    }
    IEnumerator LifeCountdown()
    {
        yield return new WaitForSeconds(lifetime - 2f);
        StartCoroutine(SlowBlink());
        yield return new WaitForSeconds(2f);
        StopCoroutine(SlowBlink());
        StartCoroutine(FastBlink());
        yield return new WaitForSeconds(1f);
        StopCoroutine(FastBlink());
        gameObject.SetActive(false);
    }

    IEnumerator SlowBlink()
    {
        sr.color = clear;
        yield return new WaitForSeconds(.05f);
        sr.color = Color.white;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SlowBlink());
    }

    IEnumerator FastBlink()
    {
        sr.color = clear;
        yield return new WaitForFixedUpdate();
        sr.color = Color.white;
        yield return new WaitForFixedUpdate();
        StartCoroutine(FastBlink());
    }
}
