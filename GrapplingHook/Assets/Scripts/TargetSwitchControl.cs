using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSwitchControl
{
    public static int targetColor;

    public static int TargetType
    {
        get
        {
            return targetColor;
        }
        set
        {
            targetColor = value;
        }
    }
}
//yes1,no2,box3,enemy4