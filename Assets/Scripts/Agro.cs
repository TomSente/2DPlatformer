using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agro : MonoBehaviour
{
    // Start is called before the first frame update
        private RhinoEnemy rhinoEnemy;
        void Start()
        {
        rhinoEnemy = new RhinoEnemy();
        }
        private void OnTriggerEnter2D(Collider2D other) 
        {  

        if(other.gameObject.CompareTag("Player"))
            {
                rhinoEnemy.Charge();
            }
        }
}
