
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Ak47 : MonoBehaviour
{

    #region Değişkenler 
    Animator animatorum;

    [Header("AYARLAR")]
    public bool atesEdebilirmi;
    float İceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public float menzil;
    public GameObject Cross;

    [Header("SESLER")]
    public AudioSource AtesSesi;
    public AudioSource SarjorDegirme;
    public AudioSource MermiBittiSes;
    public AudioSource MermiAlmaSesi;


    [Header("EFEKTLER")]
    public ParticleSystem AtesEfekt;
    public ParticleSystem Mermiİzi;
    public ParticleSystem KanEfekti;

    [Header("DİGERLERİ")]
    public Camera benimCam;
    float camFieldPov;
    public float YaklasmaPov;

    [Header("SİLAH AYARLAR")]
    public float DarbeGucu;
    int ToplamMermiSayisi;
    public int SarjorKapasitesi;
    int KalanMermi;
    public string Silahin_adi;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;
    bool zoomvarmi;

    [Header("EKLENTİLER")]
    public bool Kovan_ciksinmi;
    public GameObject Kovan_Cikis_noktasi;    
    public GameObject Kovan_objesi;

    public Mermi_Kutusu_olustur Mermi_Kutusu_olustur_yonetim;

    #endregion

    void Start()
    {
        ToplamMermiSayisi = PlayerPrefs.GetInt(Silahin_adi + "_Mermi");
        Kovan_ciksinmi = true;
        Baslangic_mermi_doldur();        
        SarjordoldurmaTeknikFonksiyon("NormalYaz");
        animatorum = GetComponent<Animator>();
        camFieldPov = benimCam.fieldOfView;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0)&& ! Input.GetKey(KeyCode.Mouse1))
        {
            //  Debug.Log("Tıklandı !");

            if (atesEdebilirmi && Time.time > İceridenAtesEtmeSikligi && KalanMermi != 0)
            {
                AtesEt(false);
                İceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSikligi;

            }

            if (KalanMermi == 0)
            {
                MermiBittiSes.Play();
            }

        }
    

        if (Input.GetKey(KeyCode.R))
        {
            if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
            {
                animatorum.Play("sarjordegistir");

            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            MermiAL();
        }



        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            zoomvarmi = true;
            animatorum.SetBool("zoomyap", true);

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            zoomvarmi = false;
            animatorum.SetBool("zoomyap", false);
            benimCam.fieldOfView = camFieldPov;
            Cross.SetActive(true);

        }

        if (zoomvarmi)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
              //  Debug.Log("Tıklandı !");

               if (atesEdebilirmi && Time.time > İceridenAtesEtmeSikligi && KalanMermi != 0)
                {
                    AtesEt(true);
                    İceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSikligi;

                }

                if (KalanMermi == 0)
                {
                    MermiBittiSes.Play();
                }

            }
        }
    }


  
    void AtesEt(bool yakinlasmavarmi)
    {
        AtesEtmeTeknikİslemleri(yakinlasmavarmi);

        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {

            if (hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(KanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.gameObject.GetComponent<Dusman_>().DarbeAl(DarbeGucu);
    
            }
            else if (hit.transform.gameObject.CompareTag("DevrilebilirObje"))
            {

                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 50f);
                Instantiate(Mermiİzi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(Mermiİzi, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }

    }

    void AtesEtmeTeknikİslemleri(bool yakinlasmavarmi)
    {
        if (Kovan_ciksinmi)
        {
            GameObject obje = Instantiate(Kovan_objesi, Kovan_Cikis_noktasi.transform.position, Kovan_Cikis_noktasi.transform.rotation);
            Rigidbody rb = obje.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(-10f, 1, 0) * 60);

        }


        StartCoroutine(CameraTitre(0.05f, 0.1f));

        AtesSesi.Play();
        AtesEfekt.Play();

        if (!yakinlasmavarmi)
        {
            animatorum.Play("ateset");

        }
        else
        {
            animatorum.Play("zoomAteset");
        }



        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();


    }

    void KameraYaklastir()
    {
        benimCam.fieldOfView = YaklasmaPov;
        Cross.SetActive(false);

    }

    IEnumerator CameraTitre(float titremesuresi, float magnitude) // magnitude = büyüklük ( Karekök ve karesini veriyordu hatırla.)
    {
        Vector3 orijinalpozisyon = benimCam.transform.localPosition;

        float gecensure = 0.0f;

        while (gecensure < titremesuresi)
        {
            float x = Random.Range(-1f, 1) * magnitude;  // Random.Range 'i görebilmen için Using System'i EN ÜSTTE SİLMEN LAZIM !

            benimCam.transform.localPosition = new Vector3(x, orijinalpozisyon.y, orijinalpozisyon.x);  // y kısmında da z kısmındada kullanabilirdik TERCİHEN x pozisyonunu SEÇTİK.

            gecensure += Time.deltaTime; // Yani Camera 1 KEZ titriyor ve gecensure'yi artırıyorum.

            yield return null;

        }

        benimCam.transform.localPosition = orijinalpozisyon;

        //  => AtesetmeTeknikişlemleri 
    }


    void MermiAL()
    {

        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, 4))
        {

            if (hit.transform.gameObject.CompareTag("Mermi"))
            {

                MermiKaydet(hit.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, hit.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_sayisi);
                Mermi_Kutusu_olustur_yonetim.NoktalarinKaldirma(hit.transform.gameObject.GetComponent<MermiKutusu>().Noktasi);
                Destroy(hit.transform.parent.gameObject);
              

            }

        }


    }

    void Baslangic_mermi_doldur()
    {

        if (ToplamMermiSayisi <= SarjorKapasitesi)
        {
            KalanMermi = ToplamMermiSayisi;
            ToplamMermiSayisi = 0;
            PlayerPrefs.SetInt(Silahin_adi + "_Mermi", 0);
        }
        else
        {
            KalanMermi = SarjorKapasitesi;
            ToplamMermiSayisi -= SarjorKapasitesi;
            PlayerPrefs.SetInt(Silahin_adi + "_Mermi", ToplamMermiSayisi);

        }

    }

    void MermiKaydet(string silahturu, int mermisayisi)
    {
        /*  PlayerPrefs.SetInt("Pompali_Mermi", 50);
             PlayerPrefs.SetInt("Magnum_Mermi", 30);
             PlayerPrefs.SetInt("Sniper_Mermi", 20);*/
        MermiAlmaSesi.Play();

        switch (silahturu)
        {
            case "Taramali":

                ToplamMermiSayisi += mermisayisi;
                PlayerPrefs.SetInt(Silahin_adi + "_Mermi", ToplamMermiSayisi);
                SarjordoldurmaTeknikFonksiyon("NormalYaz");

                break;

            case "Pompali":
                PlayerPrefs.SetInt("Pompali_Mermi", PlayerPrefs.GetInt("Pompali_Mermi") + mermisayisi);
                break;

            case "Magnum":
                PlayerPrefs.SetInt("Magnum_Mermi", PlayerPrefs.GetInt("Magnum_Mermi") + mermisayisi);
                break;

            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Mermi", PlayerPrefs.GetInt("Sniper_Mermi") + mermisayisi);
                break;


        }

    }
 
    void SarjordoldurmaTeknikFonksiyon(string tur)
    {
        switch (tur)
        {
            case "MermiVar":

                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                    int OlusanToplamDeger = KalanMermi + ToplamMermiSayisi;
                    
                    if (OlusanToplamDeger > SarjorKapasitesi)
                    {
                        KalanMermi = SarjorKapasitesi;
                        ToplamMermiSayisi = OlusanToplamDeger - SarjorKapasitesi;
                        PlayerPrefs.SetInt(Silahin_adi + "_Mermi", ToplamMermiSayisi);

                    }
                    else
                    {
                        KalanMermi += ToplamMermiSayisi;
                        ToplamMermiSayisi = 0;
                        PlayerPrefs.SetInt(Silahin_adi + "_Mermi", 0);
                    }


                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi - KalanMermi;
                    KalanMermi = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silahin_adi + "_Mermi", ToplamMermiSayisi);
                }

                ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
                KalanMermi_Text.text = KalanMermi.ToString();
                break;

            case "MermiYok":
                if (ToplamMermiSayisi <= SarjorKapasitesi)
                {
                    KalanMermi = ToplamMermiSayisi;

                    ToplamMermiSayisi = 0;
                    PlayerPrefs.SetInt(Silahin_adi + "_Mermi", 0);
                }
                else
                {
                    ToplamMermiSayisi -= SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silahin_adi + "_Mermi", ToplamMermiSayisi);
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

  
    void Sarjordegistir()
    {
        SarjorDegirme.Play();

        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            if (KalanMermi != 0)
            {
                SarjordoldurmaTeknikFonksiyon("MermiVar");
            }
            else
            {
                SarjordoldurmaTeknikFonksiyon("MermiYok");
            }

        }

    }

    /* IEnumerator ReloadYap()
     {
         if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
         {
             animatorum.Play("sarjordegistir");
         }
         yield return new WaitForSeconds(.5f);



     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
            MermiKaydet(other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_sayisi);
            Mermi_Kutusu_olustur_yonetim.NoktalarinKaldirma(other.transform.gameObject.GetComponent<MermiKutusu>().Noktasi);
            Destroy(other.transform.parent.gameObject);

        }

        if (other.gameObject.CompareTag("Can_kutusu"))
        {

            Mermi_Kutusu_olustur_yonetim.GetComponent<GameKontrolcu>().Saglik_doldur();

            Health_Kutusu_olustur.Health_kutusu_varmi = false;
            Destroy(other.transform.gameObject);

        }


    }







}
