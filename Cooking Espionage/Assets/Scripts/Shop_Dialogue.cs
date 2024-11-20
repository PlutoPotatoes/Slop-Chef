using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop_Dialogue : MonoBehaviour
{
    private Dictionary<string, string[]> dialogues = new Dictionary<string, string[]>();
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Kitchen_Handler kitchen_handler;
    private string[] lines;
    public float speed = 0.005f;
    private int index;
    public bool textFinished;
    public bool interactionFinished;
    public string currentItem;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    public void initiateDialogues()
    {
        string[] beer = {"A little something to <color=yellow>take the edge off<color=white>? Company studies have shown customers are likely to tip relaxed and friendly workers.<br><br>(<color=yellow>Money per sale +50% today<color=white>)"};
        string[] cigs = { "Need a little pick me up? Feel the rush and <color=yellow>fly through orders<color=white>.<br><br>(<color=yellow>Time moves 30% slower today<color=white>)" };
        string[] cookies = { "Can't keep up? Customers <color=yellow>might not notice their missing gruel<color=white> when they have dessert.<br><br> (<color=yellow>30% chance to get away with incorrect orders<color=white>)" };
        string[] pan = { "Massive order right before close? 'Convince' the customer to <color=yellow>fuck off<color=white> with a little projectile negotiation. (<color=yellow>Throw this at a customer to skip their order<color=white>)" };
        dialogues.Add("beer", beer);
        dialogues.Add("cigs", cigs);
        dialogues.Add("cookies", cookies);
        dialogues.Add("pan", pan);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            text.text = lines[index];
            textFinished = true;
            
        }
        if (textFinished && Input.GetKeyDown("e"))
        {
            //confirm buy
            
            kitchen_handler.buy_item(currentItem);
            interactionFinished = true;
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("q"))
        {
            interactionFinished = true;
            gameObject.SetActive(false);
            currentItem = null;
        }
    }

    public void playDialogue(string dialogueName)
    {
        currentItem = dialogueName;
        gameObject.SetActive(true);
        text.text = "";
        textFinished = false;
        interactionFinished = false;
        index = 0;
        lines = dialogues[dialogueName];
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            //make sound for each letter typed, different colors = different nosies
            text.text += c;
            yield return new WaitForSeconds(speed);

        }
        textFinished = true;
    }

}
