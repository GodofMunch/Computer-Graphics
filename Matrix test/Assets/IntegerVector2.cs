using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerVector2 : MonoBehaviour {

    private int x1;
    private int x2;
    private int y1;
    private int y2;
    private int dx;
    private int dy;
    private int TwoXDyMinusDx;

    public IntegerVector2(Vector2 start, Vector2 end)
    {
        convertVector2ToInt(start);
        convertVector2ToInt(end);
    }

    public IntegerVector2()
    {

    }
    
    public void convertVector2ToInt(Vector2 point)
    {
        
    }
}
