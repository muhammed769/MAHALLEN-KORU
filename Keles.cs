using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keles : MonoBehaviour
{
    public bool atesEdebilirmi;
    private float IceridenatesEtmeSikligi; //  Silah ate�lenirken tatl� bir b�o�luk olmas� i�in. 
    public float DisaridanAtesETmeSiklik; // Bunu d��ar�dan vericez ��nk� her silaha g�re de�i�ebilir. [ ��nk� birden fazla silah kullan�caz. ]
    public float menzil;

    public Camera benimCameram; // �NEML� !!!!!!!! =  H�erarchy'deki FirsPersonCharacter objesini S�R�KLE.
    public AudioSource AtesSesi;
    public ParticleSystem AtesEfekt;

    public ParticleSystem MermiIzi;
    public ParticleSystem KanEfekti;


    void Start()
    {
        
    }

  
    void Update()
    {

        // Ate� edilip edilmedi�ini anlamam laz�m.Yani ��yle :

        if (Input.GetKey(KeyCode.Mouse0) && atesEdebilirmi && Time.time>IceridenatesEtmeSikligi)
        {
            AtesEt();
            IceridenatesEtmeSikligi = Time.time + DisaridanAtesETmeSiklik; // silah s�kmalar� aras�ndaki belli bir bo�luk olmas�n� isteriz i�te bu kod o i�e yarar.
        }

    }

    void AtesEt()
    {
        AtesSesi.Play();
        AtesEfekt.Play();


        RaycastHit hit;

        if(Physics.Raycast(benimCameram.transform.position,benimCameram.transform.forward,out hit, menzil))
        {

            Debug.Log(hit.transform.name);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(KanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            else if (hit.transform.gameObject.CompareTag("DevrilebilirObje"))
            {
               Rigidbody  Rg= hit.transform.gameObject.GetComponent<Rigidbody>();
                // Rg.AddForce(new Vector3(2f, 1f, 3f) * 20f);
                //Rg.AddForce(transform.forward * 450f);

                Rg.AddForce(-hit.normal * 45f); //  Optimal Compatible


               Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
                
            }
            else
            {
                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));// LookRotation()  : Kameraya bakan A�ISINDA ve noktas�nda mermi izini olu�turur. hit.normal = vekt�r� a��sal olarak 0 ila 1 arsa�nda ayarlar.
            }
        }

    }

       // Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));// LookRotation()  : Kameraya bakan A�ISINDA ve noktas�nda mermi izini olu�turur. hit.normal = vekt�r� a��sal olarak 0 ila 1 arsa�nda ayarlar.
       // hit.normal = Bize bakan y�n�ne g�re i�lem yapar.
    
}
