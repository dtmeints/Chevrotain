using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, IInteractable
{
    private Transform interactPosition;
    private SpriteRenderer sr;
    public int facing = 1;

    private Interact interactor;


    private void Awake()
    {
        interactPosition = transform.GetChild(0);
        sr = GetComponent<SpriteRenderer>();
    }

    public void EndInteract()
    {
        sr.sortingLayerName = "RightBehindCharacters";
    }

    public void StartInteract()
    {
        sr.sortingLayerName = "Chairs";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out interactor))
        {
            interactor.SetCurrentInteractable(this, "swing", interactPosition, facing);
            interactor.SetIsInInteractRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out interactor))
        {
            interactor.SetIsInInteractRange(false);
        }
    }



}
