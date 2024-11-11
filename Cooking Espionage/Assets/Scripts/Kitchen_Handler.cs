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

    private int orders_completed = 0;
    private int current_day = 1;

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

    private HashSet<HashSet<string>> generateOrder(int round_number)
    {

        return null;
    }

    private void killPlayer()
    {

    }

    private void order_finished() // called when the player hits the bell
    {
        if (expoScript.isOrderComplete())
        {
            orders_completed++;
            set_order(generateOrder(current_day));
            clockScript.setTimer(7 * current_day);
        }
        else
        {
            killPlayer();
        }
    }

}
