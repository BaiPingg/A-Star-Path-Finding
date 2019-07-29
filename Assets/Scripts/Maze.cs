using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Maze

{
    public const int OBLIQUE = 14;
    public const int STEP = 10;
    public int[,] MazeArray { get; private set; }
    private List<Point> CloseList;
    private List<Point> OpenList;

    public Maze(int[,] maze)
    {
        this.MazeArray = maze;
        OpenList = new List<Point>(MazeArray.Length);
        CloseList = new List<Point>(MazeArray.Length);
    }

    public Point FindPath(Point start, Point end, bool IsIgnoreCorner)
    {
        OpenList.Add(start);
        while (OpenList.Count != 0)
        {
            //找出F值最小的点
            var tempStart = OpenList.MinPoint();
            OpenList.RemoveAt(0);
            CloseList.Add(tempStart);
            //找出它相邻的点
            var surroundPoints = SurrroundPoints(tempStart, IsIgnoreCorner);
            foreach (Point point in surroundPoints)
            {
                if (OpenList.Exists(point))
                    //计算G值, 如果比原来的大, 就什么都不做, 否则设置它的父节点为当前点,并更新G和F
                    FoundPoint(tempStart, point);
                else
                    //如果它们不在开始列表里, 就加入, 并设置父节点,并计算GHF
                    NotFoundPoint(tempStart, end, point);
            }
            if (OpenList.Get(end) != null)
                return OpenList.Get(end);
        }
        return OpenList.Get(end);
    }

    private void FoundPoint(Point tempStart, Point point)
    {
        var G = CalcG(tempStart, point);
        if (G < point.G)
        {
            point.ParentPoint = tempStart;
            point.G = G;
            point.CalcF();
        }
    }

    private void NotFoundPoint(Point tempStart, Point end, Point point)
    {
        point.ParentPoint = tempStart;
        point.G = CalcG(tempStart, point);
        point.H = CalcH(end, point);
        point.CalcF();
        OpenList.Add(point);
    }

    private int CalcG(Point start, Point point)
    {
        int G = (Math.Abs(point.X - start.X) + Math.Abs(point.Y - start.Y)) == 2 ? STEP : OBLIQUE;
        int parentG = point.ParentPoint != null ? point.ParentPoint.G : 0;
        return G + parentG;
    }

    private int CalcH(Point end, Point point)
    {
        int step = Math.Abs(point.X - end.X) + Math.Abs(point.Y - end.Y);
        return step * STEP;
    }

    //获取某个点周围可以到达的点
    public List<Point> SurrroundPoints(Point point, bool IsIgnoreCorner)
    {
        var surroundPoints = new List<Point>(9);

        for (int x = point.X - 1; x <= point.X + 1; x++)
            for (int y = point.Y - 1; y <= point.Y + 1; y++)
            {
                if (CanReach(point, x, y, IsIgnoreCorner))
                    surroundPoints.Add(x, y);
            }
        return surroundPoints;
    }

    //在二维数组对应的位置不为障碍物
    private bool CanReach(int x, int y)
    {
        return MazeArray[x, y] == 0;
    }

    public bool CanReach(Point start, int x, int y, bool IsIgnoreCorner)
    {
        if (!CanReach(x, y) || CloseList.Exists(x, y))
            return false;
        else
        {
            if (Math.Abs(x - start.X) + Math.Abs(y - start.Y) == 1)
                return true;
            //如果是斜方向移动, 判断是否 "拌脚"
            else
            {
                if (CanReach(Math.Abs(x - 1), y) && CanReach(x, Math.Abs(y - 1)))
                    return true;
                else
                    return IsIgnoreCorner;
            }
        }
    }
}