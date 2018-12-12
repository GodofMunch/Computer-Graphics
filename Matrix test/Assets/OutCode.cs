using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCode : MonoBehaviour{

    void Start()
    {
        /*Vector2 start = new Vector2(0.7f, 0.8f);
        Vector2 end = new Vector2(-0.7f, -0.8f);
        if (line_clip(ref start, ref end))
        {
            print("The point " + start.ToString() + "," + end.ToString() + " was Accepted");
        }

        start = new Vector2(-2f, 1.5f);
        end = new Vector2(.8f, 1.5f);

        if (!line_clip(ref start, ref end))
        {
            print("The point " + start.ToString() + "," + end.ToString() + " was rejected");
        }

        start = new Vector2(0f, 1.5f);
        end = new Vector2(1.5f, 0f);

        if (line_clip(ref start, ref end))
        {
            print("The point " + start.ToString() + "," + end.ToString() + " was accepted");
        }
        */
    }
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

    public static bool triviallyAccept(OutCode a, OutCode b) 
    {
        return a == new OutCode() && b == new OutCode();
    }

    public static bool triviallyReject(OutCode a, OutCode b)
    {
        return (a & b) != new OutCode();
    }

    private bool line_clip(ref Vector2 startPoint, ref Vector2 endPoint)
    {
        OutCode startOutcode = new OutCode(startPoint);
        OutCode endOutcode = new OutCode(endPoint);


        if (triviallyAccept(startOutcode, endOutcode))
        {
            return true;
        }

        if (triviallyReject(startOutcode, endOutcode))
        {
            return false;
        }


        for (int udlr = 0; udlr < 4; udlr++)
        {
            if (startOutcode.udlr[udlr])
            {
                startPoint = lineIntercept(startPoint, endPoint, udlr);
                if (new OutCode(startPoint) == new OutCode())// new point in viewport
                {
                    return line_clip(ref endPoint, ref startPoint);
                }
            }
        }

        return true;
    }

    private Vector2 lineIntercept(Vector2 startPoint, Vector2 endPoint, int udlr)
    {
        float m = getSlope(startPoint, endPoint);

        switch (udlr)
        {
            case 0: // Top Edge  y = 1

                return new Vector2(startPoint.x + (1 / m) * (1 - startPoint.y), 1);

            case 1: // Bottom Edge  y = -1

                return new Vector2(startPoint.x + (1 / m) * (-1 - startPoint.y), -1);

            case 2: // Left Edge x = -1

                return new Vector2(-1, (startPoint.y + (m * (-1 - startPoint.x))));

            case 3: // Right Edge x = 1

                return new Vector2(1, (startPoint.y + (m * (1 - startPoint.x))));

            default:

                return new Vector2();
        }
    }

    private float getSlope(Vector2 v1, Vector2 v2)
    {
        return (v2.y - v1.y) / (v2.x - v1.x);
    }

}


