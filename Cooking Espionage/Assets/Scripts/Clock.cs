using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] GameObject textObject;
    private TextMeshPro textMesh;
    private float realTime;
    public int displayedTime;
    private bool playing;
    public bool atZero = false;
    public float cigModifier = 1;

    void Start()
    {
        displayedTime = 99;
        textObject.TryGetComponent(out TextMeshPro text);
        textMesh = text;
        textMesh.text = displayedTime.ToString();
        playing = false;
        
    }

    public void stop()
    {
        playing = false;
        atZero = false;
    }
    public void start()
    {
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            updateTimer();
        }
    }

    public void setTimer(int timerVal)
    {
        playing = true;
        realTime = timerVal;
        displayedTime = timerVal;
        textMesh.color = Color.white;
    }

    private void updateTimer()
    {
        if (realTime > 0)
        {
            atZero = false;
            realTime -= (Time.deltaTime*cigModifier);
            displayedTime = ((int)realTime);
            textMesh.text = displayedTime.ToString();
            switch (displayedTime)
            {
                case 10:
                    textMesh.color = Color.green;
                    break;
                case 5:
                    textMesh.color = Color.yellow;
                    break;
                case 3: 
                    textMesh.color = Color.red;
                    break;
                case 0:
                    textMesh.color = new Vector4(1f, 0f, 0f, 0.5f);
                    break;


            }

        }
        else
        {
            atZero = true;
            textMesh.color = new Vector4(1f, 0f, 0f, 0.2f);
        }

    }

}
