using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject enemyFab;
    public GameObject electricFab;
    public GameObject freezeFab;
    public GameObject explodeFab;
    public GameObject bholeFab;
    public GameObject laserFab;
    public GameObject abFab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject newthing = BetterInst(prefab);
            newthing.GetComponent<KogelScript>().Initialize();
            GameObject.Find("CannonBody").GetComponent<GravityScript>().addAttractedObject(newthing.GetComponent<Collider2D>());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject enemy = BetterInst(enemyFab);
            enemy.GetComponent<EnemyScript>().Initialise();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject electric = BetterInst(electricFab);
            electric.GetComponent<ElectricScript>().Initialise(1,null);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject freeze = BetterInst(freezeFab);
            freeze.GetComponent<FreezeScript>().Initalise(1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameObject explosion = BetterInst(explodeFab);
            explosion.GetComponent<ExplosionScript>().Initalise(1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject Bhole = BetterInst(bholeFab);
            Bhole.GetComponent<BHoleScript>().Initalise(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject laser = BetterInst(laserFab);
            laser.GetComponent<LaserScript>().Initialise(1);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject aberattion = BetterInst(abFab);
            aberattion.GetComponent<AberattionScript>().Initialise();
        }
    }

    GameObject BetterInst(GameObject fab)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = new Vector3(0, 0, 10);
        return Instantiate(fab, pos + offset, Quaternion.identity);
    }
    // some code from https://www.youtube.com/shorts/x03mPWj3gFg
}
