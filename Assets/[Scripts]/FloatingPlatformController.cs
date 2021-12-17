/**
 * Filename:            FloatingPlatformController.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  17/12/2021
 * Program Description: Controls the behaviour of floating platforms.
 * Revision History:
 *      - 17/12/2021 - Floating Platform initial implementation of shrinking behaviour
 *      - 17/12/2021 - Expand the platform if the player is not standing on it
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
    private const float SHRINKING_CONSTATNT = 0.999f;

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
        // Adjust Platform scale based on ativity
        var newScale = child_platform.transform.localScale;
        if (isActive)
        {
            newScale.x = newScale.x * SHRINKING_CONSTATNT;
        }
        else
        {
            if (newScale.x < 1) newScale.x = (2 - SHRINKING_CONSTATNT) * newScale.x;
        }
        child_platform.transform.localScale = newScale;
    }
}
