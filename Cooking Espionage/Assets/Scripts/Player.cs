using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float grab_range = 7;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform objectGrabPointReset;




    private ObjectGrabbable heldObject;
    private float interactBuffer = 0.2f;
    private float interactCooldown;
    private float throwStrength = 500.0f;
    private float objectDistance;



    // Start is called before the first frame update
    void Start()
    {
        objectDistance = objectGrabPointTransform.localPosition.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        inputListener();
        objectReleaseCheck();

        
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
        // OBjectGrabPoint z can move between min 5 and max 7
        if(Input.mouseScrollDelta.y > 0 && objectDistance < 8f)
        {
            objectGrabPointTransform.SetPositionAndRotation(objectGrabPointTransform.position + (cameraTransform.forward * 0.1f), objectGrabPointTransform.rotation);
            objectDistance = objectGrabPointTransform.localPosition.z;


        }
        if (Input.mouseScrollDelta.y < 0 && objectDistance > 4.5f)
        {
            objectGrabPointTransform.SetPositionAndRotation(objectGrabPointTransform.position - (cameraTransform.forward * 0.1f), objectGrabPointTransform.rotation);
            objectDistance = objectGrabPointTransform.localPosition.z;
            Debug.Log(objectDistance);


        }
    }
    private void objectReleaseCheck()
    {
        if (heldObject != null && heldObject.isHeld() == false){
            heldObject = null;
            
        }else if (heldObject == null)
        {
            objectGrabPointTransform.position = objectGrabPointReset.position;
            objectDistance = objectGrabPointReset.localPosition.z;
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
                            break;
                        case "Utility":
                            onUtilityInteract(hit_info);
                            break;
                        case "Dispenser":
                            onDispenserInteract(hit_info);
                            break;
                        case "Finished_Food":
                            onInteract(hit_info);
                            break;
                        case "Plate":
                            onInteract(hit_info);
                            break;
                        case "Bowl":
                            onInteract(hit_info);
                            break;

                    }
                    interactCooldown = interactBuffer;
                    return;

                }
            }
        }else if (heldObject.tag == "Plate")
        {
            RaycastHit hit_info;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit_info, grab_range))
            {
                if (hit_info.collider.tag == "Plate_Dispenser")
                {
                    heldObject.TryGetComponent(out plate plateScript);
                    if (plateScript.plateFull)
                    {
                        Debug.Log("Plate is full");
                    }
                    else
                    {
                        plateScript.getGruel();
                    }
                    return;
                    // add food to plate
                }
                else if (hit_info.collider.tag == "Bowl_Dispenser")
                {
                    Debug.Log("Wrong dish dumbass");
                    return;
                }
                }
        }
        else if (heldObject.tag == "Bowl")
        {
            RaycastHit hit_info;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit_info, grab_range))
            {
                if (hit_info.collider.tag == "Bowl_Dispenser")
                {
                    heldObject.TryGetComponent(out bowl bowlScript);
                    if (bowlScript.bowlFull)
                    {
                        Debug.Log("bowl is full");
                    }
                    else
                    {
                        Material slopType = hit_info.transform.GetChild(0).GetComponent<SpriteRenderer>().material;
                        bowlScript.setSlop(slopType);
                    }
                    
                    return;
                }
                else if (hit_info.collider.tag == "Plate_Dispenser")
                {
                    Debug.Log("Wrong dish dumbass");
                    return;
                }
            }

        }
       
        heldObject.Grab(null);
        heldObject = null;



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
