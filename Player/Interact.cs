using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private PlayerInputManager pim;
    private Animator anim;
    private PlayerData pd;
    private Rigidbody2D rb;

    
    public bool IsInInteractRange { get; private set; }
    [SerializeField] public bool IsInteracting { get; private set; }
    private bool hasInteractPoint = false;

    public IInteractable currentInteractable;
    public string currentVerb;
    [SerializeField] private Transform currentInteractPoint;
    private int interactableFacing;

    private void Awake()
    {
        pim = GetComponent<PlayerInputManager>();
        anim = GetComponent<Animator>();
        pd = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsInteracting && hasInteractPoint)
        {
            transform.position = currentInteractPoint.position;
        }
    }

    public IEnumerator DoInteract()
    {
        IsInteracting = true;
        pim.CancelAllInputs();
        anim.SetBool(currentVerb, true);
        if (hasInteractPoint) transform.position = currentInteractPoint.position;
        if (hasInteractPoint) transform.localScale = new Vector3(interactableFacing, 1, 1);
        rb.gravityScale = 0;
        currentInteractable.StartInteract();
        yield break;
    }

    public IEnumerator EndInteract()
    {
        yield return new WaitForSeconds(.1f);
        anim.SetBool(currentVerb, false);
        IsInteracting = false;
        rb.gravityScale = pd.playerGravityScale;
        currentInteractable.EndInteract();
        yield break;
    }


    public void SetCurrentInteractable(IInteractable newInteractable, string newVerb, Transform interactPoint, int facing)
    {
        currentInteractable = newInteractable;
        currentVerb = newVerb;
        currentInteractPoint = interactPoint;
        interactableFacing = facing;
        hasInteractPoint = true;
    }

    public void SetCurrentInteractable(IInteractable newInteractable, string newVerb)
    {
        currentInteractable = newInteractable;
        currentVerb = newVerb;
        hasInteractPoint = false;
    }

    public void SetIsInInteractRange(bool isInRange)
    {
        IsInInteractRange = isInRange;
    }

    public void SetIsInteracting(bool newIsInteracting)
    {
        IsInteracting = newIsInteracting;
    }
}
