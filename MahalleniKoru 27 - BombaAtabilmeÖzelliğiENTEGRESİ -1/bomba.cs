using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{

    public float guc = 10f;
    public float menzil = 5f;
    public float yukariguc = 1f;
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision!=null)
        {
            Patlama();
        }
    }


    void Patlama()
    {
        Vector3 patlamapozisyonu = transform.position;
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

                rb.AddExplosionForce(guc,patlamapozisyonu,menzil,0,ForceMode.Impulse); // yukar�guc yerine 0 da verebilirsin bu sana kalm��

                //  �OK �NEML� 2 : AddExplosionForce() : Bu Fonksiyon �zel patlama g�c� olu�turmay� bize sa�lar.[ BEN�M YORUMUM : �stte ETK�LE��M� yakalad���m ve R�G�DBODY ' E sahip objelere PATLAMA EFEKT� G�B� B�R KUVVET UYGULAR !!!
            }

        }


    }
    
   
}
