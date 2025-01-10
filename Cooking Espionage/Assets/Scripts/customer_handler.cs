using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer_handler : MonoBehaviour
{
    [SerializeField] GameObject Customer1;
    [SerializeField] GameObject Customer2;
    [SerializeField] GameObject Customer3;
    [SerializeField] GameObject Customer4;
    [SerializeField] Rigidbody rb;
    [SerializeField] Kitchen_Handler kitchen_handler;
    [SerializeField] AudioClip panHitSound;
    

    // Start is called before the first frame update
    void Start()
    {

        setSkin();
        

    }

    private void Update()
    {
        //deleteCheck();
    }

    public void setSkin()
    {
        Customer1.SetActive(false);
        Customer2.SetActive(false);
        Customer3.SetActive(false);
        Customer4.SetActive(false);

        switch(Random.Range(0, 4))
        {
            case 0:
                Customer1.SetActive(true);

                break;
            case 1:
                Customer2.SetActive(true);

                break;
            case 2:
                Customer3.SetActive(true);

                break;
            case 3:
                Customer4.SetActive(true);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Pan")
        {
            print("pan hit");
            SFXManager.instance.playSFX(panHitSound, transform, 0.5f);
            StartCoroutine(killCustomer());

        }
    }

    IEnumerator killCustomer()
    {
        rb.isKinematic = false;
        rb.AddForce(new Vector3(0,0,500), ForceMode.Impulse);
        yield return new WaitForSeconds(2f);
        kitchen_handler.pan_hit();
        Destroy(gameObject);
        

    }

    void deleteCheck()
    {
        if (transform.position.x < 4)
        {
            Destroy(gameObject);
        }
    }
}
