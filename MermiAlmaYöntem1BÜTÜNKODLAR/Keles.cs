using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keles : MonoBehaviour
{

   #region De�i�kenler

    Animator my_Animators;

    [Header("AYARLAR")]

    public bool atesEdebilirmi;
    private float IceridenatesEtmeSikligi; //  Silah ate�lenirken tatl� bir bo�luk olmas� i�in. 
    public float DisaridanAtesETmeSiklik; // Bunu d��ar�dan vericez ��nk� her silaha g�re de�i�ebilir. [ ��nk� birden fazla silah kullan�caz. ]
    public float menzil;


    [Header("SESLER")]
    public AudioSource AtesSesi;
    public AudioSource SarjorDegistirmeSesi;
    public AudioSource MermiBitisSesi;
    public AudioSource MermiAlmaSesi;

    [Header("EFEKTLER")]
    public ParticleSystem AtesEfekt;
    public ParticleSystem MermiIzi;
    public ParticleSystem KanEfekti;


    [Header("D�GERLER�")]
    public Camera benimCameram; // �NEML� !!!!!!!! =  Hierarchy'deki FirsPersonCharacter objesini S�R�KLE.

    [Header("S�LAH AYARLARI")]
    public int ToplamMermiSayisi;
    public int SarjorKapasitesi;
    public int KalanMermi;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;

    public bool Kovan_ciksinmi;
    public GameObject Kovan_Objesi;
    public GameObject Kovan_Cikis_Noktasi;



    #endregion

    void Start()
    {
        Kovan_ciksinmi = true;
        KalanMermi = SarjorKapasitesi;
        SarjorDoldurmaTeknikFonksiyonu("NormalYaz");

        my_Animators = gameObject.GetComponent<Animator>();

       
    }


    #region  Update 
    void Update()
    {

        // Ate� edilip edilmedi�ini anlamam laz�m.Yani ��yle :

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > IceridenatesEtmeSikligi && KalanMermi != 0)
            {
                AtesEt();
                IceridenatesEtmeSikligi = Time.time + DisaridanAtesETmeSiklik; // silah s�kmalar� aras�ndaki belli bir bo�luk olmas�n� isteriz i�te bu kod o i�e yarar.   
            }

            if (KalanMermi <= 0)
            {
                MermiBitisSesi.Play();
            }


        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(ReloadYap());
        }

        // SetTrigger() , SetBool() gibi parametre ekleyerekte  yapabilirsin ancak animasyonumuz silah tepme ve �arjor de�i�tirme gibi SADECE 2 tane amimasyon oldu�u i�in b�yle bir �ey yapt�k.

        if (Input.GetKeyDown(KeyCode.E))
        {
            MermiAL();
        }

    }


    #endregion


    #region Reload Yap

    IEnumerator ReloadYap()
    {
        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            my_Animators.Play("SarjorDegistirme");
        }
        yield return new WaitForSeconds(.4f);

        /* if (!SarjorDegistirme.isPlaying) // �arjorDegistirme Sesim �ALMIYORSA  bana �unlar� yap : 
         {
             SarjorDegistirme.Play();
         }*/
        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            if (KalanMermi != 0)
            {
                SarjorDoldurmaTeknikFonksiyonu("MermiVar");
            }

            else
            {
                SarjorDoldurmaTeknikFonksiyonu("MermiYok");
            }


        }
    }


    #endregion 


    #region ATES ET Function
    void AtesEt()
    {
        AtesEtmeTeknikIslemleri();

        RaycastHit hit;

        if (Physics.Raycast(benimCameram.transform.position, benimCameram.transform.forward, out hit, menzil))
        {

         //   Debug.Log(hit.transform.name);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(KanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            }

            else if (hit.transform.gameObject.CompareTag("DevrilebilirObje"))
            {
                Rigidbody Rg = hit.transform.gameObject.GetComponent<Rigidbody>();
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

    #endregion



    private void OnTriggerEnter(Collider other)
    {
         
        // Bu 2.Y�NTEM : Mermi Kutusuna temas edildi�inde MERM� KUTUSUYLA ETK�LE��M KURAB�LD�K.

        if (other.gameObject.CompareTag("Mermi")) // MermiKutusuna Mermi Tag'�n� Vermi�tik.
        {
            MermiKaydet(other.transform.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, other.transform.GetComponent<MermiKutusu>().Olusan_Mermi_Sayisi);
            Mermi_Kutusu_Olustur.Mermi_Kutusu_Varmi = false;
            Destroy(other.transform.parent.gameObject);
        }

    }


    #region Mermi AL Function

    void MermiAL()
    {

        // Bu 1.Y�NTEM : YAN� CROSSHA�R MermiKutusunun �zerinde DURDU�UNDA E TU�UNA BASARSAK MERM� KUTUSUYLA ETK�LE��M KURAB�LD�K.

        
        RaycastHit hit;
       

        if (Physics.Raycast(benimCameram.transform.position, benimCameram.transform.forward, out hit, 6))
        {
            
            if (hit.transform.gameObject.CompareTag("Mermi"))
            {               
                MermiKaydet(hit.transform.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, hit.transform.GetComponent<MermiKutusu>().Olusan_Mermi_Sayisi);
                Mermi_Kutusu_Olustur.Mermi_Kutusu_Varmi = false;
                Destroy(hit.transform.parent.gameObject);
            }
        }
    }
    #endregion


    #region Mermi KAYDET


    void MermiKaydet(string silahturu, int mermisayisi)
    {
        MermiAlmaSesi.Play();
        switch (silahturu)
        {
            case "Taramali":

                ToplamMermiSayisi += mermisayisi;
                SarjorDoldurmaTeknikFonksiyonu("NormalYaz");

                break;


            case "Pompali":

                break;


            case "Magnum":

                break;


            case "Sniper":

                break;

        }
    }
    #endregion


    #region Sarjor Doldurma Teknik Fonksiyonu

    void SarjorDoldurmaTeknikFonksiyonu(string tur) // String t�r�nde bir parametre tan�mlad�k YAN� BURDA sarjor doldurma i�lemini NASILLL YAPACA�INA KARAR VERMES�N� �ST�YORUZ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        switch (tur)
        {
            case "MermiVar":

                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                   int OlusanToplamDeger =  KalanMermi + ToplamMermiSayisi; //   // 3  // 5  + // 8 // �arjor kapasitesini a�mamas� gerekiyor bunada D�KKAT ETMEM�Z GEREK�YOR.

                    if (OlusanToplamDeger > SarjorKapasitesi)
                    {
                        KalanMermi = SarjorKapasitesi;
                      ToplamMermiSayisi =   OlusanToplamDeger - SarjorKapasitesi;
                    }

                    else
                    {
                        KalanMermi += ToplamMermiSayisi;
                        ToplamMermiSayisi = 0;
                    }
                   
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi - KalanMermi;
                    KalanMermi = SarjorKapasitesi;
                }

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "MermiYok":

                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                    KalanMermi = ToplamMermiSayisi;

                    ToplamMermiSayisi = 0;
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi;
                    KalanMermi = SarjorKapasitesi;
                }

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "NormalYaz":

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;
        }
    }

    #endregion


    void SarjorDegistir()
    {
        SarjorDegistirmeSesi.Play();
    }


    #region AtesEtmeTEknikIslemleri

    void AtesEtmeTeknikIslemleri()
    {
        if (Kovan_ciksinmi)
        {
            GameObject obje = Instantiate(Kovan_Objesi, Kovan_Cikis_Noktasi.transform.position, Kovan_Cikis_Noktasi.transform.rotation);
            Rigidbody rb = obje.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(-10f, 1, 0) * 60); // KOVANIN HER ZAMAN SA�A DO�RU �IKMASINI SA�LAMI� OLDUK ! ! ! ! ! ! ! 

        }

        AtesSesi.Play();
        AtesEfekt.Play();
        my_Animators.Play("atesEt");

        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();
    }

    #endregion

}
