using UnityEngine;
using System.Collections;

public class Layers
{
    // A list of tag strings.
    public static LayerMask Obstacles = 1 << 8;
    public static LayerMask Players = 1 << 9;
    public static LayerMask Enemies = 1 << 10;
}
