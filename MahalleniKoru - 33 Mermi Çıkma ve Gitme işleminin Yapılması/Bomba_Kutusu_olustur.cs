using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Kutusu_olustur : MonoBehaviour
{
    public List<GameObject> BombaKutusuPoint = new List<GameObject>();
    public GameObject Bomba_Kutusunun_kendisi;

    public static bool Bomba_kutusu_varmi;
    public float Kutu_cikma_suresi;


    int randomsayim;
    void Start()
    {
        Bomba_kutusu_varmi = false;
        StartCoroutine(Bomba_Kutusu_yap());

    }


    IEnumerator Bomba_Kutusu_yap()
    {
        while (true)
        {
            yield return new WaitForSeconds(Kutu_cikma_suresi);

            if (!Bomba_kutusu_varmi)
            {
                randomsayim = Random.Range(0, 6);

                Instantiate(Bomba_Kutusunun_kendisi, BombaKutusuPoint[randomsayim].transform.position, BombaKutusuPoint[randomsayim].transform.rotation);
                Bomba_kutusu_varmi = true;
            }

        }
    }

}
