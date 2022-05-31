using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbCollectible : MonoBehaviour, ICollectible
{
    Animator anim;
    GameObject mainGO;
    GameObject sparkleParticles;
    GameObject fogParticles;
    Collider2D col;

    public string id = "NOTASSIGNED";
    public bool isCollected = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mainGO = transform.GetChild(0).gameObject;
        sparkleParticles = mainGO.transform.Find("sparkle particles").gameObject;
        fogParticles = mainGO.transform.Find("fog particles").gameObject;
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.GetComponent<Inventory>().AddBarb(1);
            anim.SetBool("collected", true);
            isCollected = true;
            sparkleParticles.SetActive(false);
            fogParticles.SetActive(false);
            col.enabled = false;
            StartCoroutine(Cleanup());
        }
    }


    IEnumerator Cleanup()
    {
        yield return new WaitForSeconds(10f);
        FinishCollect();
    }

    public void FinishCollect()
    {
        col.enabled = false;
        isCollected = true;
        mainGO.SetActive(false);
        anim.enabled = false;
    }

    public string GetID()
    {
        return id;
    }

    public bool GetStatus()
    {
        return isCollected;
    }

    public void SetStatus(bool collected)
    {
        isCollected = collected;
        gameObject.SetActive(!collected);
    }

    private void OnEnable()
    {
        CollectibleManager.AddToDict(this);
    }
}
