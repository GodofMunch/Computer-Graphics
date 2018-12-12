using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerVector2 : MonoBehaviour {

    private int x;
    private int y;

    public IntegerVector2(Vector2 point)
    {
        x = (int)point.x;
        y = (int)point.y;
    }

    public IntegerVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
