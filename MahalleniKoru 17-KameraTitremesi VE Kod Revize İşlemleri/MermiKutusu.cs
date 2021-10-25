using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MermiKutusu : MonoBehaviour
{
    string[] silahlar =
        {
            "Magnum",
            "Pompali",
            "Sniper",
            "Taramali"
        };

    int[] mermiSayisi =
    {
            10,
            20,
            5,
            30

    };

    public List<Sprite> Silah_resimleri = new List<Sprite>();
    public Image Silahin_resimi;


    public string Olusan_Silahin_Turu;
    public int Olusan_Mermi_sayisi;
    public int Noktasi;

    void Start()
    {

         int gelenanahtar = Random.Range(0, silahlar.Length);

         Olusan_Silahin_Turu = silahlar[gelenanahtar];
         Olusan_Mermi_sayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length)];

        Silahin_resimi.sprite = Silah_resimleri[gelenanahtar];
        
      /*  Olusan_Silahin_Turu = "Taramali";
        Olusan_Mermi_sayisi = 30;*/


    }



}
