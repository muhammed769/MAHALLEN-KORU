using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keles : MonoBehaviour
{

   #region Deðiþkenler

    Animator my_Animators;

    [Header("AYARLAR")]

    public bool atesEdebilirmi;
    private float IceridenatesEtmeSikligi; //  Silah ateþlenirken tatlý bir boþluk olmasý için. 
    public float DisaridanAtesETmeSiklik; // Bunu dýþarýdan vericez çünkü her silaha göre deðiþebilir. [ Çünkü birden fazla silah kullanýcaz. ]
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


    [Header("DÝGERLERÝ")]
    public Camera benimCameram; // ÖNEMLÝ !!!!!!!! =  Hierarchy'deki FirsPersonCharacter objesini SÜRÜKLE.

    [Header("SÝLAH AYARLARI")]
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

        // Ateþ edilip edilmediðini anlamam lazým.Yani þöyle :

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > IceridenatesEtmeSikligi && KalanMermi != 0)
            {
                AtesEt();
                IceridenatesEtmeSikligi = Time.time + DisaridanAtesETmeSiklik; // silah sýkmalarý arasýndaki belli bir boþluk olmasýný isteriz iþte bu kod o iþe yarar.   
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

        // SetTrigger() , SetBool() gibi parametre ekleyerekte  yapabilirsin ancak animasyonumuz silah tepme ve þarjor deðiþtirme gibi SADECE 2 tane amimasyon olduðu için böyle bir þey yaptýk.

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

        /* if (!SarjorDegistirme.isPlaying) // ÞarjorDegistirme Sesim ÇALMIYORSA  bana þunlarý yap : 
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
                Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));// LookRotation()  : Kameraya bakan AÇISINDA ve noktasýnda mermi izini oluþturur. hit.normal = vektörü açýsal olarak 0 ila 1 arsaýnda ayarlar.
            }
        }
    }

    // Instantiate(MermiIzi, hit.point, Quaternion.LookRotation(hit.normal));// LookRotation()  : Kameraya bakan AÇISINDA ve noktasýnda mermi izini oluþturur. hit.normal = vektörü açýsal olarak 0 ila 1 arsaýnda ayarlar.
    // hit.normal = Bize bakan yönüne göre iþlem yapar.

    #endregion



    private void OnTriggerEnter(Collider other)
    {
         
        // Bu 2.YÖNTEM : Mermi Kutusuna temas edildiðinde MERMÝ KUTUSUYLA ETKÝLEÞÝM KURABÝLDÝK.

        if (other.gameObject.CompareTag("Mermi")) // MermiKutusuna Mermi Tag'ýný Vermiþtik.
        {
            MermiKaydet(other.transform.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, other.transform.GetComponent<MermiKutusu>().Olusan_Mermi_Sayisi);
            Mermi_Kutusu_Olustur.Mermi_Kutusu_Varmi = false;
            Destroy(other.transform.parent.gameObject);
        }

    }


    #region Mermi AL Function

    void MermiAL()
    {

        // Bu 1.YÖNTEM : YANÝ CROSSHAÝR MermiKutusunun üzerinde DURDUÐUNDA E TUÞUNA BASARSAK MERMÝ KUTUSUYLA ETKÝLEÞÝM KURABÝLDÝK.

        
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

    void SarjorDoldurmaTeknikFonksiyonu(string tur) // String türünde bir parametre tanýmladýk YANÝ BURDA sarjor doldurma iþlemini NASILLL YAPACAÐINA KARAR VERMESÝNÝ ÝSTÝYORUZ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        switch (tur)
        {
            case "MermiVar":

                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                   int OlusanToplamDeger =  KalanMermi + ToplamMermiSayisi; //   // 3  // 5  + // 8 // þarjor kapasitesini aþmamasý gerekiyor bunada DÝKKAT ETMEMÝZ GEREKÝYOR.

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
            rb.AddRelativeForce(new Vector3(-10f, 1, 0) * 60); // KOVANIN HER ZAMAN SAÐA DOÐRU ÇIKMASINI SAÐLAMIÞ OLDUK ! ! ! ! ! ! ! 

        }

        AtesSesi.Play();
        AtesEfekt.Play();
        my_Animators.Play("atesEt");

        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();
    }

    #endregion

}
