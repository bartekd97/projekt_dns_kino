using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Utility
{
    public static float NormalizeRotation(float rot)
    {
        if (rot > 180f)
            return rot - 360f;
        return rot;
    }
    public static float InterpolateRotation(float a, float b, float t)
    {
        a = NormalizeRotation(a);
        b = NormalizeRotation(b);
        float diff = b - a;
        if (Mathf.Abs(diff - 360f) < Mathf.Abs(diff))
            diff = diff - 360f;
        return b + diff * t;
    }
}
