using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoor : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] Kitchen_Handler kitchen_handler;
    [SerializeField] AudioClip doorNoise;


    private void OnTriggerEnter(Collider other)
    {
        doorAnimator.SetBool("InOpenZone", true);
        SFXManager.instance.playSFX(doorNoise, transform, 0.5f);
        kitchen_handler.play_final_cutscene();
    }
    void Start()
    {
        transform.position = new Vector3(-1.88116446e-05f, -0.0844599977f, -0.00212999992f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
