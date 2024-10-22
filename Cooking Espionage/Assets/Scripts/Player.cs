using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float grab_range = 7;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;



    private ObjectGrabbable heldObject;
    private float interactBuffer = 0.2f;
    private float interactCooldown;
    private float throwStrength = 500.0f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputListener();

        
    }

    private void FixedUpdate()
    {
        cooldownHandler(Time.deltaTime);
    }

    private void inputListener()
    {
        if (Input.GetKeyDown("e") && interactCooldown <=0)
        {
            Interact();
        }
        if(Input.GetKeyDown("r") && heldObject != null)
        {
            throwObject();
        }

    }

    private void cooldownHandler(float delta)
    {
        if (interactCooldown > 0)
        {
            interactCooldown -= delta;
        }

    }

    void Interact()
    {
        if (heldObject == null)
        {
            RaycastHit hit_info;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit_info, grab_range))
            {

                if (hit_info.collider != null)
                {
                    switch (hit_info.collider.tag)
                    {
                        case "Ingredient":
                            onInteract(hit_info);
                            interactCooldown = interactBuffer;
                            break;
                        case "Utility":
                            onUtilityInteract(hit_info);
                            interactCooldown = interactBuffer;
                            break;
                        case "Dispenser":
                            onDispenserInteract(hit_info);
                            interactCooldown = interactBuffer;
                            break;


                    }
                }
            }
        }
        else
        {
            heldObject.Grab(null);
            heldObject = null;
        }


    }

    void onInteract(RaycastHit hit_info)
    {
        hit_info.transform.TryGetComponent(out ObjectGrabbable objectGrabbable);
        heldObject = objectGrabbable;
        objectGrabbable.Grab(objectGrabPointTransform);

    }
    void onUtilityInteract(RaycastHit hit_info)
    {
        Debug.Log(hit_info.distance);
        Debug.Log(hit_info.collider);
        Debug.Log("But its a fridge");

    }

    void onDispenserInteract(RaycastHit hit_info)
    {
        hit_info.transform.TryGetComponent(out Dispenser dispenser);
        ObjectGrabbable dispensedObject = dispenser.CreateObject();
        heldObject = dispensedObject;
        dispensedObject.Grab(objectGrabPointTransform);
    }

    void throwObject()
    {
        Vector3 throwForce = cameraTransform.forward * throwStrength;
        heldObject.Grab(null);
        heldObject.throwObject(throwForce);
        heldObject = null;

    }
}
