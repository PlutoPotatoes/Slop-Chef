using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen_Handler : MonoBehaviour
{
    [SerializeField] GameObject clock;
    [SerializeField] GameObject order_screen_object;
    [SerializeField] GameObject expo;
    [SerializeField] GameObject player;
    [SerializeField] GameObject Dialogue;
    order_screen orderScreenScript;
    Clock clockScript;
    Expo expoScript;
    Player playerScript;
    Dialogue dialogueScript;

    private int current_day = 1;
    public int total_debt = 2000;
    private float day_timer = 600;
    private int curr_order_cost;
    public bool onBreak;
    private float day_length = 5;
    private int break_length = 30;

    private HashSet<HashSet<string>> current_order = new HashSet<HashSet<string>>();
    private string[] order_bases = new string[4] { "slop_regular", "slop_strawberry", "slop_bug", "gruel" };
    private string[] toppings = new string[3] { "eyeballs", "worms", "syrup" };

    // Start is called before the first frame update
    void Start()
    {
        orderScreenScript = order_screen_object.GetComponent<order_screen>();
        clockScript = clock.GetComponent<Clock>();
        expoScript = expo.GetComponent<Expo>();
        playerScript = player.GetComponent<Player>();
        dialogueScript = Dialogue.GetComponent<Dialogue>();

        start_day();
    }

    // Update is called once per frame
    void Update()
    {
            clockCheck();
            update_timer();
        
    }

    private void set_order(HashSet<HashSet<string>> order) 
    {
        current_order = order;
    }

    private void update_timer()
    {
        if(day_timer > 0f)
        {
            day_timer -= Time.deltaTime;
        }

    }
    private HashSet<HashSet<string>> generateOrder(int round_number)
    {
        int num_items = Random.Range(1,round_number+1);
        curr_order_cost = 0;
        HashSet<HashSet<string>> order = new HashSet<HashSet<string>>();
        while (num_items > 0)
        {
            HashSet<string> item = new HashSet<string>();
            string main = order_bases[Random.Range(0, 4)];
            
            item.Add(main);
            curr_order_cost += 2;
            if (main.Equals("gruel"))
            {
                curr_order_cost += 3;
                int topping_num = Random.Range(1, 3);
                ArrayList toppings_left = new ArrayList(toppings);
                while(topping_num > 0)
                {
                    string topping = toppings_left[Random.Range(0, toppings_left.Count)] as string;
                    item.Add(topping);
                    curr_order_cost += 1;
                    toppings_left.Remove(topping);
                    topping_num--;
                }

            }
            bool repeat = false;
            foreach(HashSet<string> oldItem in order)
            {
                if (item.SetEquals(oldItem))
                {
                    repeat = true;
                    break;
                }
            }
            if (!repeat)
            {
                num_items--;
                order.Add(item);
            }
            
        }
        return order;
    }

    private void killPlayer()
    {
        playerScript.kill_player();
    }

    private void start_day()
    {
        print("rise and shine");
        print(current_day);
        StartCoroutine(day_start_routine());
        
    }

    IEnumerator day_start_routine()
    {
        switch (current_day)
        {
            case 1:
                dialogueScript.playDialogue("start_day1");
                break;
            case 2:
                dialogueScript.playDialogue("start_day2");
                break;
        }

        yield return new WaitUntil(() => dialogueScript.textFinished);

        day_timer = day_length;
        set_order(generateOrder(current_day));
        expoScript.set_order(current_order);
        orderScreenScript.setScreen(current_order);
        clockScript.setTimer(10 * current_day);
        onBreak = false;

    }

    

    public void order_finished() // called when the player hits the bell
    {
        if (!onBreak)
        {
            if (expoScript.isOrderComplete())
            {
                total_debt -= curr_order_cost;
                expoScript.housekeeping();

                if (day_timer > 0)
                {
                    next_order();
                }
                else
                {
                    start_break();
                    
                }



                print(total_debt);
            }
            else
            {

                StartCoroutine(wrong_order());
                clockScript.stop();
            }
        }
        else
        {
            start_day();
        }
    }

    IEnumerator wrong_order()
    {
        dialogueScript.playDialogue("wrong_order");
        yield return new WaitUntil(() => dialogueScript.textFinished);
        killPlayer();
        
    }

    IEnumerator times_up()
    {
        dialogueScript.playDialogue("times_up");
        yield return new WaitUntil(() => dialogueScript.textFinished);
        killPlayer();

    }

    private void next_order()
    {

        set_order(generateOrder(current_day));
        expoScript.set_order(current_order);
        orderScreenScript.setScreen(current_order);
        clockScript.setTimer(15 * current_day);
    }

    private void clockCheck()
    {
        if (!onBreak)
        {
            if (clockScript.atZero)
            {
                StartCoroutine(times_up());
                clockScript.stop();
            }
        }
        else
        {
            if (clockScript.atZero)
            {
                start_day();
            }
        }
    }

    private void start_break()
    {
        /* play goodnight dialogue
         * fade to black
         * choices?
         * new day
         * 
         */
        orderScreenScript.setScreen(null);
        onBreak = true;
        clockScript.stop();
        current_day++;
        StartCoroutine(break_routine());
        print("break time");


    }

    IEnumerator break_routine()
    {
        switch (current_day)
        {
            case 2:
                dialogueScript.playDialogue("break1");
                break;
            case 3:
                dialogueScript.playDialogue("break2");
                break;
        }

        yield return new WaitUntil(() => dialogueScript.textFinished);
        clockScript.setTimer(break_length);

    }

}
