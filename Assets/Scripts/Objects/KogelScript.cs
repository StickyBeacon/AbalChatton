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
}
