using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject kogelPrefab;
    public GameObject suckPortal;
    Queue<GameObject> kogelQueue = new Queue<GameObject>();
    GameObject Cannon;
    float kogelForce = 40f;

    private void Awake()
    {
        Cannon = GameObject.Find("CannonBody");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonUp(1))
        {
            suckPortal.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            suckPortal.SetActive(true);
        }
    }
    void Shoot()
    {
        if (kogelQueue.Count > 0)
        {
            
            GameObject nextKogel = kogelQueue.Dequeue();
            if (Input.GetMouseButton(1))
            {
                kogelQueue.Enqueue(nextKogel);
                Cannon.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/switch"));
            }
            else
            {
                Cannon.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/cannonShot"));
                nextKogel.SetActive(true);
                nextKogel.transform.position = transform.position;
                //GameObject kogel = Instantiate(nextKogel, transform.position, transform.rotation);
                Rigidbody2D rb = nextKogel.GetComponent<Rigidbody2D>();
                rb.AddForce(transform.up * kogelForce, ForceMode2D.Impulse);
                Cannon.GetComponent<GravityScript>().addAttractedObject(nextKogel.GetComponent<Collider2D>());
            }
            Color kogelColor = new Color(0,0,0,0);
            if (kogelQueue.Count > 0)
            {
                kogelColor = kogelQueue.Peek().GetComponent<KogelScript>().getColor();
            }
            Cannon.transform.Find("CannonColor").GetComponent<SpriteRenderer>().color = kogelColor;
            Cannon.transform.Find("CannonShooterColor").GetComponent<SpriteRenderer>().color = kogelColor;
        }
        else
        {
            Cannon.GetComponent<AudioSource>().Play();
        }
    }

    // some code from https://www.youtube.com/watch?v=LNLVOjbrQj4

    public void AddBall(GameObject caught)
    {
        kogelQueue.Enqueue(caught);
        if(kogelQueue.Count == 1)
        {
            Color kogelColor = kogelQueue.Peek().GetComponent<KogelScript>().getColor();
            Cannon.transform.Find("CannonColor").GetComponent<SpriteRenderer>().color = kogelColor;
            Cannon.transform.Find("CannonShooterColor").GetComponent<SpriteRenderer>().color = kogelColor;
        }
        Cannon.GetComponent<GravityScript>().removeAttractedObject(caught.GetComponent<Collider2D>());
        caught.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        caught.SetActive(false);
    }

    
}
