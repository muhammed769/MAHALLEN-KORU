using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boskovan : MonoBehaviour
{

    AudioSource yeredusmesesi;
    void Start()
    {
        yeredusmesesi = GetComponent<AudioSource>();
        Destroy(gameObject, 2f);       
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Yol"))
        {
            yeredusmesesi.Play();

            if (!yeredusmesesi.isPlaying)
            {
                Destroy(gameObject,1f);

            }
           

        }
    }

   
}
