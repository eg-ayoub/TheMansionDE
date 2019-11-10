using System;
using UnityEngine;
/// <summary>
/// intermediate monobehaviour between an array of sensors and the collisionsensorscript
/// </summary>
public class IntermediateSensorManager : GameplayObject
{
    /// <summary>
    /// array of sensors by which we decide if there is a collision.
    /// </summary>
    [SerializeField] SensorOnOff[] sensorArray;
    /// <summary>
    /// size of the array
    /// </summary>
    int ArraySize;
    /// <summary>
    /// number of sensors that are on
    /// </summary>
    int SensorsOn;
    /// <summary>
    /// whether there is actualluy a collision
    /// </summary>
    public bool on;
    void Start()
    {
        ArraySize = sensorArray.Length;
        paused = false;
    }
    /// <summary>
    /// if more than half the sensors are on, we assume there is a collision
    /// </summary>
    void FixedUpdate()
    {
        SensorsOn = 0;
        on = false;
        for (int i = 0; i < ArraySize; i++)
        {
            if (sensorArray[i].GetState())
            {
                SensorsOn += 1;
            }
        }
        // detect if really on
        if (2 * SensorsOn >= ArraySize)
        {
            on = true;
        }
    }

    internal void ResetSensorArray()
    {
        foreach (SensorOnOff sensor in sensorArray)
        {
            sensor.ResetState();
        }
    }
}
