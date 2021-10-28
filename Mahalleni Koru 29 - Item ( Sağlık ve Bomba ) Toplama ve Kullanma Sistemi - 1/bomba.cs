using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{

    public float guc = 10f;
    public float menzil = 5f;
    public float yukariguc = 1f;
    public ParticleSystem patlamaEfekt;
    AudioSource patlamaSesi;
    
    void Start()
    {
        patlamaSesi = gameObject.GetComponent<AudioSource>();
          
    }

  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision!=null)
        {
            Patlama();
            Destroy(gameObject, 0.5f); // Bunu IENumarator kullanarakta yapabilirsin !!!!!!!    
        }
    }


    void Patlama()
    {
        Vector3 patlamapozisyonu = transform.position;
        Instantiate(patlamaEfekt, transform.position, transform.rotation);
        patlamaSesi.Play();

        Collider[] colliders = Physics.OverlapSphere(patlamapozisyonu,menzil);

        //  �OK �NEML� 1 : Physics.OverlapSphere() = �evresel bir F�Z�KSEL YUVARLAK olu�turur.[ BEN�M YORUMUM : Kendi objemle ( Yani Bombayla ) Collider 'a sahip objeLERin ETK�LE��M�N� YAKALIYORUM !!!!!


        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if(hit !=null && rb)
            {
                if (hit.gameObject.CompareTag("Dusman"))
                {
                    hit.transform.gameObject.GetComponent<Dusman_>().oldun();
                }

                rb.AddExplosionForce(guc,patlamapozisyonu,menzil,yukariguc,ForceMode.Impulse); // yukar�guc yerine 0 da verebilirsin bu sana kalm��

                //  �OK �NEML� 2 : AddExplosionForce() : Bu Fonksiyon �zel patlama g�c� olu�turmay� bize sa�lar.[ BEN�M YORUMUM : �stte ETK�LE��M� yakalad���m ve R�G�DBODY ' E sahip objelere PATLAMA EFEKT� G�B� B�R KUVVET UYGULAR !!!
            }

        }


    }
    
   
}
