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

        while (!operation.isDone) // Y�kleme B�TMED�YSE SEN While d�ng�s� i�erisine yazd���m kodlar� S�REKL� TEKRAR ET ! ! !
        {
            float ilerleme = Mathf.Clamp01(operation.progress / 0.9f); // 0.9f yazmam�z�n sebebi : Slider '�n 0.9 Value de�erine kadar while d�ng�s�ne girmesini sa�l�yoruz. Yani Y�KLEME A�AMASINI 0.9 VALUE DE�ER�NE KADAR G�STER�R�Z.
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
        Debug.Log("��kt�.");
        Application.Quit();
    }


    public void Hay�r()
    {
        CikisPanel.SetActive(false);

    }*/

    public void Tercih(string butonadi)
    {
        if (butonadi == "Evet")
        {
            Debug.Log("��kt�.");
            Application.Quit();
        }

        else if (butonadi == "Hayir")
        {
            CikisPanel.SetActive(false);
        }

    }

}
