using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        if(other.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            Die();
        }
    }

    private void Die() 
    {
        {
            ItemCollector.SetAmmoCount(0);
            deathSoundEffect.Play();
            animator.SetTrigger("Death");
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
