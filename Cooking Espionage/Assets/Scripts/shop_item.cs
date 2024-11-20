using UnityEngine;

public class shop_item : MonoBehaviour
{
    public string item_type;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Item_Selected()
    {
        gameObject.SetActive(false);
    }

    public void buy()
    {
        gameObject.SetActive(false);
    }
}
