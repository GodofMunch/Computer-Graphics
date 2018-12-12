using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerVector2 : MonoBehaviour {

    private int x;
    private int y;

    public IntegerVector2(Vector2 point,int height, int width)
    {
        this.x = convertX(point.x, width);
        this.y = convertY(point.y, height);
    }

    public IntegerVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int convertX(float x, int width)
    {
        int converted = (int)(x + 1) / 2 * (width - 1);
        return converted;
    }

    public int convertY(float y, int height)
    {
        int converted = (int)(1 - y) / 2 * (height - 1);
        return converted;

    }

    public IntegerVector2(float x, float y)
    {
        this.x = Mathf.RoundToInt(x);
        this.y = Mathf.RoundToInt(y);
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }
}
