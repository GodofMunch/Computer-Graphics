using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasterize : MonoBehaviour
{
    int height = Screen.height;
    int width = Screen.width;

    // Use this for initialization
    void Start()
    {

        //For testing purposes
        Vector2 start = new Vector2(234, 502);
        Vector2 end = new Vector2(241, 506);

        IntegerVector2 startPoint = new IntegerVector2(start, height, width);
        IntegerVector2 endPoint = new IntegerVector2(end, height, width);

        List<IntegerVector2> toBeDrawn = rasterize(startPoint, endPoint);

        print(toBeDrawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    public List<IntegerVector2> rasterize(IntegerVector2 start, IntegerVector2 end)
    {
        List<IntegerVector2> toBeDrawn = new List<IntegerVector2>();

        int dx = end.getX() - start.getX();
        int dy = end.getY() - end.getY();
        int TwoDy = 2 * dy;
        int TwoDYminusDX = TwoDy - dx;
        int p = TwoDYminusDX;
        int y = 0;

        bool XandYSwapped = false;

        if (dx < 0)
        {
            toBeDrawn = rasterize(end, start);
        }

        if (dy < 0)
        {
            toBeDrawn = rasterize(negateY(start), negateY(end));
            
            toBeDrawn = NegateYList(toBeDrawn);
        }

        if(dy > dx)
        {
            swapXandY(start);
            swapXandY(end);
            XandYSwapped = true;
        }

        y = start.getY();
        for(int i = start.getX(); i < end.getX(); i ++)
        {
            
            if(p > 0)
            {
                y++;
                p = p - TwoDYminusDX;
            }

            else
            {
                p = p + TwoDy;
            }

            toBeDrawn.Add(new IntegerVector2(i, y));
        }



        if (XandYSwapped)
        {
            toBeDrawn = swapXandY(toBeDrawn);
        }

        return toBeDrawn;

    }

    public void swapXandY(IntegerVector2 point)
    {
        int holder = point.getX();
        point.setX(point.getY());
        point.setY(holder);
    }

    public List<IntegerVector2> swapXandY(List<IntegerVector2> myList)
    {
        List<IntegerVector2> swapped = new List<IntegerVector2>();

        foreach (IntegerVector2 point in myList)
            swapped.Add(new IntegerVector2(point.getY(), point.getX()));
        return swapped;
  
    }

    public IntegerVector2 negateY(IntegerVector2 point)
    {
        return new IntegerVector2(point.getX(), -point.getY());
    }

    public List<IntegerVector2> NegateYList(List<IntegerVector2> myList)
    {
        List<IntegerVector2> negated = new List<IntegerVector2>();

        foreach (IntegerVector2 v in myList) {
            negated.Add(negateY(v));
                }

        return negated;
    }
}
