using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] GameObject textObject;
    private TextMeshPro textMesh;
    private float realTime;
    public int displayedTime;
    private bool playing;

    void Start()
    {

        displayedTime = 0;
        textObject.TryGetComponent(out TextMeshPro text);
        textMesh = text;
        updateTimer();
        setTimer(15);
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
            realTime -= Time.deltaTime;
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
    }

}
