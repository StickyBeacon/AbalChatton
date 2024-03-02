using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyScript : MonoBehaviour
{
    int strength;
    void Initialise(int strenght, int mass, Color cloor)
    {
        strength = strenght;
        gameObject.GetComponent<Rigidbody2D>().mass = mass;
        gameObject.GetComponent<SpriteRenderer>().color = cloor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().addDamage(1);
            strength -= 1;
            if (strength <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
