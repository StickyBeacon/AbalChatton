using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    int strength;
    // Start is called before the first frame update

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void Initialise(int strength)
    {
        this.strength = strength * 5;
        transform.localScale = new Vector3(0.5f*strength, this.strength, 1);
        float ranDeg = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, 0, ranDeg);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(selfDestruct());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().addDamage(1);
        }
    }
}
