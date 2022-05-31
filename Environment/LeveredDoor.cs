using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeveredDoor : MonoBehaviour, ILeverable
{

    private Animator anim;
    private GameObject animatedDoor;
    private GameObject open;


    public bool isPermanent;

    private void Awake()
    {
        open = transform.Find("open").gameObject;
        animatedDoor = transform.Find("animated").gameObject;
        anim = animatedDoor.GetComponent<Animator>();
    }

    public void Activate()
    {
        anim.SetBool("opening", true);
        StartCoroutine(SetActivated());
    }

    IEnumerator SetActivated()
    {
        yield return new WaitForSeconds(.1f);
        anim.SetBool("opening", false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        if (isPermanent)
        {
            open.SetActive(true);
            animatedDoor.SetActive(false);
        }
    }

    public void Deactivate()
    {
        anim.SetBool("closing", true);
        StartCoroutine(SetDeactivated());
    }

    IEnumerator SetDeactivated()
    {
        yield return new WaitForSeconds(.1f);
        anim.SetBool("closing", false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}
