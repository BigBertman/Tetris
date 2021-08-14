using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{

    public float lastFall = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1.0f, 0.0f, 0.0f);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            }
        }

        // Move Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1.0f, 0.0f, 0.0f);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
            }
        }

        // Rotate
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0.0f, 0.0f, -90.0f);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.Rotate(0.0f, 0.0f, 90.0f);
            }
        }

        // Move Downwards and Fall
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0.0f, -1.0f, 0.0f);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0.0f, 1.0f, 0.0f);

                Playfield.deleteAllFullRows();

                FindObjectOfType<Spawner>().SpawnRandomGroup();

                enabled = false;
            }
        }

        if (Time.deltaTime * lastFall >= 3)
        {
            transform.position += new Vector3(0.0f, -1.0f, 0.0f);

            if (isValidGridPos())
            {
                updateGrid();
                lastFall = 0;
            }
            else
            {
                transform.position += new Vector3(0.0f, 1.0f, 0.0f);

                Playfield.deleteAllFullRows();

                FindObjectOfType<Spawner>().SpawnRandomGroup();

                enabled = false;
            }
        }



        lastFall++;
    }

    bool isValidGridPos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2 v = Playfield.roundVec2(transform.GetChild(i).position);

            // If Not inside Boarder...
            if (!Playfield.insideBorder(v))
            {
                return false;
            }

            // If there's already is a block in the same grid entry...
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        {
            for (int y = 0; y < Playfield.h; ++y)
            {
                for (int x = 0; x < Playfield.w; ++x)
                {
                    if (Playfield.grid[x, y] != null)
                    {
                        if (Playfield.grid[x, y].parent == transform)
                        {
                            Playfield.grid[x, y] = null;
                        }
                    }
                }
            }
        }

        // Add new children to grid
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector2 v = Playfield.roundVec2(transform.GetChild(i).position);
                Playfield.grid[(int)v.x, (int)v.y] = transform.GetChild(i);
            }
        }
    }
}