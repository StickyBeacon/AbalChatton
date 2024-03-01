using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    bool active = false;
    float timer = 0f;
    public GameObject enemyFab;
    float SpawnRange = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            active = !active;
            Debug.Log("active is " + active);
        }
    }
    void FixedUpdate() //Spawnt enemies!!!!
    {
        if (active)
        {
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                float randDeg = Random.Range(0, 360f);
                Vector3 pos = new Vector3(SpawnRange * Mathf.Cos(randDeg), SpawnRange * Mathf.Sin(randDeg), 10);
                GameObject enemy = Instantiate(enemyFab, pos, Quaternion.identity);
                enemy.GetComponent<EnemyScript>().Initialise();
                timer = Random.Range(1f, 3f);
            }
        }
    }
}
