using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kontrol : MonoBehaviour
{
    public GameObject Asteroid;
    public Vector3 randompos;
    public int score;
    public Text Text;
    bool oyunbitti = false;
    bool yenidenbaşla = false;
    public Text oyunbittitext;
    public Text sonscore;

    void update()
    {
        if (Input.GetKeyDown(KeyCode.R) && yenidenbaşla)
        {
            SceneManager.LoadScene("level1");
            
        }
    }



    void Start()
    {
        score = 0;
        Text.text = "Score:" + score;
        

        StartCoroutine(olustur());
    }
    IEnumerator olustur()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 vec = new Vector3(Random.Range(-randompos.x, randompos.x), 0, randompos.z);
                Instantiate(Asteroid, vec, Quaternion.identity);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(3);
            if (oyunbitti)
            {
                yenidenbaşla = true;
                break;
            }
        }

    }
    public void ScoreArttir(int gelenscore)
    {
        score += gelenscore;
        
            Text.text = "Score:" + score;
        
    }

    public void Gameover()
    {
        oyunbittitext.text = "OyunBitti ";
        sonscore.text = "Score:" + score;
        oyunbitti = true;
       
    }


   
}
