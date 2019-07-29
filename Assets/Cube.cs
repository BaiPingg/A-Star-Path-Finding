using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private ColorEnum cubeColor;

    public ColorEnum CubeColor
    {
        get
        {
            return cubeColor;
        }

        set
        {
            cubeColor = value;
            switch (cubeColor)
            {
                case ColorEnum.block:
                    GetComponent<MeshRenderer>().material.color = Color.red;
                    break;

                case ColorEnum.air:
                    GetComponent<MeshRenderer>().material.color = Color.gray;
                    break;

                case ColorEnum.path:
                    GetComponent<MeshRenderer>().material.color = Color.blue;
                    break;

                case ColorEnum.begin:
                    GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;

                case ColorEnum.end:
                    GetComponent<MeshRenderer>().material.color = Color.black;
                    break;
            }
        }
    }
}