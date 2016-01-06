using UnityEngine;
using System.Collections;

public class Chance
{
    private static float randValue = 0;
    public static bool Check(float chance)
    {
        if (chance < 1)
        {
            randValue = Random.value;
            if (randValue <= chance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
