using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi_Kutusu_Olustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuPoint = new List<GameObject>();
    public GameObject Mermi_Kutusunun_Kendisi;

  public static bool Mermi_Kutusu_Varmi; // public  static : Herhangi bir �RNEKLEME YAPMADAN ba�ka s�n�flarda bu de�i�keni kullanabilirsin.

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

        // E�ER Mermi_Kutusu VARSA  keles SCR�PT�NE G�D�P �LG�L� KODLARI YAZDIKTAN SONRA   en �nemli kodlar� MermiAl() fonksiyonuna ve OnTriggerEnter kod bloguna  yazd�k.

        /*   while (true)
           {
               yield return new WaitForSeconds(5f);
               int randomSayim = Random.Range(0, 4);
               Instantiate(Mermi_Kutusunun_Kendisi, MermiKutusuPoint[randomSayim].transform.position, MermiKutusuPoint[randomSayim].transform.rotation);

               // �NEML� = Random bir �ekilde  mermilerinin ��k�� noktalar�n� belirled�imiz i�in �rne�in MermiKutusu 1'inci noktadan ��kt� 5 saniye bekledi TEKRARDAN 1'�NC� NOKTADAN �IKMA �HT�MAL� VAR ��NK� RANDOM B�R �EK�LDE YAPTIK.
               // ��TE BU HATAYI D�ZELTMEK ���N :

               Mermi_Kutusu_Varmi = true;

           }*/

    }


}
