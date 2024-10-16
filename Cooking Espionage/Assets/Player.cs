using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float grab_range = 7;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("e"))
        {
            Interact();
        }
        
    }

    void Interact()
    {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit_info;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit_info, grab_range))
        {
            
            if (hit_info.collider != null)
            {
                switch (hit_info.collider.tag)
                {
                    case "Ingredient":
                        onInteract(hit_info);
                        break;
                    case "Utility":
                        onUtilityInteract(hit_info);
                        break;
                        
                }
            }
        }


    }

    void onInteract(RaycastHit hit_info)
    {
        Debug.Log(hit_info.distance);
        Debug.Log(hit_info.collider);

    }
    void onUtilityInteract(RaycastHit hit_info)
    {
        Debug.Log(hit_info.distance);
        Debug.Log(hit_info.collider);
        Debug.Log("But its a fridge");

    }
}
