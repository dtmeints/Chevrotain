using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CircleLight : MonoBehaviour
{
    Light2D circleLight;
    public float maxIntensity = 20f;
    public float flashSpeed = .1f;

    private void Awake()
    {
        circleLight = GetComponent<Light2D>();
    }



    private void DoHealFlash(Player player)
    {
        StartCoroutine(HealFlash());
    }

    IEnumerator HealFlash()
    {
        while (circleLight.intensity < maxIntensity - 1)
        {
            circleLight.intensity = Mathf.Lerp(circleLight.intensity, maxIntensity, flashSpeed * Time.deltaTime);
            yield return null;
        }
        circleLight.intensity = 0;
    }


    private void OnEnable()
    {
        Player.OnHeal += DoHealFlash;
    }

    private void OnDisable()
    {
        Player.OnHeal -= DoHealFlash;
    }
}
