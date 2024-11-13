using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class order_screen : MonoBehaviour
{
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;

    public HashSet<HashSet<string>> order = new HashSet<HashSet<string>>();



    // Start is called before the first frame update
    void Start()
    {
        setScreen(null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setScreen(HashSet<HashSet<string>> order)
    {
        //order still aren't setting right, figure it oot
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
            plate.TryGetComponent(out plate plateScript);
            plateScript.getGruel();
            if (contents.Contains("eyeballs"))
            {
                plateScript.get_topping("eyeballs");
            }

            if (contents.Contains("syrup"))
            {
                plateScript.get_topping("syrup");
            }

            if (contents.Contains("worms"))
            {
                plateScript.get_topping("worms");
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
