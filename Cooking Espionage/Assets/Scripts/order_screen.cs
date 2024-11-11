using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class order_screen : MonoBehaviour
{
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;


    // Start is called before the first frame update
    void Start()
    {
        setScreen(null);
        HashSet<HashSet<string>> order = new HashSet<HashSet<string>>();
        HashSet<string> order1 = new HashSet<string>();
        order1.Add("gruel");
        order1.Add("eyeballs");
        order1.Add("syrup");
        HashSet<string> order2 = new HashSet<string>();
        order2.Add("slop_strawberry");
        order.Add(order2);
        order.Add(order1);
        setScreen(order);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScreen(HashSet<HashSet<string>> order)
    {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);

        if (order == null)
        {
            return;
        }
        

        int i = 1;
        foreach(HashSet<string> orderItem in order)
        {
            switch (i)
            {
                case 1:
                    setItem(item1, orderItem);
                    item1.SetActive(true);
                    break;
                case 2:
                    setItem(item2, orderItem);
                    item2.SetActive(true);
                    break;
                case 3:
                    setItem(item3, orderItem);
                    item3.SetActive(true);
                    break;
            }
            i++;

        }
    }

    private void setItem(GameObject item, HashSet<string> contents)
    {
        GameObject bowl = item.transform.GetChild(0).gameObject;
        GameObject plate = item.transform.GetChild(1).gameObject;

        if (contents.Contains("gruel"))
        {
            bowl.SetActive(false);
            plate.SetActive(true);
            Transform gruel = plate.transform.GetChild(0);
            //gruel isnt active even though it should be from the line below
            gruel.gameObject.SetActive(true);

            if (contents.Contains("eyeballs"))
            {
                gruel.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gruel.GetChild(0).gameObject.SetActive(false);
            }

            if (contents.Contains("syrup"))
            {
                gruel.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                gruel.GetChild(2).gameObject.SetActive(false);
            }

            if (contents.Contains("worms"))
            {
                gruel.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                gruel.GetChild(3).gameObject.SetActive(false);
            }
        }
        else
        {
            bowl.SetActive(true);
            plate.SetActive(false);
            bowl.TryGetComponent(out bowl bowlScript);
            if (contents.Contains("slop_regular"))
            {
                bowlScript.setSlop("slop_regular");
            }else if (contents.Contains("slop_bug"))
            {
                bowlScript.setSlop("slop_bug");
            }else if (contents.Contains("slop_strawberry"))
            {
                bowlScript.setSlop("slop_strawberry");
            }

        }
    }
}
