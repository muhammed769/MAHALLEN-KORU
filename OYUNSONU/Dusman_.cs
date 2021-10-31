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

    GameObject anaKonrolcum;
    Animator Animatorum;
   


    void Start()
    {
        Animatorum = GetComponent<Animator>();
        ajan = gameObject.GetComponent<NavMeshAgent>();
        anaKonrolcum = GameObject.FindWithTag("AnaKontrolcum");
       
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
            gameObject.tag = "Untagged"; // Düþmanýn 1 kere ölüp 1 kere öldü gösterilmesi sorunu çözüldü.
        }
    }

  public void oldun()
    {
        anaKonrolcum.GetComponent<GameKontrolcu>().Dusman_sayisi_guncelle();
        Animatorum.SetTrigger("Olme");
        Destroy(gameObject,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Korumam_gerekli"))
        {
            anaKonrolcum.GetComponent<GameKontrolcu>().DarbeAl(DusmanDarbeGucu);
            oldun();
        }
    }
}
