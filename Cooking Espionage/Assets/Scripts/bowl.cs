using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl: MonoBehaviour
{

    private CapsuleCollider foodPickupZone;
    private bool plateFull = false;
    private bool movingFood = false;
    private Collider heldObject;
    private GameObject slop;
    private Material slopHeld;

    // Start is called before the first frame update
    void Start()
    {
        foodPickupZone = GetComponent<CapsuleCollider>();
        slop = transform.GetChild(0).gameObject;
        slop.SetActive(false);
        slopHeld = null;
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

    public void setSlop(Material slopType)
    {
        slop.GetComponent<SpriteRenderer>().material = slopType;
        slopHeld = slopType;
        slop.SetActive(true);
                
    }

    public Material getSlopType()
    {
        return slopHeld;
    }

}
