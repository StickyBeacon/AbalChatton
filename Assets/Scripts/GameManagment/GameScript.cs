using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject enemyFab;
    float SpawnRange = 10f;
    public GameObject miniPortal;


    bool active = false;
    float waveTimer = 123f; //best 60 seconden ofzo
    float maxWaveTimer = 30f;
    int amountwaves = 0;
    float timer = 0f;
    float startSpawnTime = 2f;
    
    public GameObject aberration;
    List<GameObject> optionList = new List<GameObject>();
    int allowTake = 1; //Maak Dit groter naarmate dat #waves doorgaat.
    int abAmount = 3;

    public GameObject startText;

    IEnumerator startGame()
    {
        startText.SetActive(false);
        amountwaves = 0;
        for(int i = 0; i< 2; i++)
        {
            spawnPortal();
            yield return new WaitForSeconds(1f);
        }
        active = true;
        waveTimer = maxWaveTimer;
    }

    void FixedUpdate() //Spawnt enemies!!!!
    {
        if (active)
        {
            waveTimer -= Time.fixedDeltaTime;
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                float randDeg = Random.Range(0, 360f);
                Vector3 pos = new Vector3(SpawnRange * Mathf.Cos(randDeg), SpawnRange * Mathf.Sin(randDeg), 10);
                GameObject enemy = Instantiate(enemyFab, pos, Quaternion.identity);
                enemy.GetComponent<EnemyScript>().Initialise();
                float lowestTime = startSpawnTime * Mathf.Pow(0.65f, amountwaves);
                if (startSpawnTime < 0.1f)
                {
                    startSpawnTime = 0.1f;
                }
                timer = Random.Range(lowestTime, lowestTime*3);
            }
        }
        if (waveTimer < 0 && active == true)
        {
            active = false;
            amountwaves++;
            abAmount = Mathf.FloorToInt(amountwaves/2) + 2;
            if (abAmount > 6) abAmount = 6;
            allowTake = Mathf.FloorToInt(abAmount / 2);
            StartCoroutine(WaveIntermission());
            
        }
    }

    IEnumerator WaveIntermission()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        while (enemyList.Length > 0)
        {
            yield return new WaitForSeconds(0.1f);
            enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        }
        GameObject.Find("CannonShooter").GetComponent<CannonScript>().setAllowShoot(false);
        GameObject[] kogels = GameObject.FindGameObjectsWithTag("Kogel");
        foreach (GameObject kogel in kogels)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject.Find("CannonShooter").GetComponent<CannonScript>().AddBall(kogel);
            Instantiate((GameObject)Resources.Load("Particles/Clink"), kogel.transform.position, Quaternion.identity);
            gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        }
        if (amountwaves > 0)
        {
            GameObject.Find("CannonBody").GetComponent<CannonManager>().Regenerate();
            for (int i = 0; i < abAmount; i++)
            {
                yield return new WaitForSeconds(1f);
                float randItem = Random.Range(0f, 10f);
                spawnAb(i);
            }
            GameObject.Find("CannonShooter").GetComponent<CannonScript>().setAllowShoot(true);
        }
        GameObject.Find("ScoreManager").GetComponent<ScoreScript>().AddScore((amountwaves+1)*10);

    }

    public void TakeItem()
    {
        allowTake--;
        if (allowTake <= 0)
        {
            foreach (GameObject gm in optionList)
            {
                if(gm != null)
                {
                    gm.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
            IEnumerator removeItems()
            {
                foreach (GameObject gm in optionList)
                {
                    yield return new WaitForSeconds(0.1f);
                    if (gm == null) continue;
                    gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
                    Instantiate((GameObject)Resources.Load("Particles/EnemySpawn"), gm.transform.position, Quaternion.identity);
                    Destroy(gm);
                }
                optionList.Clear();
                if (/*CalcIsPrime(amountwaves + 2) && amountwaves<10*/ amountwaves % 2 ==0 && amountwaves<=12)
                {
                    yield return new WaitForSeconds(0.2f);
                    spawnPortal();
                    yield return new WaitForSeconds(1f);
                }
                waveTimer = maxWaveTimer;
                active = true;
                
            }
            StartCoroutine(removeItems());
        }
    }

    
    void spawnAb(int i)
    {
        Vector3 pos = new Vector3(SpawnRange/2.5f * Mathf.Cos(i*45), SpawnRange / 2.5f * Mathf.Sin(i * 45), 10);
        Vector3 offset = new Vector3(0, 0, 10);
        GameObject ab = Instantiate(aberration, pos + offset, Quaternion.identity);
        Instantiate((GameObject)Resources.Load("Particles/EnemySpawn"), ab.transform.position, Quaternion.identity);
        ab.GetComponent<AberattionScript>().Initialise();
        optionList.Add(ab);
    }

    void spawnPortal()
    {
        float RandDeg = Random.Range(0f, 360f);
        Vector3 pos = new Vector3(SpawnRange / 1.2f * Mathf.Cos(RandDeg), SpawnRange / 1.2f * Mathf.Sin(RandDeg), 10);
        Vector3 offset = new Vector3(0, 0, 10);
        GameObject portal = Instantiate(miniPortal, pos + offset, Quaternion.identity);
    }

    public void setActive()
    {
        StartCoroutine(startGame());
    }

    public void loseGame()
    {
        startText.SetActive(true);
        active = false;
        amountwaves = 0;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemyList)
        {
            Instantiate((GameObject)Resources.Load("Particles/Clink"), enemy.transform.position, Quaternion.identity);
            Destroy(enemy);
        }
        GameObject[] kogelList = GameObject.FindGameObjectsWithTag("Kogel");
        foreach(GameObject kogel in kogelList)
        {
            Instantiate((GameObject)Resources.Load("Particles/Clink"), kogel.transform.position, Quaternion.identity);
            Destroy(kogel);
        }
        GameObject[] abilityList = GameObject.FindGameObjectsWithTag("Ab");
        foreach(GameObject ab in abilityList)
        {
            Instantiate((GameObject)Resources.Load("Particles/Clink"), ab.transform.position, Quaternion.identity);
            Destroy(ab);
        }
    }

    public int getWaves()
    {
        return amountwaves;
    }

    public bool isActive()
    {
        return active;
    }
}
