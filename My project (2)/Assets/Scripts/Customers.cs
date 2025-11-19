using TMPro;
using UnityEngine;

public class Customers : MonoBehaviour
{
    private enum CustomerMood
    {
        Happy,
        Neutral,
        Angry
    }

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI moodText;

    private CustomerMood currentMood;

    private float counter = 0;

    private bool hasOrder;

    // Update is called once per frame
    void Update()
    {
        if (!hasOrder)
        {
            counter += Time.deltaTime;
            timeText.text = "Wait Time: " + counter.ToString("F2") + "s";
            if (counter >= 5f)
            {
                currentMood = CustomerMood.Neutral;
            }


            if (counter >= 10f)
            {
                currentMood = CustomerMood.Angry;

            }
            moodText.text = "Mood: " + currentMood.ToString();
        }
    }
}
