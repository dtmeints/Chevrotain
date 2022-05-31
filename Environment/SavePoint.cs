using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public Interact interactor;
    private float saveCooldown = 1;


    private ParticleSystem particles;

    private void Awake()
    {
        particles = transform.Find("particles").GetComponent<ParticleSystem>();
        interactor = FindObjectOfType<Interact>();
    }
    public void EndInteract()
    {
        return;
    }

    public void StartInteract()
    {
        EnemyManager.RespawnAllDead();
        SaveSystem.CreateSaveData(interactor.gameObject.GetComponent<Player>(), interactor.gameObject.GetComponent<Inventory>());
        interactor.GetComponent<Player>().HealToFull();
        particles.Play();
        StartCoroutine(InteractTime());
    }

    IEnumerator InteractTime()
    {
        yield return new WaitForSeconds(saveCooldown);
        StartCoroutine(interactor.EndInteract());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interact>())
        {
            interactor.SetCurrentInteractable(this, "save");
            interactor.SetIsInInteractRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interact>())
        {
            interactor.SetIsInInteractRange(false);
        }
    }
}
