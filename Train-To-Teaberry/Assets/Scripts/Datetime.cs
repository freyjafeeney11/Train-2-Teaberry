public class DateTime
{
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }
    public int Season { get; private set; }

    // Define seasons as before
    private static readonly string[] Seasons = { "Spring", "Summer", "Autumn", "Winter" };

    public DateTime(int day, int month, int year, int hour, int minute)
    {
        Day = day;
        Month = month;
        Year = year;
        Hour = hour;
        Minute = minute;
        Season = Month / 3; // You can adjust this logic as needed
    }

    // Advance the time by a given number of minutes
    public void AdvanceMinutes(int minutesToAdvance)
    {
        Minute += minutesToAdvance;

        // Handle overflow of minutes
        while (Minute >= 60)
        {
            Minute -= 60;
            Hour++;
        }

        // Handle overflow of hours for 12-hour clock (AM/PM)
        while (Hour >= 24)
        {
            Hour -= 24;
            Day++;
        }

        // Handle overflow of days and change month/season if needed
        while (Day > 28)
        {
            Day -= 28;  // Assuming all months have 28 days for simplicity
            Month++;

            if (Month > 4)
            {
                Month = 1;
                Season++;
                if (Season > 3) Season = 0;  // Loop seasons
            }
        }

        // Handle new year
        if (Month > 4)
        {
            Month = 1;
            Year++;
        }
    }

    // Return a formatted date with AM/PM
    public string GetFormattedDate()
    {
        string ampm = Hour < 12 ? "AM" : "PM";
        int displayHour = Hour % 12;
        if (displayHour == 0) displayHour = 12; // Convert 0 to 12 for AM/PM format

        return $"{Seasons[Season]} {Month}/{Day}/{Year} {displayHour}:{Minute:D2} {ampm}";
    }
}
