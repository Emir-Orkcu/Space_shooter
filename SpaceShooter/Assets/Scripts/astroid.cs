using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroid : MonoBehaviour
{
    public GameObject patlama;
    public GameObject playerpatlama;
    GameObject Kontrol;
    Kontrol oyunkontrol;
    void Start()
    {
        Kontrol = GameObject.FindGameObjectWithTag("oyunkontrol");
        oyunkontrol = Kontrol.GetComponent<Kontrol>();
        
    }

   void OnTriggerEnter(Collider col)
    {
        if (col.tag != "sınır")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Instantiate(patlama, transform.position, transform.rotation);
            
            oyunkontrol.ScoreArttir(10); 
        }
        if (col.tag == "player")
        {
            Instantiate(playerpatlama, col.transform.position, col.transform.rotation);
            oyunkontrol.Gameover();
        }
       
    }
}
