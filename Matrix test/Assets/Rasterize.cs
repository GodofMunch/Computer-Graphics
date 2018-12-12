using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasterize : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IntegerVector2 convertVector2ToResolution(Vector2 point, int width, int height)
    {

        int x = (int)((point.x + 1) / 2 * (width - 1));
        int y = (int)((1 - point.y) / 2 * (height - 1));

        return new IntegerVector2(x, y);
    }

    public List<IntegerVector2> rasterize(Vector2 start, Vector2 end)
    {
        List<IntegerVector2> toBeDrawn = new List<IntegerVector2>();

        int dx = end.x - start.x;
        int dy = end.y - start.y;
        int TwoDy = 2 * dy;
        int TwoDyMinusDx = TwoDy - dx;

        if (dx < 0)
        {
            toBeDrawn = rasterize(end, start);
        }

        if (dy < 0)
        {
            toBeDrawn = rasterize(negateY(start), negateY(end));
            negateYList(toBeDrawn);
        }



    }

    public IntegerVector2 negateY(Vector2 point)
    {
        return new IntegerVector2(point.x, -point.y);
    }

    public List<IntegerVector2> NegateYList(List<IntegerVector2> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            myList.IndexOf(i) = new IntegerVector2(myList.IndexOf(i).x, -(myList.IndexOf(i).y));
        }
    }
}
