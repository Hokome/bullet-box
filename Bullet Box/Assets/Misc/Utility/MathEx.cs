﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
public static class MathEx
{
    /// <summary>
    /// Rotate a Vector by degrees on the Z axis
    /// </summary>
    /// <param name="v">Vector to rotate</param>
    /// <param name="degrees">Degrees by which the Vector should be rotated</param>
    /// <returns></returns>
    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    /// <summary>
    /// Clamp the angle of a vector between two angles (works on both sides)
    /// </summary>
    /// <param name="vector">Vector to clamp</param>
    /// <param name="minAngle">Minimum angle</param>
    /// <param name="maxAngle">Maximum angle</param>
    /// <returns></returns>
    public static Vector2 ClampAngle(Vector2 vector, float minAngle, float maxAngle)
    {
        float magnitude = vector.magnitude;

        //Convert angles to radians
        minAngle *= Mathf.Deg2Rad;
        maxAngle *= Mathf.Deg2Rad;

        //Get vectors representing angles
        Vector2 min = new Vector2(Mathf.Cos(minAngle), Mathf.Sin(minAngle)) * magnitude; 
        Vector2 max = new Vector2(Mathf.Cos(maxAngle), Mathf.Sin(maxAngle)) * magnitude;

		if (vector.y < min.y)
            vector = new Vector2(min.x * Mathf.Sign(vector.x), min.y);
        else if (vector.y > max.y)
            vector = new Vector2(max.x * Mathf.Sign(vector.x), max.y);

        return vector;
    }

    /// <summary>
    /// Returns true if the magnitude of comp is greater than the magnitude of v once comp projected
    /// </summary>
    /// <param name="v"></param>
    /// The base for the comparison
    /// <param name="comp"></param>
    /// The cb
    /// <returns></returns>
    public static bool GreaterProjected(Vector2 v, Vector2 comp)
	{
        comp *= Mathf.Cos(Vector2.Angle(v, comp));
        return comp.sqrMagnitude > v.sqrMagnitude;
	}

    public static bool SameSign(float a, float b) => (a < 0 && b < 0) || (a > 0 && b > 0);
}