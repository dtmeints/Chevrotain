using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiderLayer : MonoBehaviour
{

    private Tilemap tilemap;
    private Color fadedColor = new Color(1f, 1f, 1f, 0f);

    [SerializeField] float fadeSpeed;

    [SerializeField] private bool isTranslucent = false;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Update()
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
        if (!isTranslucent && tilemap.color != Color.white)
        {
            tilemap.color = Color.Lerp(tilemap.color, Color.white, fadeSpeed);
        }
        if (isTranslucent && tilemap.color != fadedColor)
        {
            tilemap.color = Color.Lerp(tilemap.color, fadedColor, fadeSpeed);
        }
    }
}
