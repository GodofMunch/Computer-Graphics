using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCode {


    Vector2[] screenBounds = { new Vector2(1f, 1f), new Vector2(1f, -1f), new Vector2(-1f, -1f), new Vector2(-1f, 1f) };
    public bool[] udlr = new bool[4];

    public OutCode(Vector2 v1)
    {
        udlr[0] = v1.y > 1;
        udlr[1] = v1.y < -1;
        udlr[2] = v1.x < -1;
        udlr[3] = v1.x > 1;
    }

    public OutCode()
    {
        udlr[0] = false;
        udlr[1] = false;
        udlr[2] = false;
        udlr[3] = false;
    }

    public OutCode(bool up, bool down, bool left, bool right)
    {
        udlr[0] = up;
        udlr[1] = down;
        udlr[2] = left;
        udlr[3] = right;
    }

    public static bool operator == (OutCode a, OutCode b)
    {

        return (a.udlr[0] == b.udlr[0]) && (a.udlr[1] == b.udlr[1]) && (a.udlr[2] == b.udlr[2]) && (a.udlr[3] == b.udlr[3]);
    }

    public static  bool operator != (OutCode a, OutCode b)
    {

        return !(a == b);
    }

    public static OutCode operator &(OutCode a, OutCode b)
    {
        return new OutCode(a.udlr[0] && b.udlr[0], a.udlr[1] && b.udlr[1], a.udlr[2] && b.udlr[2], a.udlr[3] && b.udlr[3]);

    }

    public static OutCode operator +(OutCode a, OutCode b)
    {
        return new OutCode(a.udlr[0] || b.udlr[0], a.udlr[1] || b.udlr[1], a.udlr[2] || b.udlr[2], a.udlr[3] || b.udlr[3]);
    }
}
	

