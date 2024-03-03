using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogelScript : MonoBehaviour
{
    int abStrength;
    GameObject currentAb;
    string abName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Kogel")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/ballTouch" + Random.Range(1,5).ToString()));
        }
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/enemyHit" + Random.Range(1, 5).ToString()));
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

    public SpriteRenderer getAbSpriteRenderer()
    {
        return transform.Find("Ab").GetComponent<SpriteRenderer>();
    }

    public void changeAberration(GameObject aberattion)
    {
        if (currentAb != null) {
            if (currentAb.name == aberattion.name)
            {
                abStrength++;
                if (abStrength > 5)
                {
                    abStrength = 5;
                }
                SpriteRenderer abColor = transform.Find("Ab").GetComponent<SpriteRenderer>();
                switch (abStrength)
                {
                    case 2:
                        abColor.color = Color.gray;
                        break;
                    case 3:
                        abColor.color = Color.white;
                        break;
                    case 4:
                        abColor.color = Color.blue;
                        break;
                    case 5:
                        abColor.color = Color.yellow;
                        break;
                }
                
            }
            else
            {
                abStrength = 1;
                currentAb = aberattion;
                abName = aberattion.name;
                transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Kogel" + aberattion.name);
            }
        }
        else
        {
            abStrength = 1;
            currentAb = aberattion;
            abName = aberattion.name;
            transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Kogel" + aberattion.name);
        }

        Debug.Log(abName);
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
            switch (abName) {
                case "BHole":
                    boy.GetComponent<BHoleScript>().Initalise(abStrength);
                    break;
                case "Laser":
                    boy.GetComponent<LaserScript>().Initialise(abStrength);
                    break;
                case "Freeze":
                    boy.GetComponent<FreezeScript>().Initalise(abStrength);
                    break;
                case "Explosion":
                    boy.GetComponent<ExplosionScript>().Initalise(abStrength);
                    break;
                case "Electric":
                    boy.GetComponent<ElectricScript>().Initialise(abStrength, other);
                    break;
                

            }
        }
    }
}
