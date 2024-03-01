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
            nextKogel.SetActive(true);
            nextKogel.transform.position = transform.position;
            //GameObject kogel = Instantiate(nextKogel, transform.position, transform.rotation);
            Rigidbody2D rb = nextKogel.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * kogelForce, ForceMode2D.Impulse);
            Cannon.GetComponent<GravityScript>().addAttractedObject(nextKogel.GetComponent<Collider2D>());
        }
        else
        {
            print("tick");
        }
    }

    // some code from https://www.youtube.com/watch?v=LNLVOjbrQj4

    public void AddBall(GameObject caught)
    {
        kogelQueue.Enqueue(caught);
        Cannon.GetComponent<GravityScript>().removeAttractedObject(caught.GetComponent<Collider2D>());
        caught.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        caught.SetActive(false);
    }

    
}
