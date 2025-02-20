using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSchedule : MonoBehaviour
{
    [System.Serializable]
    public class ScheduleEntry
    {
        public int hour;
        public int minute;
        public List<Transform> waypoints; // Multiple waypoints for movement
    }

    public List<ScheduleEntry> schedule;
    private int currentScheduleIndex = 0;
    private bool isMoving = false;
    public float movementSpeed = 2f;
    private static List<NPCSchedule> allNPCs = new List<NPCSchedule>();

    private void Start()
    {
        allNPCs.Add(this);
        TimeManager.OnDateTimeChanged += OnTimeChanged;
    }

    private void OnDestroy()
    {
        allNPCs.Remove(this);
        TimeManager.OnDateTimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(DateTime newDateTime)
    {
        foreach (var npc in allNPCs)
        {
            npc.CheckSchedule(newDateTime);
        }
    }

    private void CheckSchedule(DateTime dateTime)
    {
        if (schedule.Count == 0 || isMoving) return;

        ScheduleEntry nextEntry = schedule[currentScheduleIndex];

        if (dateTime.Hour == nextEntry.hour && dateTime.Minute >= nextEntry.minute)
        {
            StartCoroutine(MoveThroughWaypoints(nextEntry.waypoints));
            currentScheduleIndex = (currentScheduleIndex + 1) % schedule.Count;
        }
    }

    private IEnumerator MoveThroughWaypoints(List<Transform> waypoints)
    {
        isMoving = true;

        foreach (Transform waypoint in waypoints)
        {
            yield return MoveToWaypoint(waypoint);
        }

        isMoving = false;
    }

private IEnumerator MoveToWaypoint(Transform waypoint)
{
    Vector3 startPosition = transform.position;
    float distance = Vector3.Distance(startPosition, waypoint.position);
    float travelTime = distance / movementSpeed;
    float elapsedTime = 0f;

    yield return null; // Ensures coroutine starts properly

    while (elapsedTime < travelTime)
    {
        transform.position = Vector3.Lerp(startPosition, waypoint.position, elapsedTime / travelTime);
        elapsedTime += Time.fixedDeltaTime; 
        yield return new WaitForFixedUpdate();
    }

    transform.position = waypoint.position; // Snap to final position
}

}
