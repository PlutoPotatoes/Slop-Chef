using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{
    private CapsuleCollider foodPickupZone;
    private bool plateFull = false;
    private bool movine = false;
    private bool heldObject;

    // Start is called before the first frame update
    void Start()
    {
        foodPickupZone = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!plateFull && other.tag == "Finished_Food")
        {
            other.gameObject.transform.root.parent = this.gameObject.transform;
            other.TryGetComponent<Rigidbody>(out Rigidbody objectRigidBody);
            objectRigidBody.MovePosition(this.transform.GetChild(0).position);
            Destroy(objectRigidBody);
            other.TryGetComponent(out ObjectGrabbable objectGrabbable);
            objectGrabbable.Grab(null);


        }
    }

    private void moveToCenter()
    {

    }
}
