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
}