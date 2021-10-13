using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKontrolcu : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("OyunBasladimi"))
        {
            PlayerPrefs.SetInt("Taramali_Mermi", 70);

            /*PlayerPrefs.SetInt("Pompali_Mermi", 50);
            PlayerPrefs.SetInt("Magnum_Mermi", 30);
            PlayerPrefs.SetInt("Sniper_Mermi", 20);*/

            PlayerPrefs.SetInt("OyunBasladimi", 1);
        }
    }


    private void Update()
    {
        
    }
}
