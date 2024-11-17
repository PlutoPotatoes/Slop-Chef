using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debt_counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debt_display;
    public int debt;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDebt(int newDebt)
    {
        this.gameObject.SetActive(true);
        debt = newDebt;
        debt_display.text = debt.ToString();
    }

    public void subtractDebt(int amount)
    {
        debt -= amount;
        debt_display.text = debt.ToString();
    }
}
