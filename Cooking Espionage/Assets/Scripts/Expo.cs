using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expo : MonoBehaviour

{

    private HashSet<HashSet<string>> order_contents = new HashSet<HashSet<string>>();
    private HashSet<HashSet<string>> order = new HashSet<HashSet<string>>();

    private ArrayList heldObjects = new ArrayList();


    void Start()
    {

    }


    public void set_order(HashSet<HashSet<string>> new_order)
    {
        order_contents.Clear();
        order.Clear();
        housekeeping();
        order = new_order;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("object in");

        if (other.tag == "Bowl")
        {
            other.gameObject.TryGetComponent(out bowl bowlScript);
            order_contents.Add(bowlScript.get_contents());
            heldObjects.Add(other);
            
        }
        else if (other.tag == "Plate")
        {
            other.gameObject.TryGetComponent(out plate plateScript);
            order_contents.Add(plateScript.get_contents());
            heldObjects.Add(other);
        }

        

    }
    private void OnTriggerExit(Collider other)
    {
        print("object out");
        if (other.tag == "Bowl")
        {
            other.TryGetComponent(out bowl bowlScript);
            order_contents.Remove(bowlScript.get_contents());
            heldObjects.Remove(other);
        }
        else if (other.tag == "Plate")
        {
            other.TryGetComponent(out plate plateScript);
            order_contents.Remove(plateScript.get_contents());
            heldObjects.Remove(other);
        }
    }

    public bool isOrderComplete()
    {
        foreach(HashSet<string> reqItem in order)
        {
            bool found = false;
            foreach(HashSet<string> item in order_contents)
            {
                if (item.SetEquals(reqItem))
                {
                    found = true;
                    break;
                }

            }
            if (!found)
            {
                return false;
            }
        }
        return true;
    }

    public void housekeeping()
    {
        foreach(Collider thing in heldObjects)
        {
            Destroy(thing.gameObject);
        }
        heldObjects.Clear();
    }


}
