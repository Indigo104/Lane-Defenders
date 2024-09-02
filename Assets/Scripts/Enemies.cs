using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed;
    public int health;
    public float stunDuration = 0.5f;

    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip lifeLost;
    //private AudioSource audioSource;

    private Animator animator;
    private bool isStunned = false;
    private bool isDead = false; 

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isStunned && !isDead)
        {
            transform.position -= new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy collided with a bullet
        if (collision.CompareTag("Bullet"))
        {
            health--;
            animator.SetTrigger("hit");

            StartCoroutine(Hit());

            if (health <= 0 && !isDead)
            {
                StartCoroutine(Death());
            }
        }
        if (collision.CompareTag("Player") || collision.CompareTag("Killbox"))
        {
            PlaylifeLostSound();

            Destroy(gameObject);

            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
                player.LoseLife();
        }
    }

    private void PlaylifeLostSound()
    {
        AudioSource.PlayClipAtPoint(lifeLost, transform.position);
    }
    private IEnumerator Hit()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position);

        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        if (!isDead)
        {
            isStunned = false;
        }
    }

    private IEnumerator Death()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        isDead = true;
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.AddScore(10);
    }
}
