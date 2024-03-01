using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;

    private void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        AttractObjects();
    }

    public void removeAttractedObject(Collider2D gm) {
        AttractedObjects.RemoveAll((e) => e.gameObject.GetInstanceID() == gm.gameObject.GetInstanceID());
    }

    public void addAttractedObject(Collider2D gm)
    {
        AttractedObjects.Add(gm);
    }

    void AttractObjects()
    {
        foreach(Collider2D col in AttractedObjects)
        {
            col.GetComponent<AttractedScript>().Attract(this); //Jetst habben sie die sheise
        }
    }

    // some code from https://www.youtube.com/watch?v=e4DxQhTKJ7Y
}
