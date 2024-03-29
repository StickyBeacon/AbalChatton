using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckPortalScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kogel" && collision != null)
        {
            GameObject.Find("CannonShooter").GetComponent<CannonScript>().AddBall(collision.gameObject);
            GameObject.Find("CannonBody").GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/vwop"));
            Instantiate((GameObject)Resources.Load("Particles/Shoot"), collision.transform.position, Quaternion.identity);
        }
    }
}
