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



    

}
