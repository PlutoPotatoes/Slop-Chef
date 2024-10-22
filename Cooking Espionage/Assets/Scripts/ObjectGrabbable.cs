using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{

    private Rigidbody objectRigidBody;
    private Transform objectGrabPoint;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPoint = objectGrabPointTransform;
    }

    private void FixedUpdate()
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

}
