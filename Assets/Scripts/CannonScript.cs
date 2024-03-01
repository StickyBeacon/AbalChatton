using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject kogelPrefab;

    GameObject Cannon;

     //Verander dit met het inhouden van je mouse button
    float minForce = 6f;
    float maxForce = 40f;
    float kogelForce = 6f;

    private void Awake()
    {
        Cannon = GameObject.Find("CannonBody");
        kogelForce = 6f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (kogelForce <= maxForce)
            {
                kogelForce += Time.deltaTime*(maxForce-minForce);
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }



    void Shoot()
    {
        GameObject kogel = Instantiate(kogelPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = kogel.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * kogelForce, ForceMode2D.Impulse);
        kogelForce = minForce;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // some code from https://www.youtube.com/watch?v=LNLVOjbrQj4

    private void OnTriggerStay2D(Collider2D collision) //oppakken van kogels
    {
        if(collision.gameObject.tag == "Kogel")
        {
            Vector2 attracionDir = ((Vector2)Cannon.transform.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized;
            Vector2 otherDir = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            if (Vector2.Dot(attracionDir,otherDir) > 0)
            {
                if(collision != null)
                {
                    Cannon.GetComponent<GravityScript>().removeAttractedObject(collision);
                    //Dit moet verandert worden als we de dingen gaan opslaan
                }
                
            }
        }
        
    }
}
