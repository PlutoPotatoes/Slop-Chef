using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer_disposal : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -3.2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("got one");
        Destroy(other.gameObject);
    }
}
