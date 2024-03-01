using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity = -1f;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;

    private void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAttractedObjects();
    }

    private void FixedUpdate()
    {
        AttractObjects();
    }

    void SetAttractedObjects()
    {
        GameObject[] kogels = GameObject.FindGameObjectsWithTag("Kogel");
        foreach(GameObject gm in kogels)
        {
            AttractedObjects.Add(gm.GetComponent<Collider2D>());
        }
    }

    public void removeAttractedObject(Collider2D gm) {
        AttractedObjects.RemoveAll((e) => e.gameObject.GetInstanceID() == gm.gameObject.GetInstanceID());
        Destroy(gm.gameObject);
    }

    void AttractObjects()
    {
        for(int i = 0; i<AttractedObjects.Count; i++)
        {
            AttractedObjects[i].GetComponent<AttractedScript>().Attract(this); //Jetst habben sie die sheise
        }
    }

    // some code from https://www.youtube.com/watch?v=e4DxQhTKJ7Y
}
