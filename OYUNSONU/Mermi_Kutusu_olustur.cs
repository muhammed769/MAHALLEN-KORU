using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi_Kutusu_olustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuPoint = new List<GameObject>();
    public GameObject Mermi_Kutusunun_kendisi;

    public static bool Mermi_kutusu_varmi;
    public float Kutu_cikma_suresi;

    List<int> noktalar = new List<int>();
    int randomsayim;
    void Start()
     {
        Mermi_kutusu_varmi = false;
        StartCoroutine(Mermi_Kutusu_yap());      
    }
    
    
    IEnumerator Mermi_Kutusu_yap()
    {
        while (true)
        {
            yield return new WaitForSeconds(Kutu_cikma_suresi);

            randomsayim = Random.Range(0, 5);

            if (!noktalar.Contains(randomsayim))
            {
                noktalar.Add(randomsayim);
            }
            else
            {
                randomsayim = Random.Range(0, 5);
                continue;
            }



            GameObject objem= Instantiate(Mermi_Kutusunun_kendisi, MermiKutusuPoint[randomsayim].transform.position, MermiKutusuPoint[randomsayim].transform.rotation);

            objem.transform.gameObject.GetComponentInChildren<MermiKutusu>().Noktasi= randomsayim;
                         


        }
        /*   while(true)
           {

               yield return new WaitForSeconds(5f);
               int randomsayim = Random.Range(0, 3);
               Instantiate(Mermi_Kutusunun_kendisi, MermiKutusuPoint[randomsayim].transform.position, MermiKutusuPoint[randomsayim].transform.rotation);
               Mermi_kutusu_varmi = true;
           }*/


    }
    

   
   public void NoktalarinKaldirma(int deger)
    {

        noktalar.Remove(deger);
    }
   
}
