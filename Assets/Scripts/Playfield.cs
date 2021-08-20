using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{

    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideBorder(Vector2 pos)
    {
        bool result = false;

        if ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0)
        {
            result = true;
        }

        return result;
    }

    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                // Move Group down one unit
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update block position
                grid[x, y - 1].position += new Vector3(0.0f, -1.0f, 0.0f);
            }
        }
    }

    public static void DecreaseAboveRows(int y)
    {
        for (int i = y; i < h; i++)
        {
            DecreaseRow(i);
        }
    }

    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DeleteAllFullRows()
    {
        for (int y = 0; y < h; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseAboveRows(y + 1);
                --y;
            }
        }
    }
}
