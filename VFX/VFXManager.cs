using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] Transform VFXContainer;
    

    private void SpawnDeathParticles(Entity enemy)
    {
        GameObject deathParticles = Instantiate(enemy.entityData.deathParticles, enemy.aliveGO.transform.position, Quaternion.identity, VFXContainer) as GameObject;
        StartCoroutine(CleanUp(deathParticles, deathParticles.GetComponent<ParticleSystem>().main.duration));
    }

    private void SpawnDeathParticles(Projectile projectile)
    {
        GameObject deathParticles = Instantiate(projectile.destructParticles, projectile.transform.position, Quaternion.identity, VFXContainer) as GameObject;
        StartCoroutine(CleanUp(deathParticles, deathParticles.GetComponent<ParticleSystem>().main.duration));
    }

    private void SpawnHitParticles(Entity enemy)
    {
        GameObject hitParticles = Instantiate(enemy.entityData.hitParticles, enemy.closestPoint, Quaternion.identity, VFXContainer) as GameObject;
        StartCoroutine(CleanUp(hitParticles, hitParticles.GetComponent<ParticleSystem>().main.duration));
    }

    private void SpawnSwoop(Player player, Transform spot, GameObject thingToSpawn)
    {
        GameObject swoop = Instantiate(thingToSpawn, spot.position, Quaternion.identity, player.transform) as GameObject;
        var swoopClipInfo = swoop.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        float currentClipLength = swoopClipInfo[0].clip.length;
        StartCoroutine(CleanUp(swoop, currentClipLength -.1f));
    }

    IEnumerator CleanUp(GameObject thing, float systemDuration = 2)
    {
        yield return new WaitForSeconds(systemDuration);
        Destroy(thing);
    }

    private void OnEnable()
    {
        Entity.OnEnemyDied += SpawnDeathParticles;
        Entity.OnEnemyHit += SpawnHitParticles;
        BasicAttacks2.OnUpAttack += SpawnSwoop;
        BasicAttacks2.OnDownAttack += SpawnSwoop;
        BasicAttacks2.OnFrontAttack += SpawnSwoop;
        Projectile.OnProjectileDestruct += SpawnDeathParticles;
    }

    private void OnDisable()
    {
        Entity.OnEnemyDied -= SpawnDeathParticles;
        Entity.OnEnemyHit -= SpawnHitParticles;
        BasicAttacks2.OnUpAttack -= SpawnSwoop;
        BasicAttacks2.OnDownAttack -= SpawnSwoop;
        BasicAttacks2.OnFrontAttack -= SpawnSwoop;
        Projectile.OnProjectileDestruct -= SpawnDeathParticles;
    }


}
