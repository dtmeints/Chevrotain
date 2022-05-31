using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{

    public float dissolveRate;
    public bool startDissolved;

    Material dissolveMaterial;


    private void Awake()
    {
        dissolveMaterial = GetComponent<SpriteRenderer>().material;
        if (startDissolved) { dissolveMaterial.SetFloat("_Fade", 0f); }
    }

    public void DoDissolve() => StartCoroutine(DissolveAnimation());
    private void DoUndissolve() => StartCoroutine(UndissolveAnimation());

    public IEnumerator DissolveAnimation()
    {
        if(dissolveMaterial.GetFloat("_Fade") <= 0)
        {
            dissolveMaterial.SetFloat("_Fade", 0);
            yield break;
        }
        else
        {
            dissolveMaterial.SetFloat("_Fade", dissolveMaterial.GetFloat("_Fade") - Time.deltaTime * dissolveRate);
            yield return new WaitForEndOfFrame();
            StartCoroutine(DissolveAnimation());
            yield break;
        }
    }

    public IEnumerator UndissolveAnimation()
    {
        if (dissolveMaterial.GetFloat("_Fade") >= 1)
        {
            dissolveMaterial.SetFloat("_Fade", 1);
            yield break;
        }
        else
        {
            dissolveMaterial.SetFloat("_Fade", dissolveMaterial.GetFloat("_Fade") + Time.deltaTime * dissolveRate);
            yield return new WaitForEndOfFrame();
            StartCoroutine(UndissolveAnimation());
            yield break;
        }
    }

    private void OnEnable()
    {
       // PlayerActions.OnDeath += DoDissolve;
        Player.OnRespawn += DoUndissolve;
    }
    private void OnDisable()
    {
       // PlayerActions.OnDeath -= DoDissolve;
        Player.OnRespawn -= DoUndissolve;
    }
}
