using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private bool levelCompleted = false;
    [SerializeField] AudioSource finishSound;
    // Start is called before the first frame update
    void Start()
    {
        finishSound=GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            finishSound.Play();
            Invoke("CompleteLevel",2f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}