using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    int HP = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CannonHit()
    {
        HP--;
        if(HP == 0)
        {
            Debug.Log("You Lose!");
            //end game
        }
    }
}
