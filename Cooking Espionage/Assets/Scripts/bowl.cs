using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl: MonoBehaviour
{
    [SerializeField] Material slop_regular;
    [SerializeField] Material slop_bug;
    [SerializeField] Material slop_strawberry;
    [SerializeField] GameObject slop;

    public bool bowlFull;
    private HashSet<string> bowlContents = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        //bow dispenser not working either
        bowlFull = false;
        slop.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSlop(Material slopType)
    {
        slop.GetComponent<SpriteRenderer>().material = slopType;
        slop.SetActive(true);
        bowlFull = true;
        string slopName = slopType.name.Replace(" (Instance)", "");
        bowlContents.Add(slopName);
    }

    public HashSet<string> get_contents()
    {
        return bowlContents;
    }

    public void setSlop(string slopType)
    {
        switch (slopType)
        {
            case "slop_regular":
                setSlop(slop_regular);
                break;
            case "slop_bug":
                setSlop(slop_bug);
                break;
            case "slop_strawberry":
                setSlop(slop_strawberry);
                break;
        }
    }

}
