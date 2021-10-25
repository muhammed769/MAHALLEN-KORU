using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman_ : MonoBehaviour
{

    NavMeshAgent ajan;
    GameObject Hedef;
    public float health;

    public float  DusmanDarbeGucu;
  
  


    void Start()
    {

        ajan = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        ajan.SetDestination(Hedef.transform.position); // ajan Hedefe gitsin.
    }


    public void HedefBelirle(GameObject objem)
    {
        Hedef = objem;
    }

    public void DarbeAl(float darbegucu)
    {
        health -= darbegucu;
        if (health <= 0)
        {
            oldun();
        }
    }

    void oldun()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Korumam_gerekli"))
        {
            GameObject anaKonrolcum = GameObject.FindWithTag("AnaKontrolcum");
            anaKonrolcum.GetComponent<GameKontrolcu>().DarbeAl(DusmanDarbeGucu);
        }
    }





}
