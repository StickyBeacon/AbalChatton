using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransitionScript : MonoBehaviour
{
    bool isBig = true;
    bool isSmall = false;
    public AudioClip inClip;
    public AudioClip outClip;
    void Start()
    {
        gameObject.GetComponent<Image>().enabled = true;
        GameObject.Find("CannonBody").GetComponent<AudioSource>().PlayOneShot(inClip);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBig)
        {
            gameObject.transform.localScale += new Vector3(-50, -50, -50);
            if (gameObject.transform.localScale.magnitude < 60f)
            {
                gameObject.GetComponent<Image>().enabled = false;
                isBig = false;
            }
        }
        if (isSmall)
        {
            gameObject.transform.localScale += new Vector3(50, 50, 50);
        }
        
    }
    public void transOut()
    {
        isSmall = true;
        GameObject.Find("CannonBody").GetComponent<AudioSource>().PlayOneShot(outClip);
        gameObject.GetComponent<Image>().enabled = true;
    }
}
