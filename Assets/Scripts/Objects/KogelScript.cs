using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogelScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().addDamage(1);
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CannonManager>().CannonHit();
        }
    }

    public void Initialize()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1);
    }
}
