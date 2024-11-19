using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private Dictionary<string, string[]> dialogues = new Dictionary<string, string[]>();
    [SerializeField] TextMeshProUGUI text;
    private string[] lines;
    public float speed = 0.001f;
    private int index;
    public bool textFinished;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void initiateDialogues()
    {
        string[] start_script = {"Rise and shine citizen..... due to your <color=red>UNPAID BALANCE<color=white> of 200 units you have been reassigned to <color=red>KITCHEN DUTY<color=white> until your debt with <color=#ADD8E6>CompanyIncorperated™<color=white> is settled",
            "Orders will appear on the large screen in front of you from <color=red>HUNGRY CUSTOMERS.<color=white> Once you prepare them and deliver them to the window ring the bell to get your next customer",
            "Remember... these are hard working employees that <color=red>PAY THEIR DEBTS....<color=white>","Do Not keep them waiting. Do Not give them the wrong order. Do Not try to run...<br>Failure to perform will lead to <color=red>TERMINATION<color=white>",
            "Ready....? Too bad. <color=red>WERE OPEN.<color=white>" };
        dialogues.Add("start_day1", start_script);

        string[] break_script1 = { "Congratulation, you made it through your first shift. Please Enjoy this <color=yellow>complementary 60 second break<color=white>",
            "Should you feel the need to get back to work, simply hit the bell to call a customer"};
        dialogues.Add("break1", break_script1);

        string[] start_day2 = { "<color=red>BREAK IS OVER.", "<color=white>Our employees are <color=red>HUNGRY... BACK TO WORK<color=white>" };
        dialogues.Add("start_day2", start_day2);
        string[] break_script2 = { "Second shift complete.", "Beginning your <color=yellow>complementary 60 break<color=white>" };
        dialogues.Add("break2", break_script2);

        string[] wrong_order = { "<color=red>UH OH CITIZEN <color=white> <br><br>We have an unhappy customer who recieved the wrong order.", "Prepare for <color=red>TERMINATION<color=white>" };
        string[] times_up = { "<color=red>UH OH CITIZEN <color=white> <br><br>It appears you couldn't meet <color=#ADD8E6>CompanyIncorperated<color=white> <color=red>PRODUCTIVITY STANDARDS<color=white>.", "Prepare for <color=red>TERMINATION<color=white>" };
        dialogues.Add("wrong_order", wrong_order);
        dialogues.Add("times_up", times_up);

        string[] so_close = { "<color=red> OH DEAR<color=white> and you were getting so close too", "prepare for <color=red>TERMINATION<color=white>" };
        dialogues.Add("fail_so_close", so_close);

        string[] start_day_regular = { "<color=red>BREAK IS OVER.....<br>BACK TO WORK<color=white>" };
        string[] start_break_regular = { "Shift Complete, starting <color=yellow>break<color=white>" };
        dialogues.Add("start_break_regular", start_break_regular);
        dialogues.Add("start_day_regular", start_day_regular);

        string[] debt_settled = { "<color=yellow>Congratulations Citizen<color=white> <br>You have officially settled your debt with <color=#ADD8E6>CompanyIncorperated<color=white>", "You are free to go... <br><color=red>DO NOT LET THIS HAPPEN AGAIN<color=white>" };
        dialogues.Add("debt_settled", debt_settled);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(text.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void playDialogue(string dialogueName)
    {
        gameObject.SetActive(true);
        text.text = "";
        textFinished = false;
        index = 0;
        lines = dialogues[dialogueName];
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            //make sound for each letter typed, different colors = different nosies
            text.text += c;
            yield return new WaitForSeconds(speed);

        }
    }

    void nextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            textFinished = true;
        }
    }
}
