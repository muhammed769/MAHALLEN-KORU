using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MermiKutusu : MonoBehaviour
{

    #region Açýklama

    /* Gerekli Olanlar :
       
       1 ) Silah Türü
       2 ) Mermi Sayýsý     
    */

    #endregion

    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    string[] silahlar = // Dizi oluþturmamýzýn sebebi MERMÝ KUTUSU SAHNEDE OLUÞTUÐUNDA RANDOM BÝR SÝLAH TÜRÜNÜN MERMÝ KUTUSU ÞEKLÝNDE ÇIKACAK.Örneðin Mermi kutusu oyunda  Magnum içinde çýkabilir Pompali içinde ÇIKABÝLÝR.
    {
            "Magnum",
            "Pompali",
            "Sniper",
            "Taramali"
    };

    int[] mermiSayisi = // Mermi Sayýsýnýda RANDOM ATACAK..
    {
            10,
            20,
            5,
            30
    };

    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public string Olusan_Silahin_Turu;
    public int Olusan_Mermi_Sayisi;

    // ÖNEMLÝ AÇIKLAMA = Bu 2 deðiþken Dizilerden gelen RANDOM DEÐERLERÝ bu 2 deðiþkene tanýmlýcaz.


    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public List<Sprite> Silah_resimleri = new List<Sprite>();
    public Image Silahin_resmi; // Hierarchy => MermiKutusu => Canvas => Image 'i ( YENÝ ÝSMÝ Silahýn Resmi objesini) BURAYA SÜRÜKLEMEYÝ UNUTMA !!!

    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public int Noktasi; // ilgili noktanýn hangi noktada oluþacaðýný tutan deðiþkendir.

    void Start()
    {
        int gelenAnahtar = Random.Range(0, silahlar.Length );

        Olusan_Silahin_Turu = silahlar[gelenAnahtar];
        Olusan_Mermi_Sayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length )];

        Silahin_resmi.sprite = Silah_resimleri[gelenAnahtar];

         //Olusan_Silahin_Turu = silahlar[Random.Range(0, silahlar.Length - 1)]; // 0'ýncý deðerden baþlayýp silahlar dizinin -1 'inci elemaný al.-1 deðerini koymamýzýn NEDENÝ  ÞU : ÝNDÝS NUMARALARI 0 DAN BAÞLAR HATIRLA !!!!!!!
         //Olusan_Mermi_Sayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length - 1)];

        // Olusan_Silahin_Turu = "Taramali";
        // Olusan_Mermi_Sayisi = 30;
    }

   
    
}
