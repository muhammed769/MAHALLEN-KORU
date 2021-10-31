using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenuKontrolum : MonoBehaviour
{

    public GameObject LoadingPanel;
    public GameObject CikisPanel;
    public Slider LoadingSlider;


    public void OyunaBasla()
    {
        StartCoroutine(sahneYuklemeLoading());
    }



    IEnumerator sahneYuklemeLoading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        LoadingPanel.SetActive(true);

        while (!operation.isDone) // Yükleme BÝTMEDÝYSE SEN While döngüsü içerisine yazdýðým kodlarý SÜREKLÝ TEKRAR ET ! ! !
        {
            float ilerleme = Mathf.Clamp01(operation.progress / 0.9f); // 0.9f yazmamýzýn sebebi : Slider 'ýn 0.9 Value deðerine kadar while döngüsüne girmesini saðlýyoruz. Yani YÜKLEME AÞAMASINI 0.9 VALUE DEÐERÝNE KADAR GÖSTERÝRÝZ.
            LoadingSlider.value = ilerleme;
            yield return null;
        }
    }


    public void OyundanCik()
    {
        CikisPanel.SetActive(true);
    }




    /*public void Evet()
    {
        Debug.Log("Çýktý.");
        Application.Quit();
    }


    public void Hayýr()
    {
        CikisPanel.SetActive(false);

    }*/

    public void Tercih(string butonadi)
    {
        if (butonadi == "Evet")
        {
            Debug.Log("Çýktý.");
            Application.Quit();
        }

        else if (butonadi == "Hayir")
        {
            CikisPanel.SetActive(false);
        }

    }

}
