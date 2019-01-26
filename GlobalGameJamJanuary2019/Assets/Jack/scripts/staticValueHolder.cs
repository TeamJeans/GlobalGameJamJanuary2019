using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds values between scenes
public static class StaticValueHolder
{
    //initialise here
    private static int dailyMoney = 0, currentDay = 0, currentWeek = 0, totalMoney = 0;
    private static int[] dayValues = new int[7];
    private static int hitPoints = 0, maxHitPoints = 0;

    public static int[] CookingResult
    {
        get { return CookingResult; }
        set { CookingResult = value; }
    }
}