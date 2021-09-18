using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keles : MonoBehaviour
{
    public bool atesEdebilirmi;
    private float IceridenatesEtmeSikligi; //  Silah ateþlenirken tatlý bir böoþluk olmasý için. 
    public float DisaridanAtesETmeSiklik; // Bunu dýþarýdan vericez çünkü her silaha göre deðiþebilir. [ Çünkü birden fazla silah kullanýcaz. ]
    public float menzil;

    public Camera benimCameram; // ÖNEMLÝ !!!!!!!! =  HÝerarchy'deki FirsPersonCharacter objesini SÜRÜKLE.
    public AudioSource AtesSesi;
    public ParticleSystem AtesEfekt;

    public ParticleSystem MermiIzi;
    public ParticleSystem KanEfekti;


    void Start()
    {
        
    }

  
    void Update()
    {

        // Ateþ edilip edilmediðini anlamam lazým.Yani þöyle :

        if (Input.GetKey(KeyCode.Mouse0) && atesEdebilirmi && Time.time>IceridenatesEtmeSikligi)
        {
            AtesEt();
            IceridenatesEtmeSikligi = Time.time + DisaridanAtesETmeSiklik; // silah sýkmalarý arasýndaki belli bir boþluk olmasýný isteriz iþte bu kod o iþe yarar.
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
                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));// LookRotation()  : Kameraya bakan AÇISINDA ve noktasýnda mermi izini oluþturur. hit.normal = vektörü açýsal olarak 0 ila 1 arsaýnda ayarlar.
            }
        }

    }

       // Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));// LookRotation()  : Kameraya bakan AÇISINDA ve noktasýnda mermi izini oluþturur. hit.normal = vektörü açýsal olarak 0 ila 1 arsaýnda ayarlar.
       // hit.normal = Bize bakan yönüne göre iþlem yapar.
    
}
