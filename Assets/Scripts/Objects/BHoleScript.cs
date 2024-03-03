using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHoleScript : MonoBehaviour
{
    int strength;
    List<GameObject> caught = new List<GameObject>();
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(0.5f + strength/2f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        foreach(GameObject gm in caught)
        {
            if(gm != null)
            {
                gm.GetComponent<EnemyScript>().AlterFocus(new Vector3(0, 0, 0));
            }
        }
        Destroy(gameObject);
    }
    public void Initalise(int strength)
    {
        this.strength = strength;
        gameObject.transform.localScale = new Vector3(3 + strength * 2, 3 + strength * 2, 3 + strength * 2);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(selfDestruct());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !caught.Contains(collision.gameObject)) 
        {
            caught.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyScript>().AlterFocus(gameObject.transform.position);
        }
    }
}
