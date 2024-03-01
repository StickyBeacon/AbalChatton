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
        }
    }
}
