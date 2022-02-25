using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFKSystemData
{
    static int levelTime = 0;
    static int levelWork = 0;

    public static void levelUpTime()
    {
        levelTime++;
    }

    public static void levelUpWork()
    {
        levelWork++;
    }

    public static ulong getPrice(string name)
    {
        ulong price = 0;
        return price;
    }
}
