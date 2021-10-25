using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("DUSMAN AYARLARI")]
    public GameObject[] dusmanlar;
    public GameObject[] cikisNoktalari;
    public GameObject[] hedefNoktalar;
    public float  DusmancikmaSuresi;


    #endregion



    void Start()
    {
        aktifsira = 0;
     
        if (!PlayerPrefs.HasKey("OyunBasladimi"))
        {
            PlayerPrefs.SetInt("Taramali_Mermi", 70);
            PlayerPrefs.SetInt("Pompali_Mermi", 50);
            PlayerPrefs.SetInt("Magnum_Mermi", 30);
            PlayerPrefs.SetInt("Sniper_Mermi", 20);

            PlayerPrefs.SetInt("OyunBasladimi", 1);

        }

        StartCoroutine(DusmanCıkar());

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))// Alpha 0 1 2 = Klavyenin üstündeki sayısal rakamlardır. Keypad 0 1 2 = ise Klavyenin sağ taraftaki bulunan sayısal rakamlardır.
        {
            Silahdegistir(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Silahdegistir(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Silahdegistir(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Silahdegistir(3);
        }


        if (Input.GetKeyDown((KeyCode.Q)))
        {
            QtusuVersiyonuSilahdegistir();

        }
    }

    public void DarbeAl(float darbegucu) // Düşmanlarında Darbe güçleri farklı olabilir dimi ?
    {
        health -= darbegucu;
        HealthBar.fillAmount = health/100;

        if (health<=0)
        {
            GameOver();
        }

    }

    void GameOver()
    {
        // Burada Canvas Çıkacak.
        Debug.Log("ÖLDÜN ! ");
    }


    IEnumerator DusmanCıkar()
    {
        while (true)
        {
            yield return new WaitForSeconds(DusmancikmaSuresi);

            int  dusman = Random.Range(0, 5);
            int cikisnoktasi = Random.Range(0, 2);
            int hedefnoktasi = Random.Range(0, 2);

           GameObject Obje= Instantiate(dusmanlar[dusman], cikisNoktalari[cikisnoktasi].transform.position, Quaternion.identity);
           Obje.GetComponent<Dusman_>().HedefBelirle(hedefNoktalar[hedefnoktasi]);

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


}
