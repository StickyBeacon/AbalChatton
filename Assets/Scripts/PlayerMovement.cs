using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    Vector2 mousePos;
    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)*Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    // some code from https://www.youtube.com/watch?v=LNLVOjbrQj4
}
