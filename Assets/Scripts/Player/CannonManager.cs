using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public GameObject Ear1;
    public GameObject Ear2;
    int HP = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            CannonHit();
            Destroy(collision.gameObject);
        }
    }
    public void Regenerate()
    {
        HP = 3;
        IEnumerator popRegen()
        {
            yield return new WaitForSeconds(0.75f);
            for (int i = 0; i < 2; i++)
            {
                if (!Ear1.activeSelf && !Ear2.activeSelf)
                {
                    gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/pop"));
                    int rand = Random.Range(0, 2);
                    if (rand == 1)
                    {
                        Ear1.SetActive(true);
                        Instantiate((GameObject)Resources.Load("Particles/Shoot"), Ear1.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Ear2.SetActive(true);
                        Instantiate((GameObject)Resources.Load("Particles/Shoot"), Ear2.transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (!Ear1.activeSelf)
                    {
                        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/pop"));
                        Ear1.SetActive(true);
                        Instantiate((GameObject)Resources.Load("Particles/Shoot"), Ear1.transform.position, Quaternion.identity);
                    }
                    else if(!Ear2.activeSelf)
                    {
                        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/pop"));
                        Ear2.SetActive(true);
                        Instantiate((GameObject)Resources.Load("Particles/Shoot"), Ear2.transform.position, Quaternion.identity);
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
        StartCoroutine(popRegen());
    }
    public void CannonHit()
    {
        GameObject.Find("ScoreManager").GetComponent<ScoreScript>().AddScore(-10);
        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/hurt"));
        Instantiate((GameObject)Resources.Load("Particles/EnemySpawn"), transform.position, Quaternion.identity);
        HP--;
        if (HP <= 0)
        {
            GameObject.Find("ActualManager").GetComponent<ActualManagerScript>().EndGame();
        }
        else
        {
            print("ow!!");
            if (Ear1.activeSelf && Ear2.activeSelf)
            {
                int rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    Ear1.SetActive(false);
                }
                else
                {
                    Ear2.SetActive(false);
                }
            }
            else
            {
                if (Ear1.activeSelf)
                {
                    Ear1.SetActive(false);
                }
                else
                {
                    Ear2.SetActive(false);
                }
            }
        }
        
    }
}
