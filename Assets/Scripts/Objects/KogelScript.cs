using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogelScript : MonoBehaviour
{
    int abStrength;
    GameObject currentAb;
    string abName;
    int stage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Kogel")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/ballTouch" + Random.Range(1,5).ToString()));
            Instantiate((GameObject)Resources.Load("Particles/Clink"), collision.transform.position, Quaternion.identity);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreScript>().AddScore(stage);
            stageIncrease();
            gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/enemyHit" + Random.Range(1, 5).ToString()));
            collision.gameObject.GetComponent<EnemyScript>().addDamage(1);
            rolForAb(collision.gameObject);
            Instantiate((GameObject)Resources.Load("Particles/Clink"), collision.transform.position, Quaternion.identity);
        }
    }

    public void Initialize()
    {
        //it's nothing>>>
    }

    void stageIncrease()
    {
        stage += 1;
        switch (stage)
        {
            case 2:
                transform.Find("Stage").GetComponent<SpriteRenderer>().color = new Color(184f / 255f, 139f / 255f, 74f / 255f, 1);
                break;
            case 3:
                transform.Find("Stage").GetComponent<SpriteRenderer>().color = new Color(106f / 255f, 65f / 255f, 16f / 255f, 1);
                break;
            case 4:
                transform.Find("Stage").GetComponent<SpriteRenderer>().color = new Color(118f / 255f, 152f / 255f, 179f / 255f, 1);
                break;
            case 5:
                transform.Find("Stage").GetComponent<SpriteRenderer>().color = new Color(26f / 255f, 34f / 255f, 78f / 255f, 1);
                break;
        }
    }
    public void resetStage()
    {
        stage = 1;
        transform.Find("Stage").GetComponent<SpriteRenderer>().color = new Color(221f / 255f, 202f / 255f, 125f / 255f, 1);
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
                SpriteRenderer abColor = transform.Find("Color").GetComponent<SpriteRenderer>();
                switch (abStrength)
                {
                    case 2:
                        abColor.color = new Color(184f/255f,139f/255f,74f/255f,1);
                        break;
                    case 3:
                        abColor.color = new Color(106f / 255f, 65f / 255f, 16f / 255f, 1);
                        break;
                    case 4:
                        abColor.color = new Color(118f/255f,152f/255f,179f/255f,1);
                        break;
                    case 5:
                        abColor.color = new Color(26f/255f,34f/255f,78f/255f,1);
                        break;
                }
                
            }
            else
            {
                abStrength = 1;
                currentAb = aberattion;
                abName = aberattion.name;
                transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Kogel" + aberattion.name);
                transform.Find("Color").GetComponent<SpriteRenderer>().color = new Color(221f / 255f, 202f / 255f, 125f / 255f, 1);
            }
        }
        else
        {
            abStrength = 1;
            currentAb = aberattion;
            abName = aberattion.name;
            transform.Find("Ab").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Kogel" + aberattion.name);
            transform.Find("Color").GetComponent<SpriteRenderer>().color = new Color(221f / 255f, 202f / 255f, 125f / 255f, 1);
        }
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
