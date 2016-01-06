using UnityEngine;
using System.Collections;

public class Layers
{
    // A list of tag strings.
    public static LayerMask Obstacles = 1 << 8;
    public static LayerMask Ground = 1 << 9;
    public static LayerMask Buildings = 1 << 10;
}
