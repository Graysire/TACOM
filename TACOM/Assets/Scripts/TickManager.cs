using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grayson Hill
 * Last Updated: 12/919
 */

//Manages ticks which trigger after a given amount of time
public class TickManager : MonoBehaviour
{
    static float timeSinceTick = 0; //the number of seconds since the last tick
    static float tickTime = 2; //the time between ticks

    public delegate void OnTick();
    public static event OnTick onTick;

    // Update is called once per frame
    void Update()
    {
        timeSinceTick += Time.deltaTime;
        if (timeSinceTick >= tickTime)
        {
            timeSinceTick = 0;
            onTick?.Invoke();
        }
    }
}
