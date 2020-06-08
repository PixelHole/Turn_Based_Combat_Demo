using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_movement : MonoBehaviour
{
    public BoardDatabase Database;
    
    public LayerMask raycast_ignore;
    public Transform Selectioncursor;
    public GameObject selectedUnit;
    public GameObject movementrangeindicator;
    public GameObject tilecursor;

    public int defaultwalkrange = 2; // this will be used only for testing purposes. do not let it stay in the final product ok? did i spell purpose right? 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100f, raycast_ignore))
            {
                if (hit.transform.CompareTag("Unit"))
                {
                    UnitProperties unitproperty = hit.transform.gameObject.GetComponent<UnitProperties>();
                    
                    if (unitproperty.canmove)
                    {
                        selectedUnit = hit.transform.gameObject;
                        Quickaccesscords unitQAC = selectedUnit.GetComponent<Quickaccesscords>();
                        placeCursor(selectedUnit.transform.position);
                        showmovementrange(unitQAC.x, unitQAC.y, defaultwalkrange);
                    }
                    else
                    {
                        selectedUnit = null;
                    }
                }

                if (selectedUnit && !hit.transform.CompareTag("Unit"))
                {
                    deselectactiveunit();
                    hidecursor();
                    destroyindicators();
                }
            }
            else
            {
                deselectactiveunit();
                hidecursor();
                destroyindicators();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, raycast_ignore))
            {
                if (hit.transform.CompareTag("Tile") && selectedUnit)
                {
                    Quickaccesscords unitQAC = selectedUnit.GetComponent<Quickaccesscords>();
                    UnitProperties unitProperties = selectedUnit.GetComponent<UnitProperties>();
                    Quickaccesscords tileQAC = hit.transform.GetComponent<Quickaccesscords>();
                    
                    if (isinmovementrange(tileQAC.x, tileQAC.y, defaultwalkrange))
                    {
                        Database.moevunit(unitQAC.x, unitQAC.y, tileQAC.x, tileQAC.y);
                        unitProperties.canmove = false;
                        placeCursor(new Vector3(tileQAC.x * 2, 0f, tileQAC.y * 2));
                        deselectactiveunit();
                        hidecursor();
                        destroyindicators();
                    }
                }
            }
        }

        if (selectedUnit)
        {
            
        }
    }


    void hidecursor()
    {
        Selectioncursor.transform.position = new Vector3(-20f, -20f, -20f);
    }

    void deselectactiveunit()
    {
        selectedUnit = null;
    }

    void placeCursor(Vector3 pos)
    {
        Selectioncursor.transform.position = new Vector3(pos.x, 2f, pos.z);
    }

    public bool isinmovementrange(int desx, int desy, int range)
    {
        Quickaccesscords unitQAC = selectedUnit.GetComponent<Quickaccesscords>();
        bool condition = false;
        
        if (desx <= unitQAC.x + range && desx >= unitQAC.x - range)
        {
            if (desy <= unitQAC.y + range && desy >= unitQAC.y - range)
            {
                if (!Database.Board[desx, desy])
                {
                    condition = true;
                }
                else
                {
                    Debug.LogError("tile [" + desx + " , " + desy + "] is full");
                }
            }
        }
        return condition;
    }

    public GameObject[] indicators;

    void destroyindicators()
    {
        foreach (var cube in indicators)
        {
            Destroy(cube);
        }
    }
    void showmovementrange(int x, int y, int range)
    {
        destroyindicators();
        
        indicators = new GameObject[(range * 2 + 1) * (range * 2 + 1)];

        for (int i = x - range, index = 0; i <= x + range; i++)
        {
            for (int j = y - range; j <= y + range; j++)
            {
                int cordx = Mathf.Clamp(i, 0, Database.xsize - 1);
                int cordy = Mathf.Clamp(j, 0, Database.zsize - 1);
                
                if (!Database.Board[cordx, cordy] && Database.ground[cordx, cordy])
                {
                    indicators[index] = Instantiate(movementrangeindicator, new Vector3(cordx * 2, 1f, cordy * 2), 
                        Quaternion.identity);
                    
                    index++;
                }
            }
        }
    }
}
