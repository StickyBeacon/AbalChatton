using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Vector3 focus;
    int distract = 0;
    float freezetime =-1f;
    float speed = 0.005f;
    float baseSpeed;
    public void Initialise()
    {
        Instantiate((GameObject)Resources.Load("Particles/EnemySpawn"), transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/enemySpawn" + Random.Range(1,5)));
        focus = new Vector3(0, 0, 0);
        float randSize = Random.Range(0.5f, 1.2f * Mathf.Pow(1.1f, (GameObject.Find("GameManager").GetComponent<GameScript>().getWaves()+1)));
        transform.localScale = new Vector3(randSize, randSize, randSize);
        //int ghost = Random.Range(0, 3);
        if(GameObject.Find("GameManager").GetComponent<GameScript>().getWaves() + 1 > 7)
        {
            float speedMax = 0.05f * Mathf.Pow(1.1f, GameObject.Find("GameManager").GetComponent<GameScript>().getWaves() + 1);
            speed = Random.Range(0.01f, speedMax);
        }
        else
        {
            speed = Random.Range(0.01f, 0.05f);
        }
        
        baseSpeed = speed;
        /*if(ghost == 0)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,0.5f,0.5f,0.5f);
        }*/
    }
    public void addDamage(int amount)
    {
        gameObject.transform.localScale = gameObject.transform.localScale / (amount * 2 * Mathf.Pow(0.95f, (GameObject.Find("GameManager").GetComponent<GameScript>().getWaves() + 1)));
        if (gameObject.transform.localScale.magnitude <= 0.87f)
        {
            GameObject.Find("CannonBody").GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/enemyDeath" + Random.Range(1, 4)));
            Instantiate((GameObject)Resources.Load("Particles/EnemyDeath"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (freezetime<0)
        {
            transform.position = Vector3.MoveTowards(transform.position, focus, speed);
        }
        else
        {
            freezetime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kogel")
        {
            addDamage(1);
            collision.gameObject.GetComponent<KogelScript>().rolForAb(gameObject);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/enemyHit" + Random.Range(1, 5).ToString()));
        }
    }

    public void Freeze(float time)
    {
        freezetime = time;
    }
    
    public void AlterFocus(Vector3 focus)
    {
        if(focus != Vector3.zero)
        {
            distract += 1;
            this.focus = focus;
            speed = speed * 2;
        }
        else
        {
            distract -= 1;
            if(distract == 0)
            {
                this.focus = focus;
                speed = baseSpeed;
            }
        }
        
    }
}
