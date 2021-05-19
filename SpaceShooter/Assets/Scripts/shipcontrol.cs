using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipcontrol : MonoBehaviour
{
    Rigidbody fizik;
    float horizontal = 0;
    float vertical = 0;
    Vector3 vec;
    public float hız;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float egim;
    float ateszamanı=0;
    public float atesgecensure;
    public GameObject kursun;
    public Transform cıkmayerı;
    void Start()
    {
        fizik = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetButton("Fire1")&&Time.time>ateszamanı)
        {
            ateszamanı = Time.time + atesgecensure;
            Instantiate(kursun,cıkmayerı.position,Quaternion.identity);

        }



    }
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("vertical");
        vec = new Vector3(horizontal, 0, vertical);
        fizik.velocity = vec*hız;

        fizik.position = new Vector3(Mathf.Clamp(fizik.position.x, minX, maxX), 0, Mathf.Clamp(fizik.position.z, minZ, maxZ));


        fizik.rotation = Quaternion.Euler(180,0,fizik.velocity.x*egim);







    }
   



}
