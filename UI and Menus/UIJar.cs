using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJar : MonoBehaviour
{
    public GameObject cork;
    public RawImage filling;
    public ParticleSystem saltPour;

    private void Awake()
    {
        cork = transform.Find("Cork").gameObject;
        filling = transform.Find("Filling").GetComponent<RawImage>();
        saltPour = transform.GetComponentInChildren<ParticleSystem>();
    }

    public void SetFilling(Texture saltLevel)
    {
        filling.texture = saltLevel; 
    }
}
