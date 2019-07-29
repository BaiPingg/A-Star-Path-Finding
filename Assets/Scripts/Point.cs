using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Point
{
    public Point ParentPoint { get; set; }
    public int F { get; set; }//F=G+H
    public int G { get; set; }
    public int H { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void CalcF()
    {
        F = G + H;
    }
}

public static class ListHelper
{
    public static bool Exists(this List<Point> points, Point point)
    {
        foreach (Point p in points)
            if ((p.X == point.X) && (p.Y == point.Y))
                return true;
        return false;
    }

    public static bool Exists(this List<Point> points, int x, int y)
    {
        foreach (Point p in points)
            if ((p.X == x) && (p.Y == y))
                return true;
        return false;
    }

    public static Point MinPoint(this List<Point> points)
    {
        points = points.OrderBy(p => p.F).ToList();
        return points[0];
    }

    public static void Add(this List<Point> points, int x, int y)
    {
        Point point = new Point(x, y);
        points.Add(point);
    }

    public static Point Get(this List<Point> points, Point point)
    {
        foreach (Point p in points)
            if ((p.X == point.X) && (p.Y == point.Y))
                return p;
        return null;
    }

    public static void Remove(this List<Point> points, int x, int y)
    {
        foreach (Point point in points)
        {
            if (point.X == x && point.Y == y)
                points.Remove(point);
        }
    }
}