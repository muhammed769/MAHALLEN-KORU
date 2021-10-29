using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * -250f; // Transform.Translate ilede bunu yapabilirsin FAKAT AK-47 her bir FRAME 'de sürekli hesaplanacaðý için ve SÜREKLÝ  VE SIK MERMÝ sýkacaðýmýz için  bu ÇOK BÜYÜK BÝR PERFORMANS KAYBINA NEDEN OLUR ! ! !

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // BAzen bazý yerlerde Collider OLMAYABÝLÝR O YÜZDEN bu kodu Start metodunda YAZKÝ ÝÞÝN GARANTÝ OLSUN !!!!!!!
    }

}
