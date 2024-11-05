using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl: MonoBehaviour
{

    public bool bowlFull;
    private GameObject slop;
    private HashSet<string> bowlContents = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        bowlFull = false;
        slop = transform.GetChild(0).gameObject;
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

}
