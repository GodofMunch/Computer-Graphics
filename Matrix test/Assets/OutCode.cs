using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCode {


    Vector2[] screenBounds = { new Vector2(1f, 1f), new Vector2(1f, -1f), new Vector2(-1f, -1f), new Vector2(-1f, 1f) };
    bool[] udlr = new bool[4];
    private object p1;
    private object p2;
    private object p3;
    private object p4;

    public OutCode(Vector2 v1)
    {
        udlr[0] = v1.y > screenBounds[0].y;
        udlr[1] = v1.y < screenBounds[1].y;
        udlr[2] = v1.x < screenBounds[2].x;
        udlr[3] = v1.x > screenBounds[3].x;
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
        this.p1 = up;
        this.p2 = down;
        this.p3 = left;
        this.p4 = right;
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

    //to go in matrices script

    /*void line_clip(Vector2 v1, Vector2 v2)

    {
        OutCode o1 = new OutCode(v1);
        OutCode o2 = new OutCode(v2);

        if ((o1 == new OutCode()) && (o2 == new OutCode()))
        {// trivially Accept}
        }

        if ((o1 & o2) != new OutCode())
        {//trivially reject     }
        }
    }*/

}
	

