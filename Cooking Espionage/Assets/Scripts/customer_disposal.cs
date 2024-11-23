using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer_disposal : MonoBehaviour
{
    [SerializeField] GameObject expo;
    Expo expoScript;

    // Start is called before the first frame update
    void Start()
    {
         expoScript = expo.GetComponent<Expo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pan")
        {
            expoScript.hide_pan();
        }
    }
}
