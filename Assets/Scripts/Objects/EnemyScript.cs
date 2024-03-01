using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    float speed = 0.005f;
    public void Initialise()
    {
        float randSize = Random.Range(0.5f, 2f);
        transform.localScale = new Vector3(randSize, randSize, randSize);
        int ghost = Random.Range(0, 3);
        speed = Random.Range(0.001f, 0.005f);
        if(ghost == 0)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,0.5f,0.5f,0.5f);
        }
    }
    public void addDamage(int amount)
    {
        gameObject.transform.localScale = gameObject.transform.localScale / (amount * 2);
        if (gameObject.transform.localScale.magnitude < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("You lose!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kogel")
        {
            addDamage(1);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You lose!!");
        }
    }
}
