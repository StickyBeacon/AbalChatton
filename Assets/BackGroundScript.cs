using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = new Vector3(0, 8*Mathf.Cos(Time.time/16), 0);
    }
}
