using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject tempCube;

    private Maze maze;
    private Vector3 start;

    public Vector3 end;
    private int[,] array;
    private bool canchangeway = false;

    private void Start()
    {
        array = new int[,] {
                           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1,1},
                           { 1, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0,0, 1, 0, 0, 0, 0,1},
                           { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0,1},
                           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0,0, 1, 0, 1, 1, 1,1},
                           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0,0, 1, 0, 1, 1, 1,1},
                           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0,0, 1, 0, 1, 1, 1,1},
                           { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0,1},
                           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0,0, 1, 0, 0, 1, 1,1},
                           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0,0, 1, 0, 1, 1, 1,1},
                           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0,0, 1, 0, 1, 1, 1,1},
                           { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0,0, 0, 0, 1, 1, 0,1},
                           { 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0,1},
                           { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0,0, 0, 1, 0, 0, 0,1},
                           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1,1}
        };

        maze = new Maze(array);
        GenerateCube(array);
        start = new Vector3(1, 0, 1);
        //end = new Vector3(12, 0, 18);
        ShowPath(start, end, maze);
    }

    private void Update()
    {
    }

    private void ShowPath(Vector3 s, Vector3 e, Maze maze)
    {
        canchangeway = false;
        Refresh(array);
        Point start = new Point((int)s.x, (int)s.z);
        GameObject.Find(start.X + "," + start.Y).GetComponent<Cube>().CubeColor = ColorEnum.begin;
        Point end = new Point((int)e.x, (int)e.z);
        GameObject.Find(end.X + "," + end.Y).GetComponent<Cube>().CubeColor = ColorEnum.end;
        var parent = maze.FindPath(start, end, false);

        Debug.Log("Print path:");
        List<Point> path = new List<Point>();
        while (parent != null)
        {
            Debug.Log(parent.X + ", " + parent.Y);
            path.Add(parent);
            parent = parent.ParentPoint;
        }
        StartCoroutine(ShowCube(path));
    }

    private void Refresh(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                GameObject obj = GameObject.Find(i + "," + j);
                if (array[i, j] == 0)
                {
                    obj.GetComponent<Cube>().CubeColor = ColorEnum.air;
                }
                if (array[i, j] == 1)
                {
                    obj.GetComponent<Cube>().CubeColor = ColorEnum.block;
                }
                obj.name = i + "," + j;
            }
        }
    }

    private IEnumerator ShowCube(List<Point> path)
    {
        for (int i = path.Count - 2; i > 0; i--)
        {
            yield return new WaitForSeconds(.2f);
            GameObject.Find(path[i].X + "," + path[i].Y).GetComponent<Cube>().CubeColor = ColorEnum.path;
        }
        canchangeway = true;
    }

    private void GenerateCube(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Vector3 vector = new Vector3(i, 0, j);
                GameObject obj = Instantiate(tempCube, vector, Quaternion.identity);
                if (array[i, j] == 0)
                {
                    obj.GetComponent<Cube>().CubeColor = ColorEnum.air;
                }
                if (array[i, j] == 1)
                {
                    obj.GetComponent<Cube>().CubeColor = ColorEnum.block;
                }
                obj.name = i + "," + j;
            }
        }
    }
}