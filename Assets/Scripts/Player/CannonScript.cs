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
        GameObject kogel = Instantiate(kogelPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = kogel.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * kogelForce, ForceMode2D.Impulse);
        Cannon.GetComponent<GravityScript>().addAttractedObject(kogel.GetComponent<Collider2D>());
    }


    public void AddBall(GameObject caught)
    {
        kogelQueue.Enqueue(caught);
        Cannon.GetComponent<GravityScript>().removeAttractedObject(caught.GetComponent<Collider2D>());
        Destroy(caught);
    }

    // some code from https://www.youtube.com/watch?v=LNLVOjbrQj4
}
