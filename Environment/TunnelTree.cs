using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTree : MonoBehaviour
{

    private GameObject front;

    private SpriteRenderer frontSR;
    private Color fadedColor = new Color(1f,1f,1f,.2f);

    [SerializeField] float fadeSpeed;

    [SerializeField] private bool isTranslucent = false;

    private void Awake()
    {
        front = transform.Find("Front").gameObject;
        frontSR = front.GetComponent<SpriteRenderer>(); 
    }

    private void FixedUpdate()
    {
        ManageTranslucence();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Player>(out Player player);
        if (player != null)
        {
            isTranslucent = true;
           // StartCoroutine(Fade());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.TryGetComponent(out Player player);
        if (player != null)
        {
            isTranslucent = false;
        }
    }

    private void ManageTranslucence()
    {
        if (!isTranslucent && frontSR.color != Color.white)
        {
            frontSR.color = Color.Lerp(frontSR.color, Color.white, fadeSpeed);
        }
        if (isTranslucent && frontSR.color != fadedColor)
        {
            frontSR.color = Color.Lerp(frontSR.color, fadedColor, fadeSpeed);
        }
    }
}
