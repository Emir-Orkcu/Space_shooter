using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sınırlar : MonoBehaviour
{
    void OnTriggerExit(Collider col)
    {
        Destroy(col.gameObject);
      
    }

   

}