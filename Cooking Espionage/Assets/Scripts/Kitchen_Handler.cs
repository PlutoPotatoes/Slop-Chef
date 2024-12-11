using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Kitchen_Handler : MonoBehaviour
{
    [SerializeField] GameObject clock;
    [SerializeField] GameObject order_screen_object;
    [SerializeField] GameObject expo;
    [SerializeField] GameObject player;
    [SerializeField] GameObject Dialogue;
    [SerializeField] GameObject debt_display;
    [SerializeField] GameObject door;
    [SerializeField] GameObject shop_dialogue;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject expoDoor;
    [SerializeField] GameObject Customer;
    [SerializeField] Camera cutSceneCamera;
    [SerializeField] GameObject cutsceneTimeline;
    [SerializeField] Dialogue credits;
    [SerializeField] GameObject city;
    [SerializeField] GameObject gameOverText;
    [SerializeField] RenderTexture mainCameraTexture;
    [SerializeField] RenderTexture cutsceneTexture;
    [SerializeField] Canvas playerCanvas;
    [SerializeField] Canvas cutsceneCanvas;


    order_screen orderScreenScript;
    Clock clockScript;
    Expo expoScript;
    Player playerScript;
    Dialogue dialogueScript;
    debt_counter debtScript;
    Animator doorAnimator;
    Shop_Dialogue shopDialogueScript;
    Shop shopScript;
    Animator expoDoorAnimator;
    Animator customerAnimator;
    customer_handler customerScript;
    PlayableDirector cutsceneDirector;
    

    private int current_day = 1;
    public int total_debt = 1;
    private float day_timer = 600;
    private int curr_order_cost;
    public bool onBreak;
    private float day_length = 60;
    private int break_length = 30;
    private bool markedForDeath = false;
    public bool introPlaying;
    private bool cutsceneStarted = false;
    private bool cutsceneFinished = false;
    private bool isDead = false;

    private HashSet<HashSet<string>> current_order = new HashSet<HashSet<string>>();
    private string[] order_bases = new string[4] { "slop_regular", "slop_strawberry", "slop_bug", "gruel" };
    private string[] toppings = new string[3] { "eyeballs", "worms", "syrup" };

    //item vars
    private bool hasBeer = false;
    private bool hasPan = false;
    private bool hasCig = false;
    private bool hasCookies = false;
    private GameObject currCustomer;

    // Start is called before the first frame update
    void Start()
    {
        orderScreenScript = order_screen_object.GetComponent<order_screen>();
        clockScript = clock.GetComponent<Clock>();
        expoScript = expo.GetComponent<Expo>();
        playerScript = player.GetComponent<Player>();
        dialogueScript = Dialogue.GetComponent<Dialogue>();
        debtScript = debt_display.GetComponent<debt_counter>();
        doorAnimator = door.GetComponent<Animator>();
        shopDialogueScript = shop_dialogue.GetComponent<Shop_Dialogue>();
        shopScript = shop.GetComponent<Shop>();
        dialogueScript.initiateDialogues();
        shopDialogueScript.initiateDialogues();
        expoDoorAnimator = expoDoor.GetComponent<Animator>();
        cutsceneDirector = cutsceneTimeline.GetComponent<PlayableDirector>();
        total_debt = 200;
        door.transform.position = new Vector3(-1.88116446e-05f, -0.0222699996f, -0.0044300002f);
        expoDoor.transform.position = new Vector3(-3.7888844f, 1.29642832f, 0.126666918f);
        city.SetActive(false);
        gameOverText.SetActive(false);
        resetItems();
        StartCoroutine(firstDayStart());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    IEnumerator firstDayStart()
    {
        Cursor.visible = false;
        introPlaying = true;
        yield return new WaitForSeconds(5);
        start_day();
    }

    // Update is called once per frame
    void Update()
    {
        clockCheck();
        update_timer();
        restartCheck();
        
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
            int base_select = Random.Range(0, 4 + round_number);
            string main = order_bases[Mathf.Min(base_select, 3)];
            
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
            if (!repeat && !current_order.Contains(item))
            {
                num_items--;
                order.Add(item);
            }
            
        }


        curr_order_cost = hasBeer ? (int)((float)curr_order_cost * 1.5f) : curr_order_cost;
        return order;
    }

    private void killPlayer()
    {
        playerScript.kill_player();
    }

    private void new_customer()
    {
        currCustomer = Instantiate(Customer);
        currCustomer.SetActive(true);
        customerScript = currCustomer.GetComponent<customer_handler>();
        customerAnimator = currCustomer.GetComponent<Animator>();
        customerScript.setSkin();
        customerAnimator.SetTrigger("newCustomer");
        customerAnimator.ResetTrigger("walkAway");
        
    }
    private void start_day()
    {
        
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
            default:
                dialogueScript.playDialogue("start_day_regular");
                break;
        }

        yield return new WaitUntil(() => dialogueScript.textFinished);
        introPlaying = false;

        doorAnimator.SetBool("isOpen", false);
        expoDoorAnimator.SetBool("isOpen", true);

        
        day_timer = day_length;
        next_order();
        debtScript.setDebt(total_debt);
        onBreak = false;

    }

    public void skip_order()
    {
        next_order();
    }

    public void order_finished() // called when the player hits the bell
    {
        if (!markedForDeath && !introPlaying)
        {
            if (!onBreak)
            {
                if (expoScript.isOrderComplete())
                {
                    total_debt -= curr_order_cost;
                    debtScript.subtractDebt(curr_order_cost);
                    expoScript.housekeeping();
                    if (total_debt <= 0)
                    {
                        debt_settled();
                        return;
                    }

                    if (day_timer > 0)
                    {
                        next_order();
                    }
                    else
                    {
                        start_break();

                    }
                }
                else
                {
                    if (hasCookies)
                    {
                        int customerLuck = Random.Range(1,11);
                        print(customerLuck);
                        if(customerLuck>7)
                        {
                            //play cookie sparkle particles
                            print("got lucky this time");
                            total_debt -= curr_order_cost;
                            debtScript.subtractDebt(curr_order_cost);
                            expoScript.housekeeping();
                            if (total_debt <= 0)
                            {
                                debt_settled();
                                return;
                            }

                            if (day_timer > 0)
                            {
                                next_order();
                            }
                            else
                            {
                                start_break();

                            }

                        }
                        else
                        {
                            StartCoroutine(failure_to_perform("wrong_order"));
                            clockScript.stop();
                        }
                    }
                    else
                    {
                        StartCoroutine(failure_to_perform("wrong_order"));
                        clockScript.stop();
                    }

                    
                }
            }
            else
            {
                start_day();
            }
        }
    }

    public void pan_hit()
    {
        if (markedForDeath)
        {
            markedForDeath = false;
            dialogueScript.clear();
            StartCoroutine(complaint_terminated());

        }
        else
        {
            next_order();
        }
    }
    IEnumerator complaint_terminated()
    {
        dialogueScript.playDialogue("complaint_terminated");
        yield return new WaitUntil(() => dialogueScript.textFinished);
        next_order();


    }
    IEnumerator failure_to_perform(string reason)
    {
        markedForDeath = true;
        dialogueScript.playDialogue(reason);
        orderScreenScript.setScreen(null);
        yield return new WaitUntil(() => dialogueScript.textFinished);
        if (markedForDeath)
        {
            expoDoorAnimator.SetBool("isOpen", false);
            gameOverText.SetActive(true);
            killPlayer();
            isDead = true;
        }

    }

    private void next_order()
    {
        if (customerAnimator)
        {
            customerAnimator.SetTrigger("walkAway");
            customerAnimator.ResetTrigger("newCustomer");
        }
        new_customer();
        set_order(generateOrder(current_day));
        expoScript.set_order(current_order);
        orderScreenScript.setScreen(current_order);
        clockScript.setTimer(10 + (5*(current_order.Count-1)));
    }

    private void clockCheck()
    {
        if (!onBreak)
        {
            if (clockScript.atZero)
            {
                StartCoroutine(failure_to_perform("times_up"));
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
        customerAnimator.SetTrigger("walkAway");
        customerAnimator.ResetTrigger("newCustomer");
        orderScreenScript.setScreen(null);
        onBreak = true;
        clockScript.stop();
        current_day++;
        StartCoroutine(break_routine());


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
            default:
                dialogueScript.playDialogue("start_break_regular");
                break;
        }
        resetItems();
        shopScript.reset_store();
        expoDoorAnimator.SetBool("isOpen", false);
        yield return new WaitUntil(() => dialogueScript.textFinished);
        playerScript.canBuyItem = true;
        doorAnimator.SetBool("isOpen", true);
        clockScript.setTimer(break_length);

    }

    private void debt_settled()
    {
        clockScript.stop();
        dialogueScript.playDialogue("debt_settled");
        shop.SetActive(false);
        doorAnimator.SetBool("isOpen", true);
        expoDoorAnimator.SetBool("isOpen", false);
        city.SetActive(true);
    }

    public void interactWithShop(string item)
    {
        playerScript.canMove = false;
        clockScript.stop();
        StartCoroutine(shop_interact(item));
    }
    
    IEnumerator shop_interact(string item)
    {
        shopDialogueScript.playDialogue(item);
        yield return new WaitUntil(() => shopDialogueScript.interactionFinished);
        playerScript.canMove = true;
        clockScript.start();

    }

    public void buy_item(string item)
    {
        shopScript.buyItem(item);
        playerScript.canBuyItem = false;
        doorAnimator.SetBool("isOpen", false);
        switch (item)
        {
            case "cigs":
                hasCig = true;
                clockScript.cigModifier = 0.7f;
                break;
            case "beer":
                hasBeer = true;
                break;
            case "pan":
                hasPan = true;
                expoScript.show_pan();
                break;
            case "cookies":
                hasCookies = true;
                expoScript.show_cookies();
                break;
        }

    }

    public void dontGetGreedy()
    {
        dialogueScript.playDialogue("greedy_worker");
    }

    public void resetItems()
    {
        hasCig = false;
        clockScript.cigModifier = 1;
        hasBeer = false;
        hasCookies = false;
        expoScript.hide_cookies();
        hasPan = true;
        expoScript.show_pan();
    }

    public void play_final_cutscene()
    {
        StopAllCoroutines();
        playerScript.canMove = false;
        player.gameObject.SetActive(false);
        city.SetActive(true);
        cutsceneTimeline.SetActive(true);
        cutSceneCamera.gameObject.SetActive(true);
        cutsceneDirector.Play();
        cutsceneDirector.stopped += OnPlayableDirectorStopped;

        return;
    }

    private void restartCheck()
    {
        if (cutsceneFinished || isDead)
        {
            if (Input.GetKeyDown("r"))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SceneManager.LoadSceneAsync("MainGameplayScene");
            }
            if (Input.GetKeyDown("space"))
            {
                /*Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                releaseTextures();
                */
                Application.Quit();
            }
        }
    }
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        credits.playEndCredits();
        cutsceneFinished = true;
        cutsceneDirector.stopped -= OnPlayableDirectorStopped;
        

    }

    void releaseTextures()
    {
        
        mainCameraTexture.Release();
        cutsceneTexture.Release();
        playerCanvas.gameObject.SetActive(false);
        cutsceneCanvas.gameObject.SetActive(false);
        Destroy(player);
        Destroy(cutsceneTimeline);
    }
}
