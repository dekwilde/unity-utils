using System;
using UnityEngine;

[Serializable]
public class CustomDate
{
    public int Year;
    public int Month;
    public int Day;

    public DateTime ToDateTime()
    {
        return new DateTime(Year, Month, Day);
    }
}

public class MaxAllowedDate : MonoBehaviour
{
    public CustomDate maxAllowedDate;

    void Start()
    {
        DateTime selectedDate = maxAllowedDate.ToDateTime();
        if (DateTime.Now > selectedDate)
        {
            Application.Quit();
        }
    }
}
