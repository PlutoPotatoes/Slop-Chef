using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expo : MonoBehaviour
{
    private HashSet<HashSet<string>> order_contents = new HashSet<HashSet<string>>();
    private HashSet<HashSet<string>> order = new HashSet<HashSet<string>>();

    void Start()
    {
        new_order();
        HashSet<string> order1 = new HashSet<string>();
        order1.Add("gruel");
        order1.Add("eyeballs");
        order1.Add("syrup");
        HashSet<string> order2 = new HashSet<string>();
        order2.Add("slop_strawberry");
        order.Add(order2);
        order.Add(order1);
    }

    public void new_order()
    {
        order_contents.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bowl")
        {
            other.gameObject.TryGetComponent(out bowl bowlScript);
            order_contents.Add(bowlScript.get_contents());
            
        }
        else if (other.tag == "Plate")
        {
            other.gameObject.TryGetComponent(out plate plateScript);
            order_contents.Add(plateScript.get_contents());
        }

        Debug.Log(isOrderComplete());
        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bowl")
        {
            other.TryGetComponent(out bowl bowlScript);
            order_contents.Remove(bowlScript.get_contents());
        }
        else if (other.tag == "Plate")
        {
            other.TryGetComponent(out plate plateScript);
            order_contents.Remove(plateScript.get_contents());
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
}
