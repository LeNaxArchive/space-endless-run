using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformmove : MonoBehaviour
{
    private float speed = 4f;  // initial movement speed
    private float timer = 0f;     // timer to track 1-second intervals

    void Update()
    {
        // Move forward based on current speed
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Increase timer by the time passed since last frame
        timer += Time.deltaTime;

        // Every 1 second, increase speed
        if (timer >= 1f)
        {
            speed += 0.5f;
            timer = 0f;
        }
    }
}
