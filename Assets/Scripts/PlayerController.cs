using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public PlayerInputActions PlayerControls;

    public int lives = 3;
    public int currentScore = 0;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;

    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fireCooldown = 1f;

    private float lastFireTime;
    private Coroutine firingCoroutine;

    private void Awake()
    {
        PlayerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        move.Enable();

        fire = PlayerControls.Player.Fire;
        fire.Enable();
        fire.started += StartFiring;
        fire.canceled += StopFiring;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }

    public void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            LoadGameOverScene();
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    private void StartFiring(InputAction.CallbackContext context)
    {
        if (firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
    }

    private void StopFiring(InputAction.CallbackContext context)
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            if (Time.time >= lastFireTime + fireCooldown)
            {
                FireBullet();
                lastFireTime = Time.time;
            }
            yield return null; 
        }
    }

    private void FireBullet()
    {
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);

        ExplosionManager explosionManager = shootingPoint.GetComponent<ExplosionManager>();
        if (explosionManager != null)
        {
            explosionManager.TriggerExplosion();
        }
    }
}
