using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D=GetComponent<Rigidbody2D>();
        rigidbody2D.velocity=transform.right*speed;
        Destroy(gameObject,5f);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(100f);
            Destroy(gameObject);

        }
        if(other.gameObject.CompareTag("Surface"))
        {
            Destroy(gameObject);
        }
    }
}
