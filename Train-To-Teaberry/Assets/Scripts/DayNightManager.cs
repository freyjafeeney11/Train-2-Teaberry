using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightScript : MonoBehaviour
{
    public Volume ppv; // Post Processing Volume
    public GameObject[] lights; // All the lights we want on when it's dark

    public bool activateLights; // Checks if lights are on

    // Variables for time calculation
    private int hour;
    private int minutes;
    private int day = 1;

    // Reference to the post exposure effect
    private ColorAdjustments colorAdjustments;

    private void Start()
    {

        // Ensure the post-processing volume has the necessary effect
        ppv.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        // Subscribe to TimeManager's event to get the updated time
        TimeManager.OnDateTimeChanged += UpdateTime;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        TimeManager.OnDateTimeChanged -= UpdateTime;
    }

    // This method will be called when the time is updated from TimeManager
    private void UpdateTime(DateTime newDateTime)
    {
        hour = newDateTime.Hour;
        minutes = newDateTime.Minute;
        day = newDateTime.Day;

        // Control the day-night cycle whenever the time changes
        ControlPPV();
    }

    // Control the post-processing and lights based on time of day
    void ControlPPV()
    {
        int minutesUntilDusk = GetMinutesUntilDusk();
        int minutesUntilDawn = GetMinutesUntilDawn();

        if (hour >= 21 && hour < 22) // Dusk at 9 PM
        {
            ppv.weight = (float)minutes / 60; // Gradually increase post-processing effect
            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(0f, 1f, (float)minutes / 60); // Adjust post exposure
            }

            if (!activateLights && minutes > 45) // Turn lights on if dark
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(true);
                }
                activateLights = true;
            }
        }
        else if (hour >= 6 && hour < 7) // Dawn at 6 AM
        {
            ppv.weight = 1 - (float)minutes / 60; // Gradually decrease post-processing effect
            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(1f, 0f, (float)minutes / 60); // Adjust post exposure
            }

            if (activateLights && minutes > 45) // Turn lights off if bright
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(false);
                }
                activateLights = false;
            }
        }

        // Debug: Print how long until dusk or dawn
        Debug.Log($"Minutes until Dusk: {minutesUntilDusk} minutes.");
        Debug.Log($"Minutes until Dawn: {minutesUntilDawn} minutes.");
    }

    // Get the time difference until dusk (9 PM)
    int GetMinutesUntilDusk()
    {
        if (hour < 21)
        {
            return (21 - hour) * 60 - minutes;
        }
        else if (hour == 21)
        {
            return 0; // It's already dusk
        }
        else
        {
            return (24 - hour + 21) * 60 - minutes; // Wrap around midnight if past dusk
        }
    }

    // Get the time difference until dawn (6 AM)
    int GetMinutesUntilDawn()
    {
        if (hour < 6)
        {
            return (6 - hour) * 60 - minutes;
        }
        else if (hour == 6)
        {
            return 0; // It's already dawn
        }
        else
        {
            return (24 - hour + 6) * 60 - minutes; // Wrap around midnight if past dawn
        }
    }
}
