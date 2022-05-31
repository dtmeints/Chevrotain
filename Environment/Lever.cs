using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IPermanent
{

    [SerializeField] GameObject affected;
    private ILeverable linkedLeverable;
    public bool isFlipped = false;
    [SerializeField] private string id = "NOTASSIGNED";

    private Animator anim;

    private Collider2D col;
    private ParticleSystem particles;
    private GameObject lightObject;

    public bool isPermanent = true;

    [System.NonSerialized] public Transform arrowHolderPos;

    private void OnEnable()
    {
        SendToList();
    }
    private void Awake()
    {
        //cached refs
        linkedLeverable = affected.GetComponent<ILeverable>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        particles = transform.Find("Particles").GetComponent<ParticleSystem>();
        lightObject = transform.Find("Light").gameObject;
        arrowHolderPos = transform.Find("ArrowHolderPos");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ArrowHolder arrow))
        {
            arrow.SetInLeverRange(true, this);
            particles.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ArrowHolder arrow))
        {
            arrow.SetInLeverRange(false, this);
            particles.Stop();
        }
    }

    public void CrankLever()
    {
        anim.SetBool("crank", true);
        linkedLeverable.Activate();
        isFlipped = true;
    }

    public void UncrankLever()
    {
        anim.SetBool("uncrank", true);
        linkedLeverable.Deactivate();
        isFlipped = false;
    }

    public void KillLever()
    {
        anim.SetBool("crank", false);
        anim.SetBool("uncrank", false);
        if (isPermanent)
        {
            col.enabled = false;
            lightObject.SetActive(false);
        }
        else return;
    }

    public bool GetStatus()
    {
        return isFlipped;
    }

    public void SetStatus(bool status)
    {
        isFlipped = status;
        if (isFlipped)
        {
            InitializeStatus();
        }
    }

    public string GetID()
    {
        return id;
    }

    public void InitializeStatus()
    {
        CrankLever();
        linkedLeverable.Activate();
        KillLever();
    }

    public void SendToList()
    {
        Permanents.AddToList(this);
    }
}
