using System.Collections;
using UnityEngine;

public class HazardRespawner : MonoBehaviour
{
    [SerializeField] Transform feet;
    Vector2 hazardRespawnPoint;
    Player player;
    PlayerInputManager pim;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        pim = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetRespawnPoint();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == 10 && gameObject.layer != 8)
        {
            StartCoroutine(HandleHazardHit());
        }
    }
    
    IEnumerator HandleHazardHit()
    {
        player.Immobilize();
        player.GetHurt();
        yield return new WaitForSeconds(.2f);
        if (player.dying) { yield break; }
        UIActions.FadeToBlack?.Invoke(true);
        transform.position = hazardRespawnPoint;
        pim.CancelAllInputs();
        player.Mobilize();
    }

    void SetRespawnPoint()
    {
        if (Physics2D.OverlapCircle(feet.position, .1f, (LayerMask.GetMask("Ground"))))
        {
            hazardRespawnPoint = transform.position; 
        }

        else if (!Physics2D.OverlapCircle(feet.position, .1f, (LayerMask.GetMask("Ground"))))
        {
            return;
        }
    }
}
