using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameKontrolcu : MonoBehaviour
{

   #region Değişkenler

    int aktifsira;
    float health = 100;

    [Header("SAĞLIK AYARLARI")]
    public Image HealthBar;


    [Header("SİLAH AYARLARI")]
    public GameObject[] silahlar;
    public AudioSource degisimSesi;
    public GameObject Bomba;
    public GameObject BombaPoint; // Bomba Cıkış noktası
    public Camera benimCam; // Bombanın Gideceği Yön


    [Header("DUSMAN AYARLARI")]
    public GameObject[] dusmanlar;
    public GameObject[] cikisNoktalari;
    public GameObject[] hedefNoktalar;
    public float DusmancikmaSuresi;
    public TextMeshProUGUI KalanDusman_text;
    public int Baslangic_dusman_sayisi;
    public static int Kalan_dusman_sayisi;

    [Header("DİĞER AYARLAR")]
    public GameObject GameOverCanvas;
    public GameObject KazandınCanvas;
    public GameObject PauseCanvas;
    public AudioSource OyunIcSes;
    public TextMeshProUGUI Saglik_sayisi_text;
    public TextMeshProUGUI Bomba_sayisi_text;
    public AudioSource ItemYok;

    public static bool OyunDurdumu;
    #endregion



    void Start()
    {
        BaslangicIslemleri();
    }

    void BaslangicIslemleri()
    {
        OyunDurdumu = false;

        if (!PlayerPrefs.HasKey("OyunBasladimi"))
        {
            PlayerPrefs.SetInt("Taramali_Mermi", 70);
            PlayerPrefs.SetInt("Pompali_Mermi", 50);
            PlayerPrefs.SetInt("Magnum_Mermi", 30);
            PlayerPrefs.SetInt("Sniper_Mermi", 20);

            PlayerPrefs.SetInt("Saglik_sayisi", 1);
            PlayerPrefs.SetInt("Bomba_sayisi", 5);

            PlayerPrefs.SetInt("OyunBasladimi", 1);

        }

        KalanDusman_text.text = Baslangic_dusman_sayisi.ToString();
        Kalan_dusman_sayisi = Baslangic_dusman_sayisi;

        Saglik_sayisi_text.text = PlayerPrefs.GetInt("Saglik_sayisi").ToString();
        Bomba_sayisi_text.text = PlayerPrefs.GetInt("Bomba_sayisi").ToString();

        aktifsira = 0;

        OyunIcSes = GetComponent<AudioSource>();
        OyunIcSes.Play();

        StartCoroutine(DusmanCıkar());
      
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && ! OyunDurdumu)// Alpha 0 1 2 = Klavyenin üstündeki sayısal rakamlardır. Keypad 0 1 2 = ise Klavyenin sağ taraftaki bulunan sayısal rakamlardır.
        {
            Silahdegistir(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !OyunDurdumu)
        {
            Silahdegistir(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !OyunDurdumu)
        {
            Silahdegistir(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && !OyunDurdumu)
        {
            Silahdegistir(3);
        }


        if (Input.GetKeyDown(KeyCode.Q )&& ! OyunDurdumu)
        {
            QtusuVersiyonuSilahdegistir();

        }

        if (Input.GetKeyDown(KeyCode.G) && !OyunDurdumu)
        {
            BombaAt();
        }

        if (Input.GetKeyDown(KeyCode.E) && !OyunDurdumu)
        {
            Saglik_doldur();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && ! OyunDurdumu)
        {
            Pause();
        }


    }

    public void DarbeAl(float darbegucu) // Düşmanlarında Darbe güçleri farklı olabilir dimi ?
    {
        health -= darbegucu;
        HealthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            GameOver();
        }

    }


    IEnumerator DusmanCıkar()
    {
        while (true)
        {
            yield return new WaitForSeconds(DusmancikmaSuresi);

            if (Baslangic_dusman_sayisi != 0)
            {
                int dusman = Random.Range(0, 5);
                int cikisnoktasi = Random.Range(0, 2);
                int hedefnoktasi = Random.Range(0, 2);

                GameObject Obje = Instantiate(dusmanlar[dusman], cikisNoktalari[cikisnoktasi].transform.position, Quaternion.identity);
                Obje.GetComponent<Dusman_>().HedefBelirle(hedefNoktalar[hedefnoktasi]);
                Baslangic_dusman_sayisi--;
            }
        }

    }


    public void Dusman_sayisi_guncelle()
    {
       
            Kalan_dusman_sayisi--;

            if (Kalan_dusman_sayisi <= 0)
            {
                KazandınCanvas.SetActive(true);
                KalanDusman_text.text = "0";
                Time.timeScale = 0;
            }

            else
            {

                KalanDusman_text.text = Kalan_dusman_sayisi.ToString();
            }
        
     
 
    }

    public void Saglik_doldur()
    {
        if (PlayerPrefs.GetInt("Saglik_sayisi") !=0 && health != 100)
        {
            health = 100;
            HealthBar.fillAmount = health / 100;
            PlayerPrefs.SetInt("Saglik_sayisi", PlayerPrefs.GetInt("Saglik_sayisi") - 1);
            Saglik_sayisi_text.text = PlayerPrefs.GetInt("Saglik_sayisi").ToString();
        }
        else
        {
            ItemYok.Play();
        }
    }

    public void Saglik_Al()
    {
       PlayerPrefs.SetInt("Saglik_sayisi", PlayerPrefs.GetInt("Saglik_sayisi") + 1);
        Saglik_sayisi_text.text = PlayerPrefs.GetInt("Saglik_sayisi").ToString();
        
    }

    public void Bomba_Al()
    {
        PlayerPrefs.SetInt("Bomba_sayisi", PlayerPrefs.GetInt("Bomba_sayisi") + 1);
        Bomba_sayisi_text.text = PlayerPrefs.GetInt("Bomba_sayisi").ToString();
    }

    void BombaAt()
    {

        if (PlayerPrefs.GetInt("Bomba_sayisi") != 0)
        {
            GameObject obje = Instantiate(Bomba, BombaPoint.transform.position, Bomba.transform.rotation);
            Rigidbody rg = obje.GetComponent<Rigidbody>();

            // NOT : Bombaya AÇISAL BİR KUVVET VERMEN LAZIM yoksa bombayı attığında  mermi gibi gider. ŞÖYLE YAPICAZ : 

            Vector3 acimiz = Quaternion.AngleAxis(90, benimCam.transform.forward) * benimCam.transform.forward;  // 1 ) Açı derecesini belirttik  2 ) Bu açıyı BenimCameramdan İLERİYE göre hesaplayacağını söyledik ,                                                                                                             // 3 ) BenimCameramdan İLERİYE  doğru bir REFERANS ALMASINI SÖYLEDİK !!!!!!!!!!!!!
            rg.AddForce(acimiz * 250f);


            PlayerPrefs.SetInt("Bomba_sayisi", PlayerPrefs.GetInt("Bomba_sayisi") - 1);
            Bomba_sayisi_text.text = PlayerPrefs.GetInt("Bomba_sayisi").ToString();
        }

        else
        {
            ItemYok.Play();
        }



    }


    void Silahdegistir(int siraNumarasi)
    {
        degisimSesi.Play();

        foreach (GameObject silah in silahlar) // For döngüsü ilede yapabilirdik.
        {
            silah.SetActive(false);
        }

        aktifsira = siraNumarasi;  // ÖNEMLİ : 2 Farklı yöntemdeki sistemimiz biribirinden BAĞIMSIZ çalışıyor.Bunlar birbirleriyle nasıl koordineli çalışır ? İşte şöyle :  Silahdeğiştir fonksiyonunda aktifsira=siranumarasi yazarak.

        silahlar[siraNumarasi].SetActive(true);

    }

    void QtusuVersiyonuSilahdegistir()
    {
        degisimSesi.Play();

        foreach (GameObject silah in silahlar)
        {
            silah.SetActive(false);
        }

        if (aktifsira == 3)
        {
            aktifsira = 0;
            silahlar[aktifsira].SetActive(true);
        }
        else
        {
            aktifsira++;
            silahlar[aktifsira].SetActive(true);
        }




    }


    public void BastanBasla()
    {
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().name);    // 1.YOL : İlgili sahnenin tekrar yüklenilmesi SAĞLANDI.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // 2.YOL : İlgili sahnenin tekrar yüklenilmesi SAĞLANDI.
        Time.timeScale = 1;
        OyunDurdumu = false;

        Cursor.visible = false;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0; // Oyun Durur.
        OyunDurdumu = true;
        // => => Update ()

        Cursor.visible = true; // Cursor : Maus imleci 

        // ÖNEMLİ  MUTLAKA OKU !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;

                                                                               // 1 ) FirstPersonController Scriptine ULAŞABİLMEN İÇİN   FirstPersonController Scriptini açıp KENDİ NAME SPACE 'İNİ ALIP BU SCRİPTE KOPYALA.
                                                                               // 2 ) Daha SONRA  FirstPersonController scriptine git ordaki Maouse Look değişkenini PUBLİC olarak DEĞİŞTİR YOKSA ERİŞEMEZSİN !!!!!!!!

        Cursor.lockState = CursorLockMode.None; // Maus 'un kilidini kaldırıyoruz çünkü herhangi bir ÇAKIŞMA olmaması için. GARANTİ OLMASI AÇISINDAN YAZDIK !!!!!!!!

      


    }
    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
        // => => Update ()
        OyunDurdumu = true;

        Cursor.visible = true; 
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DevamEt()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        OyunDurdumu = false;
        // => => Update ()

        Cursor.visible = false;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void anaMenu()
    {
        SceneManager.LoadScene(0);

    }


}
