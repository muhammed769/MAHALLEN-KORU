using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MermiKutusu : MonoBehaviour
{

    #region A��klama

    /* Gerekli Olanlar :
       
       1 ) Silah T�r�
       2 ) Mermi Say�s�     
    */

    #endregion

    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    string[] silahlar = // Dizi olu�turmam�z�n sebebi MERM� KUTUSU SAHNEDE OLU�TU�UNDA RANDOM B�R S�LAH T�R�N�N MERM� KUTUSU �EKL�NDE �IKACAK.�rne�in Mermi kutusu oyunda  Magnum i�inde ��kabilir Pompali i�inde �IKAB�L�R.
    {
            "Magnum",
            "Pompali",
            "Sniper",
            "Taramali"
    };

    int[] mermiSayisi = // Mermi Say�s�n�da RANDOM ATACAK..
    {
            10,
            20,
            5,
            30
    };

    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public string Olusan_Silahin_Turu;
    public int Olusan_Mermi_Sayisi;

    // �NEML� A�IKLAMA = Bu 2 de�i�ken Dizilerden gelen RANDOM DE�ERLER� bu 2 de�i�kene tan�ml�caz.


    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public List<Sprite> Silah_resimleri = new List<Sprite>();
    public Image Silahin_resmi; // Hierarchy => MermiKutusu => Canvas => Image 'i ( YEN� �SM� Silah�n Resmi objesini) BURAYA S�R�KLEMEY� UNUTMA !!!

    // -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public int Noktasi; // ilgili noktan�n hangi noktada olu�aca��n� tutan de�i�kendir.

    void Start()
    {
        int gelenAnahtar = Random.Range(0, silahlar.Length );

        Olusan_Silahin_Turu = silahlar[gelenAnahtar];
        Olusan_Mermi_Sayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length )];

        Silahin_resmi.sprite = Silah_resimleri[gelenAnahtar];

         //Olusan_Silahin_Turu = silahlar[Random.Range(0, silahlar.Length - 1)]; // 0'�nc� de�erden ba�lay�p silahlar dizinin -1 'inci eleman� al.-1 de�erini koymam�z�n NEDEN�  �U : �ND�S NUMARALARI 0 DAN BA�LAR HATIRLA !!!!!!!
         //Olusan_Mermi_Sayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length - 1)];

        // Olusan_Silahin_Turu = "Taramali";
        // Olusan_Mermi_Sayisi = 30;
    }

   
    
}
