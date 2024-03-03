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
        Ear2.SetActive(true);
        Ear1.SetActive(true);
    }
    public void CannonHit()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/hurt"));
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
