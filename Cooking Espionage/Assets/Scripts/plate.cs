using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{

    [SerializeField] GameObject gruel;
    [SerializeField] AudioClip wormSound;
    [SerializeField] AudioClip syrupSound;
    [SerializeField] AudioClip eyeSound;

    public bool plateFull;
    private HashSet<string> plateContents = new HashSet<string>();



    // Start is called before the first frame update
    void Start()
    {
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
                        SFXManager.instance.playSFX(eyeSound, transform, 1f);
                    }
                    break;
                case "syrup":
                    if (!plateContents.Contains("syrup"))
                    {
                        gruel.transform.Find("Syrup").gameObject.SetActive(true);
                        plateContents.Add("syrup");
                        SFXManager.instance.playSFX(syrupSound, transform, 1f);

                    }
                    break;
                case "worms":
                    if (!plateContents.Contains("worms"))
                    {
                        gruel.transform.Find("Worms").gameObject.SetActive(true);
                        plateContents.Add("worms");
                        SFXManager.instance.playSFX(wormSound, transform, 1f);

                    }
                    break;
            }
        }
    }

    public void reset_plate()
    {
        plateContents.Clear();
        gruel.transform.Find("Eyeballs").gameObject.SetActive(false);
        gruel.transform.Find("Syrup").gameObject.SetActive(false);
        gruel.transform.Find("Worms").gameObject.SetActive(false);


    }

    public HashSet<string> get_contents()
    {
        return plateContents;
    }




}
