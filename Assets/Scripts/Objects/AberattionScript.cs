using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberattionScript : MonoBehaviour
{
    GameObject aberrationType;
    public void Initialise()
    {
        int rand = Random.Range(0, 5);
        switch (rand)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                aberrationType = (GameObject)Resources.Load("Prefabs/BHole");
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                aberrationType = (GameObject)Resources.Load("Prefabs/Electric");
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                aberrationType = (GameObject)Resources.Load("Prefabs/Explosion");
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                aberrationType = (GameObject)Resources.Load("Prefabs/Freeze");
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                aberrationType = (GameObject)Resources.Load("Prefabs/Laser");
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                aberrationType = (GameObject)Resources.Load("Prefabs/BabyBall");
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Kogel")
        {
            collision.gameObject.GetComponent<KogelScript>().changeAberration(aberrationType);
            Destroy(gameObject);
        }
        
    }
}
