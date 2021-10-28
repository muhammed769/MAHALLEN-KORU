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

        //  ÇOK ÖNEMLÝ 1 : Physics.OverlapSphere() = Çevresel bir FÝZÝKSEL YUVARLAK oluþturur.[ BENÝM YORUMUM : Kendi objemle ( Yani Bombayla ) Collider 'a sahip objeLERin ETKÝLEÞÝMÝNÝ YAKALIYORUM !!!!!


        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if(hit !=null && rb)
            {
                if (hit.gameObject.CompareTag("Dusman"))
                {
                    hit.transform.gameObject.GetComponent<Dusman_>().oldun();
                }

                rb.AddExplosionForce(guc,patlamapozisyonu,menzil,yukariguc,ForceMode.Impulse); // yukarýguc yerine 0 da verebilirsin bu sana kalmýþ

                //  ÇOK ÖNEMLÝ 2 : AddExplosionForce() : Bu Fonksiyon Özel patlama gücü oluþturmayý bize saðlar.[ BENÝM YORUMUM : Üstte ETKÝLEÞÝMÝ yakaladýðým ve RÝGÝDBODY ' E sahip objelere PATLAMA EFEKTÝ GÝBÝ BÝR KUVVET UYGULAR !!!
            }

        }


    }
    
   
}
