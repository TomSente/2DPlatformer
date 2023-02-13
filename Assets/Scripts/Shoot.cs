using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;

    [SerializeField] public GameObject bulletPrefab;

    [SerializeField] private AudioSource shootSoundEffect;

    [SerializeField] private TextMeshProUGUI ammoText;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int ammoCount = ItemCollector.GetAmmoCount();
        if(Input.GetButtonDown("Fire1")&& ammoCount>0)
        {
            ItemCollector.UseAmmo();
            ammoText.text=": "+ItemCollector.GetAmmoCount();
            shootSoundEffect.Play();
            animator.SetTrigger("Shoot");
            Debug.Log("Shoot");
            Instantiate(bulletPrefab,shootingPoint.position,transform.rotation);
        }
    }
}
