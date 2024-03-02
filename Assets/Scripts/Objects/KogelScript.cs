using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogelScript : MonoBehaviour
{
    int abStrength;
    GameObject currentAb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Kogel")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/ballTouch" + Random.Range(1,5).ToString()));
        }
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().addDamage(1);
            rolForAb(collision.gameObject);
        }
    }

    public void Initialize()
    {
        gameObject.transform.Find("Color").GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1);
    }

    public Color getColor()
    {
        return gameObject.transform.Find("Color").GetComponent<SpriteRenderer>().color;
    }

    public void changeAberration(GameObject aberattion)
    {
        abStrength = 1;
        currentAb = aberattion;
        print(aberattion);
        //Verander type sprite
        //Particle effects?
    }

    public void rolForAb(GameObject other)
    {
        if (currentAb != null)
        {
            Vector3 pos = transform.position;
            Vector3 offset = new Vector3(0, 0, 10);
            GameObject boy = Instantiate(currentAb, pos + offset, Quaternion.identity);
            if (boy.GetComponent<BHoleScript>() != null) boy.GetComponent<BHoleScript>().Initalise(abStrength);
            else if (boy.GetComponent<BabyScript>() != null)
            {
                print("Ik doe het morgen wel!!");
            }
            else if (boy.GetComponent<LaserScript>() != null) boy.GetComponent<LaserScript>().Initialise(abStrength);
            else if (boy.GetComponent<ExplosionScript>() != null) boy.GetComponent<ExplosionScript>().Initalise(abStrength);
            else if (boy.GetComponent<ElectricScript>() != null) boy.GetComponent<ElectricScript>().Initialise(abStrength, other);
            else if (boy.GetComponent<FreezeScript>() != null) boy.GetComponent<FreezeScript>().Initalise(abStrength);
        }
    }
}
