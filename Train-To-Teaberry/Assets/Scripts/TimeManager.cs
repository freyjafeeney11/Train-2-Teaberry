using UnityEngine;
using UnityEngine.UI; // Don't forget to add this for UI elements
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Date & Time Settings")]
    [Range(1, 28)] public int dateInMonth;
    [Range(1, 4)] public int season;
    [Range(1, 99)] public int year;
    [Range(0, 24)] public int hour;
    [Range(0, 6)] public int minutes;
    public int TickSecondsIncrease = 1; // Reduced to make the time pass slower

    private DateTime dateTime;
    
    [Header("Tick Settings")]
    public float TimeBetweenTicks = 3; // Slower time progression (higher value makes it slower)
    private float currentTimeBetweenTicks = 0;

    public static UnityAction<DateTime> OnDateTimeChanged;

    // Reference to the UI Text element
    public Text dateTimeText;

    private void Awake() 
    {
        dateTime = new DateTime(dateInMonth, season - 1, year, hour, minutes * 10);
        Debug.Log($"Starting Date: {dateTime.GetFormattedDate()}");
    }

    private void Start() 
    {
        OnDateTimeChanged += UpdateDateTimeText; // Subscribe to the event
        OnDateTimeChanged?.Invoke(dateTime);
    }

    private void Update() 
    {
        currentTimeBetweenTicks += Time.deltaTime;
        if (currentTimeBetweenTicks >= TimeBetweenTicks) 
        {
            currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    void Tick() 
    {
        AdvanceTime();
    }

    void AdvanceTime() 
    {
        dateTime.AdvanceMinutes(TickSecondsIncrease);
        OnDateTimeChanged?.Invoke(dateTime);
    }

    // This method will update the UI Text element whenever the time changes
    void UpdateDateTimeText(DateTime newDateTime)
    {
        if (dateTimeText != null)
        {
            dateTimeText.text = newDateTime.GetFormattedDate(); // Set the formatted date to the UI Text
        }
    }
}
