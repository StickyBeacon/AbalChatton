using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricScript : MonoBehaviour
{
    int strength = 1;
    GameObject current;
    CircleCollider2D coll = null;
    List<GameObject> ignoreList = new List<GameObject>();
    Material Linemat;
    public void Initialise(int strength, GameObject ignore)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/electric" + Random.Range(1, 4)));
        Linemat = new Material(Shader.Find("Sprites/Default"));
        Linemat.color = new Color(221f/255f, 202f/255f, 125f/255f, 1);
        this.strength = strength;
        current = ignore;
        coll = gameObject.GetComponent<CircleCollider2D>();
        coll.radius = 4 + strength;
        if (ignore != null)
        {
            ignoreList.Add(ignore);
        }
        Activate();
    }

    public void Activate()
    {
        
        IEnumerator radiusRemove()
        {
            float i = 0f;
            while (i< 4 + strength*2)
            {
                yield return new WaitForSeconds(0.05f);
                coll.enabled = true;
                yield return new WaitForSeconds(0.05f);
                if (coll.enabled) coll.enabled = false;
                i += 1;
            }
            Destroy(gameObject);
            
        }
        StartCoroutine(radiusRemove());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject != current && !ignoreList.Contains(collision.gameObject) && ignoreList.Count<3+strength*2)
        {
            StartCoroutine(drawLine(current,collision.gameObject));
            coll.enabled = false;
            ignoreList.Add(collision.gameObject);
            current = collision.gameObject;
            gameObject.transform.position = current.transform.position;
            //VOEG HIER PARTICLES TOE N AL
            current.GetComponent<EnemyScript>().addDamage(1);
            gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/electric" + Random.Range(1, 4)));
        }
    }

    IEnumerator drawLine(GameObject g1,GameObject g2)
    {
        if(g1 == null)
        {
            g1 = gameObject;
        }
        LineRenderer lineRenderer;
        if (g1.GetComponent<LineRenderer>() ==null) g1.AddComponent<LineRenderer>();
        lineRenderer = g1.GetComponent<LineRenderer>();
        lineRenderer.material = Linemat;
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.SetPositions(new Vector3[] { g1.transform.position, g2.transform.position });
        yield return new WaitForSeconds(0.1f);
        if(g1 != null)
        {
            Destroy(lineRenderer);
        }
    }

}
