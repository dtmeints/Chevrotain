using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaybackArt : MonoBehaviour
{
    private Transform player;

    private Vector3 oldPlayerPosition;

    public float parallaxGain;

    [SerializeField] Transform layer1;
    [SerializeField] Transform layer2;
    [SerializeField] Transform layer3;
    [SerializeField] Transform layer4;

    private List<Transform> layers = new List<Transform>();

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        layers.Add(layer1);
        layers.Add(layer2);
        layers.Add(layer3);
        layers.Add(layer4);
    }

    private void Update()
    {
        if (oldPlayerPosition != player.position)
        {
            var playerPosYDelta = player.position.y - oldPlayerPosition.y;
            foreach (Transform t in layers)
            {
                t.position += parallaxGain * Vector3.down * playerPosYDelta / (.5f * t.position.z);
            }
        }
        oldPlayerPosition = player.position;
    }
}
