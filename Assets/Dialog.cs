using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    [TextArea(3,15)]
    public string[] sentences;

    public Sprite[] images;
}
