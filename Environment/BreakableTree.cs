using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTree : MonoBehaviour, IDamageable
{
    private bool isBroken;
    [SerializeField] private int currentHealth;

    private int timesHit = 0;
    private GameObject wholeTree;
    private GameObject breakablePart;
    private GameObject trunk;
    private GameObject roots;

    private Rigidbody2D trunkRB;

    private ParticleSystem hitParticles;
    private ParticleSystem breakParticles;

    private Transform arrowPos;

    public float rotateTime;
    private float rotateTimer = .5f;

    private float playerSide;

    Vector2 trunkPush;

    private void Awake()
    {
        wholeTree = transform.Find("WholeTree").gameObject;
        breakablePart = transform.Find("BreakablePart").gameObject;
        trunk = transform.Find("Trunk").gameObject;
        roots = transform.Find("Roots").gameObject;
        hitParticles = breakablePart.transform.Find("Hit Particles").GetComponent<ParticleSystem>();
        breakParticles = transform.Find("Break Particles").GetComponent<ParticleSystem>();
        arrowPos = GameObject.FindWithTag("ArrowCollider").transform;
        trunkRB = trunk.transform.GetChild(0).GetComponent<Rigidbody2D>();
    }

    public void DealDamage(int damage)
    {
        Debug.Log("You hit the tree");
        currentHealth--;
        breakablePart.transform.GetChild(timesHit).gameObject.SetActive(false);
        timesHit++;
        HitVFX();
        if (currentHealth <= 0)
        {
            StartCoroutine(Break());
        }
    }

    IEnumerator Break()
    {
        isBroken = true;
        breakParticles.Play();
        wholeTree.SetActive(false);
        breakablePart.SetActive(false);
        trunk.SetActive(true);
        roots.SetActive(true);
        playerSide = Mathf.Sign(breakablePart.transform.position.x - arrowPos.position.x);
        trunkPush = 5f * playerSide * Vector2.right;
        trunk.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity += trunkPush;
        while (rotateTimer < rotateTime)
        {
            trunk.transform.Rotate(0, 0, playerSide * -3);
            yield return new WaitForSeconds(.01f);
            rotateTimer++;
            yield return null;
        }
        rotateTimer = 0;
        StartCoroutine(FreezeTrunk());
    }

    IEnumerator FreezeTrunk()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => (trunkRB.angularVelocity == 0f));
        trunkRB.bodyType = RigidbodyType2D.Static;
    }

    private void HitVFX()
    {
        hitParticles.Play();
        int particleFacing = (int)Mathf.Sign(transform.position.x - arrowPos.position.x);
        var hitParticleShape = hitParticles.shape;
        hitParticleShape.rotation = new Vector3(0f, particleFacing * -90f, 0f);
    }
}
