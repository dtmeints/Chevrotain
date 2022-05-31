using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour, IInteractable
{

    private GameObject front;
    private GameObject back;
    private Transform interactPosition;
    private Animator anim;

    private Interact interactor;

    [SerializeField] private int facing = 1;

    private void Awake()
    {
        front = transform.Find("front").gameObject;
        back = transform.Find("back").gameObject;
        interactPosition = transform.Find("transform");
        anim = GetComponent<Animator>();
    }

    public void StartInteract()
    {
        front.GetComponent<SpriteRenderer>().sortingLayerName = "Art Front";
        back.GetComponent<SpriteRenderer>().sortingLayerName = "Chairs";
        anim.SetBool("sway", true);
    }

    public void EndInteract()
    {
        front.GetComponent<SpriteRenderer>().sortingLayerName = "RightBehindCharacters";
        back.GetComponent<SpriteRenderer>().sortingLayerName = "RightBehindCharacters";
        anim.SetBool("sway", false);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter!");
        if(collision.TryGetComponent(out interactor))
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
