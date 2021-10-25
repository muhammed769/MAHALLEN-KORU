﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Pompali : MonoBehaviour
{


   #region Değişkenler 

    Animator animatorum;

    [Header("AYARLAR")]
    public bool atesEdebilirmi;
    float İceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public float menzil;

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
    int ToplamMermiSayisi;
    public int SarjorKapasitesi;
    int KalanMermi;
    public string Silahin_adi;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;
    bool zoomvarmi;

    public bool Kovan_ciksinmi;
    public GameObject Kovan_Cikis_noktasi;    
    public GameObject Kovan_objesi;

    public Mermi_Kutusu_olustur Mermi_Kutusu_olustur_yonetim;

    #endregion

    void Start()
    {
        ToplamMermiSayisi = PlayerPrefs.GetInt(Silahin_adi + "_Mermi");
        Kovan_ciksinmi = false;
        Baslangic_mermi_doldur();        
        SarjordoldurmaTeknikFonksiyon("NormalYaz");
        animatorum = GetComponent<Animator>();
        camFieldPov = benimCam.fieldOfView;
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && ! Input.GetKey(KeyCode.Mouse1))
        {

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
            StartCoroutine(ReloadYap());
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


    void KameraYaklastir()
    {
        benimCam.fieldOfView = YaklasmaPov;

    }

   

    void AtesEt(bool yakinlasmavarmi)
    {
        AtesEtmeTeknikİslemleri( yakinlasmavarmi);

        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {

            if (hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(KanEfekti, hit.point, Quaternion.LookRotation(hit.normal));


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

    void MermiKaydet(string silahturu, int mermisayisi)
    {
        /*  PlayerPrefs.SetInt("Pompali_Mermi", 50);
             PlayerPrefs.SetInt("Magnum_Mermi", 30);
             PlayerPrefs.SetInt("Sniper_Mermi", 20);*/
        MermiAlmaSesi.Play();

        switch (silahturu)
        {
            case "Taramali":
                PlayerPrefs.SetInt("Taramali_Mermi", PlayerPrefs.GetInt("Taramali_Mermi") + mermisayisi);
                break;

            case "Pompali":
                ToplamMermiSayisi += mermisayisi;
                PlayerPrefs.SetInt(Silahin_adi + "_Mermi", ToplamMermiSayisi);
                SarjordoldurmaTeknikFonksiyon("NormalYaz");
                break;

            case "Magnum":
                PlayerPrefs.SetInt("Magnum_Mermi", PlayerPrefs.GetInt("Magnum_Mermi") + mermisayisi);
                break;

            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Mermi", PlayerPrefs.GetInt("Sniper_Mermi") + mermisayisi);
                break;


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

    }


    IEnumerator ReloadYap()
    {
        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            animatorum.Play("sarjordegistir");

        }
        yield return new WaitForSeconds(.5f);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
            MermiKaydet(other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Silahin_Turu, other.transform.gameObject.GetComponent<MermiKutusu>().Olusan_Mermi_sayisi);
            Mermi_Kutusu_olustur_yonetim.NoktalarinKaldirma(other.transform.gameObject.GetComponent<MermiKutusu>().Noktasi);
            Destroy(other.transform.parent.gameObject);

        }
    }

}
