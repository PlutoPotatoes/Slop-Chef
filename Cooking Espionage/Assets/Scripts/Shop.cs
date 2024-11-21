using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject cigs;
    [SerializeField] GameObject pan;
    [SerializeField] GameObject beer;
    [SerializeField] GameObject cookies;

    // Start is called before the first frame update
    void Start()
    {

        cigs.SetActive(true);
        beer.SetActive(true);
        cookies.SetActive(true);
        pan.SetActive(true);
        gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyItem(string item)
    {
        switch (item)
        {
            case "cigs":
                cigs.SetActive(false);
                break;
            case "pan":
                pan.SetActive(false);
                break;
            case "beer":
                beer.SetActive(false);
                break;
            case "cookies":
                cookies.SetActive(false);
                break;
        }
    }

    public void reset_store()
    {
        cigs.SetActive(true);
        beer.SetActive(true);
        cookies.SetActive(true);
        pan.SetActive(true);
        gameObject.SetActive(true);
    }
}
