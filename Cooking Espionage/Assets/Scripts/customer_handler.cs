using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer_handler : MonoBehaviour
{
    [SerializeField] GameObject Customer1;
    [SerializeField] GameObject Customer2;
    [SerializeField] GameObject Customer3;
    [SerializeField] GameObject Customer4;
    private GameObject[] skins = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {

        setSkin();

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
    /*
    public void reset_pos()
    {
        transform.SetPositionAndRotation(new Vector3(7.51399994f, 3.67000008f, -44.4700012f), transform.rotation);
        setSkin();

    }
    */
    
}
