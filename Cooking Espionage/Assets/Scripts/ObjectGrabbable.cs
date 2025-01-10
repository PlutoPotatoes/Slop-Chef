using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] AudioClip hitAudio;
    [SerializeField] AudioClip hitAudio2;
    [SerializeField] AudioClip hitAudio3;
    [SerializeField] AudioClip hitAudio4;

    [SerializeField] float hitSoundThreshold;


    private Rigidbody objectRigidBody;
    private Transform objectGrabPoint;
    public LayerMask collideWith;
    private bool held;
    public AudioClip[] audioClips;
    
    private void Awake()
    {
        audioClips = new AudioClip[] { hitAudio, hitAudio2, hitAudio3, hitAudio4};


        objectRigidBody = GetComponent<Rigidbody>();
        held = false;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPoint = objectGrabPointTransform;
        held = (objectGrabPoint != null);
    }

    private void FixedUpdate()
    {
        if(objectRigidBody != null)
        {
            updateObject();
        }
    }

    private void updateObject()
    {
        if (objectGrabPoint != null)
        {
            float lerp = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPoint.position, Time.deltaTime * lerp);
            objectRigidBody.MovePosition(newPosition);
            //objectRigidBody.isKinematic = true;
            objectRigidBody.useGravity = false;
        }
        else
        {
            objectRigidBody.useGravity = true;
            objectRigidBody.isKinematic = false;
        }
    }

    
    public void throwObject(Vector3 throwForce)
    {
        objectRigidBody.isKinematic = false;
        objectRigidBody.AddForce(throwForce, ForceMode.Impulse);
    }

    public bool isHeld()
    {
        return held;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(Mathf.Min(collision.impulse.magnitude, 100) * 0.001f);
        if(collision.impulse.magnitude > hitSoundThreshold)
        {
            
            SFXManager.instance.playSFX(audioClips[(int)Random.Range(0, audioClips.Length)], transform, Mathf.Min(collision.impulse.magnitude, 100)*0.001f);
        }
    }
}
