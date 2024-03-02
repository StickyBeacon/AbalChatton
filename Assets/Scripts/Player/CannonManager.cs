using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    int HP = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            CannonHit();
            Destroy(collision.gameObject);
        }
    }
    public void CannonHit()
    {
        print("ow!!");
        HP--;
        if(HP == 0)
        {
            Debug.Log("You Lose!");
            //end game
        }
    }
}
