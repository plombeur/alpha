using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Utils
{
    public float Vector2Det(Vector2 reference,Vector2 target)
    {
        return reference.x * target.y - reference.y * target.x;
    }

    public bool isLeft(Vector2 reference,Vector2 target)
    {
        return Vector2Det(reference, target) > 0;
    }

    public static Vector2 vectorFromAngle(float angle, float length = 1)
    {
        angle = angle % 360;
        return new Vector2(length * Mathf.Cos(Mathf.Deg2Rad * angle), length * Mathf.Sin(Mathf.Deg2Rad * angle));
    }

    public static float angleFromVector(Vector2 v)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(v.y, v.x);
    }

    public static List<Vector2> getOrthogonalsVectors(Vector2 vDirection)
    {
        List<Vector2> results = new List<Vector2>();
        results.Add(new Vector2(-vDirection.y,  vDirection.x));
        results.Add(new Vector2( vDirection.y, -vDirection.x));
        return results;
    }

    public static float sizeOfVector(Vector2 vector)
    {
        return Vector2.Distance(Vector2.zero, vector);
    }
}