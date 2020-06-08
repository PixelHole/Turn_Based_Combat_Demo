using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeontile : MonoBehaviour
{
    public LayerMask raycast_ignore;
    public SpriteRenderer Rend; 
    void Start()
    {
        Rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, raycast_ignore))
        {
            Vector3 hitpos = hit.transform.position;
            
            if (!Rend.enabled)
            {
                Rend.enabled = true;
            }

            gameObject.transform.position = new Vector3(hitpos.x, 1.001f, hitpos.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(-100f, 1.001f, -100f);
        }
    }
}
