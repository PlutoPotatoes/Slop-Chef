using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toppings_dispenser : MonoBehaviour
{
    [SerializeField] string toppingType;

    public string get_topping()
    {
        return toppingType;
    }
}
