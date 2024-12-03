using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoor : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;


    private void OnTriggerEnter(Collider other)
    {
        doorAnimator.SetBool("InOpenZone", true); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
