/**
 * Filename:            FloatingPlatformController.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  17/12/2021
 * Program Description: Controls the behaviour of floating platforms.
 * Revision History:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatingPlatformController : MonoBehaviour
{
    private GameObject player;
    private GameObject child_platform;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        foreach (Transform child in transform)
        {
            if (child.name == "Platform")
            {
                child_platform = child.gameObject;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            var currentScale = child_platform.transform.localScale;
            currentScale.x = currentScale.x * 0.999f;
            child_platform.transform.localScale = currentScale;
        }
    }

    public void Reset()
    {

    }
}
