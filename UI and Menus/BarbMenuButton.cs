using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArrowCustomization;

public class BarbMenuButton : MonoBehaviour
{

    [SerializeField] BarbType barbType;
    [SerializeField] int addOrSubtract;
    private PlayerAugments playerAugments;

    private void Awake()
    {
        playerAugments = FindObjectOfType<PlayerAugments>();
    }
    public void ActivateButton()
    {
        playerAugments.EditBarb(barbType, addOrSubtract);
    }
}
