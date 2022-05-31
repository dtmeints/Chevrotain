using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject dustParticles;
    public int numberOfParticles;
    ParticleSystem dustParticleSystem;


    private void Awake()
    {
        dustParticleSystem = dustParticles.GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionSpeed = collision.relativeVelocity.magnitude;
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject particleInstance;
        if (contactPoint.y - collision.transform.position.y < 1)
        {
            particleInstance = Instantiate(dustParticles, contactPoint, Quaternion.identity) as GameObject;
        }
        else if (contactPoint.x - collision.transform.position.x > .1)
        {
            particleInstance = Instantiate(dustParticles, contactPoint, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
        }
        else if (contactPoint.x - collision.transform.position.x < -.1)
        {
            particleInstance = Instantiate(dustParticles, contactPoint, Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
        }
        else
        {
            particleInstance = Instantiate(dustParticles, contactPoint, Quaternion.identity) as GameObject;
        }
        StartCoroutine(WaitThenCleanup(particleInstance));
    }

    IEnumerator WaitThenCleanup(GameObject toDestroy)
    {
        yield return new WaitForSeconds(2);
        Destroy(toDestroy);
    }
}
