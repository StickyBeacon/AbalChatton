using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractedScript : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    float maxForce = 80f;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Attract(GravityScript grav)
    {
        Vector2 attracionDir = (Vector2)grav.planetTransform.position - m_rigidbody.position;
        m_rigidbody.AddForce(attracionDir.normalized * -grav.gravity * Time.fixedDeltaTime);
        if (m_rigidbody.velocity.magnitude > maxForce)
        {
            Debug.Log("Boom!!!");
            GameObject.Find("CannonBody").GetComponent<GravityScript>().removeAttractedObject(gameObject.GetComponent<Collider2D>());
        }
    }

    // some code from https://www.youtube.com/watch?v=e4DxQhTKJ7Y

}