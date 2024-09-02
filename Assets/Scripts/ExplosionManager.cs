using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AudioClip ShootSFX;

    public void TriggerExplosion()
    {
        if (ShootSFX != null)
        {
            AudioSource.PlayClipAtPoint(ShootSFX, transform.position);
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Animator animator = explosion.GetComponent<Animator>();
            Destroy(explosion, 1f);
        }
    }
}
