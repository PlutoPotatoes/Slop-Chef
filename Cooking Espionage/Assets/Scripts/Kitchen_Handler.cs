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

    private string[] order_bases = new string[4] { "slop_regular", "slop_strawberry", "slop_bug", "gruel" };
    private string[] toppings = new string[3] {"eyeballs", "worms", "syrup" };

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
        int num_items = Random.Range(1,round_number);
        HashSet<HashSet<string>> order = new HashSet<HashSet<string>>();
        while (num_items > 0)
        {
            HashSet<string> item = new HashSet<string>();
            string main = order_bases[Random.Range(0, 3)];
            item.Add(main);
            if (main.Equals("gruel"))
            {
                int topping_num = Random.Range(1, 3);
                ArrayList toppings_left = new ArrayList(toppings);
                while(topping_num > 0)
                {
                    string topping = toppings_left[Random.Range(0, topping_num-1)] as string;
                    item.Add(topping);
                    toppings_left.Remove(topping);
                    topping_num--;
                }

            }
            order.Add(item);
        }
        return order;
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
