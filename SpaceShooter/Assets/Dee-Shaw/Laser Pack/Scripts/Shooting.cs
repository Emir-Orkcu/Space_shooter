using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Lazer;
    public GameObject Lazer1;
    public GameObject Lazer2;
    public GameObject Lazer3;
    public GameObject Lazer4;
    

    public GameObject ShootPoint;

    private AudioSource Beam;

    private GameObject SpawnedLazer;

    private GameObject SpawnedLazer1;

    private GameObject SpawnedLazer2;

    private GameObject SpawnedLazer3;

    private GameObject SpawnedLazer4;

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnedLazer = Instantiate(Lazer, ShootPoint.transform) as GameObject;
        DisableLazer();

        SpawnedLazer1 = Instantiate(Lazer1, ShootPoint.transform) as GameObject;
        DisableLazer1();

        SpawnedLazer2 = Instantiate(Lazer2, ShootPoint.transform) as GameObject;
        Disablelazer2();

        SpawnedLazer3 = Instantiate(Lazer3, ShootPoint.transform) as GameObject;
        Disablelazer3();

        SpawnedLazer4 = Instantiate(Lazer4, ShootPoint.transform) as GameObject;
        Disablelazer4();

       



        Beam = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnableLazer1();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UpdateLazer1();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            DisableLazer1();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Beam.Play();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Beam.Stop();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            EnableLazer2();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UpdateLazer2();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            Disablelazer2();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Beam.Play();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Beam.Stop();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EnableLazer3();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateLazer3();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            Disablelazer3();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Beam.Play();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Beam.Stop();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            EnableLazer4();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateLazer4();
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            Disablelazer4();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Beam.Play();
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            Beam.Stop();
        }


       

        if (Input.GetMouseButtonDown(0))
        {
            EnableLazer();
        }
        if (Input.GetMouseButton(0))
        {
            UpdateLazer();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DisableLazer();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Beam.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Beam.Stop();
        }
    }
    void EnableLazer()
    {
        SpawnedLazer.SetActive (true);
    }
    void EnableLazer1()
    {
        SpawnedLazer1.SetActive (true);
    }
    void EnableLazer2()
    {
        SpawnedLazer2.SetActive (true);
    }
    void EnableLazer3()
    {
        SpawnedLazer3.SetActive(true);
    }
    void EnableLazer4()
    {
        SpawnedLazer4.SetActive(true);
    }


    void UpdateLazer()
    {
        if (ShootPoint != null)
        {
            SpawnedLazer.transform.position = ShootPoint.transform.position;
        }
    }
    void UpdateLazer2()
    {
        if (ShootPoint != null)
        {
            SpawnedLazer2.transform.position = ShootPoint.transform.position;
        }
    }
    void UpdateLazer1()
    {
        if (ShootPoint != null)
        {
            SpawnedLazer1.transform.position = ShootPoint.transform.position;
        }
    }
    void UpdateLazer3()
    {
        if (ShootPoint != null)
        {
            SpawnedLazer3.transform.position = ShootPoint.transform.position;
        }
    }
    void UpdateLazer4()
    {
        if (ShootPoint != null)
        {
            SpawnedLazer4.transform.position = ShootPoint.transform.position;
        }
    }

  


    void DisableLazer()
    {


        {
            SpawnedLazer.SetActive(false);
        }
    }
    void DisableLazer1()
    {
        {
            SpawnedLazer1.SetActive(false);
        }
    }
    void Disablelazer2()
    {
        {
            SpawnedLazer2.SetActive(false);
        }
    }
    void Disablelazer3()
    {
        {
            SpawnedLazer3.SetActive(false);
        }
    }
    void Disablelazer4()
    {
        {
            SpawnedLazer4.SetActive(false);
        }
    }

    
}
 



    
