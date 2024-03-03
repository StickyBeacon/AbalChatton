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

    bool allowShoot = true;

    private void Awake()
    {
        Cannon = GameObject.Find("CannonBody");
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
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
    }
    void Shoot()
    {
        if (kogelQueue.Count > 0 && allowShoot)
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

            ViewNextBall();

        }
        else
        {
            Cannon.GetComponent<AudioSource>().Play();
        }
    }

    // some code from https://www.youtube.com/watch?v=LNLVOjbrQj4

    void ViewNextBall()
    {
        Color kogelColor = new Color(0, 0, 0, 0);
        if (kogelQueue.Count > 0)
        {
            kogelColor = kogelQueue.Peek().GetComponent<KogelScript>().getColor();
            SpriteRenderer aberration = kogelQueue.Peek().GetComponent<KogelScript>().getAbSpriteRenderer();
            if (aberration.sprite != null)
            {
                Cannon.transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = aberration.sprite;
                Cannon.transform.Find("Ab").GetComponent<SpriteRenderer>().color = aberration.color;
            }
            else
            {
                Cannon.transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        else
        {
            Cannon.transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = null;
        }
        Cannon.transform.Find("CannonColor").GetComponent<SpriteRenderer>().color = kogelColor;
        Cannon.transform.Find("CannonShooterColor").GetComponent<SpriteRenderer>().color = kogelColor;

    }

    public void AddBall(GameObject caught)
    {
        kogelQueue.Enqueue(caught);
        if(kogelQueue.Count == 1)
        {
            ViewNextBall();
        }
        Cannon.GetComponent<GravityScript>().removeAttractedObject(caught.GetComponent<Collider2D>());
        caught.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        caught.SetActive(false);
    }

    public void setAllowShoot(bool value)
    {
        allowShoot = value;
    }

    public void clearQueue()
    {
        kogelQueue.Clear();
        ViewNextBall();
    }
}
