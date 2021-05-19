using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroid_rotasyon : MonoBehaviour
{
    public float hız;
    Rigidbody fizik;
    void Start()
    {
        fizik = GetComponent<Rigidbody>();
        fizik.angularVelocity = Random.insideUnitSphere*hız;
    }

}
