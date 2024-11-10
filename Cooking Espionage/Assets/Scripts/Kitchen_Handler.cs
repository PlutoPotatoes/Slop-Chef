using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen_Handler : MonoBehaviour
{
    [SerializeField] GameObject clock;
    [SerializeField] GameObject order_screen_object;
    [SerializeField] GameObject expo;
    [SerializeField] GameObject player;
    order_screen orderScreenScript;
    Clock clockScript;
    Expo expoScript;
    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        orderScreenScript = order_screen_object.GetComponent<order_screen>();
        clockScript = clock.GetComponent<Clock>();
        expoScript = expo.GetComponent<Expo>();
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void set_order(HashSet<HashSet<string>> order) 
    {
        
    }

    private void generateOrder()
    {

    }

    private void killPlayer()
    {

    }

}
