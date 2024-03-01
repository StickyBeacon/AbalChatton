using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject enemyFab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = new Vector3(0, 0, 10);
            GameObject newthing = Instantiate(prefab, pos+offset, Quaternion.identity);
            GameObject.Find("CannonBody").GetComponent<GravityScript>().addAttractedObject(newthing.GetComponent<Collider2D>());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = new Vector3(0, 0, 10);
            GameObject enemy = Instantiate(enemyFab, pos + offset, Quaternion.identity);
            enemy.GetComponent<EnemyScript>().Initialise();
        }
    }
    // some code from https://www.youtube.com/shorts/x03mPWj3gFg
}
