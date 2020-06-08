using System;
using UnityEngine;

public class Quickaccesscords : MonoBehaviour
{
    public int x, y;
    public bool SetPosition = true;
    public void setXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 cordVector2()
    {
        return new Vector2(x, y);
    }

    private void Update()
    {
        if (SetPosition)
        {
            transform.position = new Vector3(x * 2, transform.position.y, y * 2);
        }
    }
}
