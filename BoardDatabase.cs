using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoardDatabase : MonoBehaviour
{
    public GameObject tile;
    public int xsize = 10;
    public int zsize = 10;
    public GameObject[,] Board;
    public GameObject[,] ground;
    public GameObject[] onboardunits;
    public GameObject[] props;
    public int SpawnChance = 9;
    public int Turn = 0;
    public SetTextToValue TurnUI;

    public GameObject unit;
    
    public Vector2 spawnpoint = Vector2.zero;
    void Start()
    {
        Board = new GameObject[xsize, zsize];
        ground = new GameObject[xsize, zsize];
        
        for (int x = 0; x < xsize; x++)
        {
            for (int z = 0; z < zsize; z++)
            {
                if ((x < 2 || x > xsize - 3) || (z < 2 || z > zsize - 3))
                {
                    if (UnityEngine.Random.Range(0, 2) == 1)
                    {
                        ground[x, z] = Instantiate(tile, new Vector3(2 * x, 0f, 2 * z), Quaternion.identity,
                            gameObject.transform);
                        ground[x, z].GetComponent<Quickaccesscords>().setXY(x, z);
                    }
                }
                else
                {
                    ground[x, z] = Instantiate(tile, new Vector3(2 * x, 0f, 2 * z), Quaternion.identity,
                        gameObject.transform);
                    ground[x, z].GetComponent<Quickaccesscords>().setXY(x, z);
                }
            }
        }

        for (int x = 0; x < xsize; x++)
        {
            for (int y = 0; y < zsize; y++)
            {
                if (ground[x, y])
                {
                    if (UnityEngine.Random.Range(0, SpawnChance) == 1)
                    {
                        Board[x, y] = Instantiate(props[UnityEngine.Random.Range(0, props.Length)], indextocords(x, 1, y),
                            Quaternion.identity);
                    }
                }
            }
        }
        
        Board[(int) spawnpoint.x, (int) spawnpoint.y] = Instantiate(unit,
            new Vector3(spawnpoint.x * 2, 1f, spawnpoint.y * 2), Quaternion.identity);
        
        //Quickaccesscords unitqac = Board[(int) spawnpoint.x, (int) spawnpoint.y].AddComponent<Quickaccesscords>();
        //unitqac.setXY((int) spawnpoint.x, (int) spawnpoint.y);

        Quickaccesscords unitQAC = Board[(int) spawnpoint.x, (int) spawnpoint.y].GetComponent<Quickaccesscords>();
        unitQAC.setXY((int) spawnpoint.x, (int) spawnpoint.y);
        
        Board[(int) spawnpoint.x + 1, (int) spawnpoint.y + 1] = Instantiate(unit,
            new Vector3(spawnpoint.x * 2 + 2, 1f, spawnpoint.y * 2 + 2), Quaternion.identity);
        
        //Quickaccesscords unitqac = Board[(int) spawnpoint.x, (int) spawnpoint.y].AddComponent<Quickaccesscords>();
        //unitqac.setXY((int) spawnpoint.x, (int) spawnpoint.y);

        unitQAC = Board[(int) spawnpoint.x + 1, (int) spawnpoint.y + 1].GetComponent<Quickaccesscords>();
        unitQAC.setXY((int) spawnpoint.x + 1, (int) spawnpoint.y + 1);

        refreshunitlist();
    }

    public void moevunit(int unitx, int unity, int desx, int desy)
    {
        if (Board[unitx, unity])
        {
            Quickaccesscords unitQAC = Board[unitx, unity].GetComponent<Quickaccesscords>();

                Board[desx, desy] = Board[unitx, unity];
                unitQAC.setXY(desx, desy);

                Board[unitx, unity] = null;
            
        }
        else
        {
            Debug.LogError("No Units at " + unitx + " : " + unity);
        }
    }

    public bool tileisempty(int x, int y)
    {
        bool condition = !Board[x, y];
        return condition;
    }

    public void refreshunitlist()
    {
        onboardunits = GameObject.FindGameObjectsWithTag("Unit");
    }

    public void passturn()
    {
        Turn++;
        foreach (var unit in onboardunits)
        {
            unit.GetComponent<UnitProperties>().canmove = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            passturn();
        }
        
        TurnUI.SetText(Turn.ToString());
    }

    public Vector3 indextocords(int x, int y, int z)
    {
        Vector3 pos = new Vector3(x * 2, y, z * 2);
        return pos;
    }
}
