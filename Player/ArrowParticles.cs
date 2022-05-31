using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArrowCustomization;

public class ArrowParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem iceParticles;
    [SerializeField] ParticleSystem stoneParticles;
    [SerializeField] ParticleSystem lightParticles;

    public static Dictionary<string, ParticleSystem> arrowParticleDict = new Dictionary<string, ParticleSystem>();

    public ParticleSystem activeParticles;

    private PlayerAugments playerAugments;


    private void Awake()
    {
        playerAugments = FindObjectOfType<PlayerAugments>();

        if (arrowParticleDict.Count == 0)
        {
            arrowParticleDict.Add("fire", fireParticles);
            arrowParticleDict.Add("ice", iceParticles);
            arrowParticleDict.Add("stone", stoneParticles);
            arrowParticleDict.Add("light", lightParticles);
        }
    }
    public void SetActiveParticles(string type)
    {
        activeParticles = arrowParticleDict[type];
    }
    
    public void PlayArrowParticles()
    {
        if (activeParticles == null) return;
        activeParticles.Play();
    }

    private void FindLargestBarbCount(BarbType barb)
    {
        if (playerAugments.heatStage < 6)
        {
            SetActiveParticles("fire");
        }
        else if (playerAugments.heatStage > 6)
        {
            SetActiveParticles("ice");
        }
    }

    private void OnEnable()
    {
        PlayerAugments.OnBarbChange += FindLargestBarbCount;
    }
    private void OnDisable()
    {
        PlayerAugments.OnBarbChange += FindLargestBarbCount;
    }
}
