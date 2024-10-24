using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{
    private CapsuleCollider foodPickupZone;
    private bool plateFull = false;
    private bool movingFood = false;
    private Collider heldObject;

    // Start is called before the first frame update
    void Start()
    {
        foodPickupZone = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingFood)
        {
            moveFood();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!plateFull && other.tag == "Finished_Food")
        {
            heldObject = other;
            movingFood = true;
            plateFull = true;

        }
    }
    private void moveFood()
    {
        heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, this.transform.GetChild(0).position, 0.5f);
        if (heldObject.transform.position== this.transform.GetChild(0).position)
        {
            movingFood = false;
            heldObject.TryGetComponent(out Rigidbody objectRigidbody);
            Destroy(objectRigidbody);
            heldObject.gameObject.transform.root.parent = this.gameObject.transform;
            heldObject.TryGetComponent(out ObjectGrabbable objectGrabbable);
            objectGrabbable.Grab(null);
        }

    }

}
