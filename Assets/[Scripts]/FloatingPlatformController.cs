/**
 * Filename:            FloatingPlatformController.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  17/12/2021
 * Program Description: Controls the behaviour of floating platforms.
 * Revision History:
 *      - 17/12/2021 - Floating Platform initial implementation of shrinking behaviour
 *      - 17/12/2021 - Expand the platform if the player is not standing on it
 *      - 17/12/2021 - Shrink the platform faster than expanding
 *      - 17/12/2021 - Add platform sound effects for shrinking and expanding
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Floating platform sound enumeration
public enum PlatformSounds
{
   SHRINK,
   EXPAND
}

// This class controls the floating platorms in the level
[System.Serializable]
public class FloatingPlatformController : MonoBehaviour
{
    private GameObject player;
    private GameObject child_platform;
    private float movingTimer;
    private float inactiveTimer;
    private Vector3 startingPosition;
    private AudioSource[] sounds;

    // True if player is standing on the platform, false otherwise
    public bool isActive = false;

    // Value used to shrink the platform each game tick when the player is standing on it
    public float shrinkingConstant = 0.001f;

    // Valued used to expand the platform each game tick when the player is not standing on it
    public float expandingConstant = 0.001f;

    // Time that needs to pass after player is no longer standing on the platform for the platform to start expanding
    public float expandTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sounds = GetComponents<AudioSource>();

        foreach (Transform child in transform)
        {
            if (child.name == "Platform")
            {
                child_platform = child.gameObject;
                break;
            }
        }

        startingPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust Platform scale based on ativity
        var newScale = child_platform.transform.localScale;
        bool isMoving = false;
        if (isActive)
        {
            if (newScale.x > 0)
            {
                if (!sounds[(int)PlatformSounds.SHRINK].isPlaying)
                    sounds[(int)PlatformSounds.SHRINK].Play();
                newScale.x = newScale.x - shrinkingConstant;
                isMoving = true;
            }
            inactiveTimer = 0;
        }
        else
        {
            if (newScale.x < 1) inactiveTimer += Time.deltaTime;
        }

        // Expand after 2 seconds
        if (!isActive && inactiveTimer > expandTime && newScale.x < 1)
        {
            if (!sounds[(int)PlatformSounds.EXPAND].isPlaying)
                sounds[(int)PlatformSounds.EXPAND].Play();
            newScale.x = newScale.x + expandingConstant;
            isMoving = true;
        }

        if (!isMoving)
        {
            sounds[(int)PlatformSounds.SHRINK].Stop();
            sounds[(int)PlatformSounds.EXPAND].Stop();
        }

        child_platform.transform.localScale = newScale;
        movingTimer += Time.deltaTime;
        _Move();
    }

    void _Move()
    {
        var newPosition = gameObject.transform.position;
        newPosition.y = startingPosition.y + Mathf.PingPong(movingTimer, 0.5f);
        gameObject.transform.position = newPosition;
    }
}
