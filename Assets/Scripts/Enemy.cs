using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health=100;

    [SerializeField] private GameObject peaBulletPrefab;

    [SerializeField] Animator animator;

    [SerializeField] private Transform shootingPoint;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("TriggerAnimator",0f,2f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
        if(health<=0)
        {
            animator.SetTrigger("Death");
        }
    }

    public void TriggerAnimator()
    {
        animator.SetTrigger("Shoot");
    }

    private void Shoot()
    {
         Instantiate(peaBulletPrefab,shootingPoint.position,shootingPoint.rotation);
    }



    // Update is called once per frame
}
