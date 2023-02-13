using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{    
    [SerializeField] private AudioSource collectionCherrySoundEffect;
    [SerializeField] private AudioSource collectionAmmoSoundEffect;
    private int cherryCounter=0;
    public static int ammoCounter=0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private TextMeshProUGUI ammoText;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Cherry"))
        {
            collectionCherrySoundEffect.Play();
            other.gameObject.GetComponent<Animator>().SetTrigger("Collected");
            cherryCounter++;
            cherryText.text = ": "+cherryCounter;
        }
        if(other.gameObject.CompareTag("Ammo"))
        {
            collectionAmmoSoundEffect.Play();
            other.gameObject.GetComponent<Animator>().SetTrigger("Collected");
            ammoCounter+=5;
            ammoText.text = ": "+ammoCounter;
        }
    }

    public static int GetAmmoCount()
    {
        return ammoCounter;
    }

    public static void UseAmmo()
    {
        ammoCounter--; 
    }

    public static void SetAmmoCount(int newAmmoCounter)
    {
        ammoCounter= newAmmoCounter;
    }
}
