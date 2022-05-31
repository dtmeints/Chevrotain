using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    float timer = 0f;

    public bool isBouncing = false;

    public IEnumerator Bounce(Rigidbody2D rb, float bounceSpeed, float bounceTime)
    {
        isBouncing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        while (timer < bounceTime)
        {
            rb.velocity += bounceSpeed * Time.deltaTime * Vector2.up;
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        isBouncing = false;
        yield break;
    }
}
