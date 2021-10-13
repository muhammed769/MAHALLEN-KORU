using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi_Kutusu_Olustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuPoint = new List<GameObject>();
    public GameObject Mermi_Kutusunun_Kendisi;

  public static bool Mermi_Kutusu_Varmi; // public  static : Herhangi bir ÖRNEKLEME YAPMADAN baþka sýnýflarda bu deðiþkeni kullanabilirsin.

    public float  Kutu_Cikma_suresi;
    void Start()
    {
        Mermi_Kutusu_Varmi = false;
        StartCoroutine(Mermi_Kutusu_Yap());
    }


    IEnumerator Mermi_Kutusu_Yap()
    {
        while (true)
        {
            yield return null; // Her saniyeyi kontrol et.

            if (!Mermi_Kutusu_Varmi) // MermiKutusuYOKSA :
            {
                yield return new WaitForSeconds(Kutu_Cikma_suresi);

                    int randomSayim = Random.Range(0, 4);
                    Instantiate(Mermi_Kutusunun_Kendisi, MermiKutusuPoint[randomSayim].transform.position, MermiKutusuPoint[randomSayim].transform.rotation);
                    Mermi_Kutusu_Varmi = true;              
            }
        }

        // EÐER Mermi_Kutusu VARSA  keles SCRÝPTÝNE GÝDÝP ÝLGÝLÝ KODLARI YAZDIKTAN SONRA   en önemli kodlarý MermiAl() fonksiyonuna ve OnTriggerEnter kod bloguna  yazdýk.

        /*   while (true)
           {
               yield return new WaitForSeconds(5f);
               int randomSayim = Random.Range(0, 4);
               Instantiate(Mermi_Kutusunun_Kendisi, MermiKutusuPoint[randomSayim].transform.position, MermiKutusuPoint[randomSayim].transform.rotation);

               // ÖNEMLÝ = Random bir þekilde  mermilerinin çýkýþ noktalarýný belirledðimiz için örneðin MermiKutusu 1'inci noktadan çýktý 5 saniye bekledi TEKRARDAN 1'ÝNCÝ NOKTADAN ÇIKMA ÝHTÝMALÝ VAR ÇÜNKÜ RANDOM BÝR ÞEKÝLDE YAPTIK.
               // ÝÞTE BU HATAYI DÜZELTMEK ÝÇÝN :

               Mermi_Kutusu_Varmi = true;

           }*/

    }


}
