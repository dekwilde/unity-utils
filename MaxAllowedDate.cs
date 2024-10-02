using System;
using UnityEngine;

public class MaxAllowedDate : MonoBehaviour
{
    public DateTime _maxAllowedDate;

    void Start()
    {
        if (DateTime.Now > _maxAllowedDate)
        {
            Application.Quit();
        }
    }

}
