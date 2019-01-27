using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds values between scenes
public static class StaticValueHolder
{
    //initialise here
    private static int currentNight = 1;

    public static int CurrentNight
    {
        get { return currentNight; }
        set { currentNight = value; }
    }
}