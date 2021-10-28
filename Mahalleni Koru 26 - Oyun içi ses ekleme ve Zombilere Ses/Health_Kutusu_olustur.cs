using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Kutusu_olustur : MonoBehaviour
{
    public List<GameObject> HealthKutusuPoint = new List<GameObject>();
    public GameObject Health_Kutusunun_kendisi;

    public static bool Health_kutusu_varmi;
    public float Kutu_cikma_suresi;


    int randomsayim;
    void Start()
    {
        Health_kutusu_varmi = false;
        StartCoroutine(Health_Kutusu_yap());

    }


    IEnumerator Health_Kutusu_yap()
    {
        while (true)
        {
            yield return new WaitForSeconds(Kutu_cikma_suresi);

            if (!Health_kutusu_varmi)
            {
                randomsayim = Random.Range(0, 6);

                Instantiate(Health_Kutusunun_kendisi, HealthKutusuPoint[randomsayim].transform.position, HealthKutusuPoint[randomsayim].transform.rotation);
                Health_kutusu_varmi = true;
            }

        }
    }

}
