using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * -250f; // Transform.Translate ilede bunu yapabilirsin FAKAT AK-47 her bir FRAME 'de s�rekli hesaplanaca�� i�in ve S�REKL�  VE SIK MERM� s�kaca��m�z i�in  bu �OK B�Y�K B�R PERFORMANS KAYBINA NEDEN OLUR ! ! !

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // BAzen baz� yerlerde Collider OLMAYAB�L�R O Y�ZDEN bu kodu Start metodunda YAZK� ���N GARANT� OLSUN !!!!!!!
    }

}
