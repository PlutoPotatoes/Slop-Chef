using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{

    private Rigidbody objectRigidBody;
    private Transform objectGrabPoint;
    public LayerMask collideWith;
    private bool held;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        held = false;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPoint = objectGrabPointTransform;
        held = (objectGrabPoint != null);
    }

    private void FixedUpdate()
    {
        if(objectRigidBody != null)
        {
            updateObject();
        }
    }

    private void updateObject()
    {
        if (objectGrabPoint != null)
        {
            float lerp = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPoint.position, Time.deltaTime * lerp);
            objectRigidBody.MovePosition(newPosition);
            //objectRigidBody.isKinematic = true;
            objectRigidBody.useGravity = false;
        }
        else
        {
            objectRigidBody.useGravity = true;
            objectRigidBody.isKinematic = false;
        }
    }

    
    public void throwObject(Vector3 throwForce)
    {
        objectRigidBody.isKinematic = false;
        objectRigidBody.AddForce(throwForce, ForceMode.Impulse);
    }

    public bool isHeld()
    {
        return held;
    }
}
