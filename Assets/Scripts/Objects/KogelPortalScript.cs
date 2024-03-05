using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogelPortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(selfDestruct());
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject kogel = Instantiate((GameObject)Resources.Load("Prefabs/DebugObject"), transform.position + new Vector3(0, 0, 10), Quaternion.identity);
        kogel.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/pop"));
        Instantiate((GameObject)Resources.Load("Particles/Shoot"), transform.position, Quaternion.identity);
        kogel.GetComponent<KogelScript>().Initialize();
        GameObject.Find("CannonBody").GetComponent<GravityScript>().addAttractedObject(kogel.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
