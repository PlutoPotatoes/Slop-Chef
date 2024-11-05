using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{

    [SerializeField] GameObject gruel;
    public bool plateFull;
    private HashSet<string> plateContents = new HashSet<string>();


    // Start is called before the first frame update
    void Start()
    {
        gruel.SetActive(false);
        plateFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void getGruel()
    {
        gruel.SetActive(true);
        plateFull = true;
        plateContents.Add("gruel");
    }

    public void get_topping(string topping)
    {
        if (plateContents.Contains("gruel"))
        {
            switch (topping)
            {
                case "eyeballs":
                    if (!plateContents.Contains("eyeballs"))
                    {
                        gruel.transform.Find("Eyeballs").gameObject.SetActive(true);
                        plateContents.Add("eyeballs");
                        get_contents();
                    }
                    break;
                case "syrup":
                    if (!plateContents.Contains("syrup"))
                    {
                        gruel.transform.Find("Syrup").gameObject.SetActive(true);
                        plateContents.Add("syrup");
                        get_contents();
                    }
                    break;
            }
        }
    }

    public HashSet<string> get_contents()
    {
        return plateContents;
    }




}
