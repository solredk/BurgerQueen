using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customers : MonoBehaviour
{
    private enum CustomerMood
    {
        Happy,
        Neutral,
        Angry
    }

    [SerializeField] private TextMeshProUGUI patienceText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Image moodImage;

    [SerializeField] private Sprite[] moodSprites;

    private CustomerMood currentMood;

    private float counter = 0;

    private int score;

    private bool hasOrder;


    void Update()
    {
        if (!hasOrder)
        {
            counter += Time.deltaTime;

            patienceText.text = "Wait Time: " + counter.ToString("F2") + "s";

            if (counter >= 5f)
                currentMood = CustomerMood.Neutral;

            if (counter >= 10f)
                currentMood = CustomerMood.Angry;

            moodImage.sprite = moodSprites[((int)currentMood)];
        }
    }

    public void ReceivedOrder()
    {
        hasOrder = true;

        switch (currentMood)
        {
            case CustomerMood.Happy:
                score += 10;
                break;
            case CustomerMood.Neutral:
                score += 5;
                break;
            case CustomerMood.Angry:
                score -= 5;
                break;
        }

        scoreText.text = "Score: " + score.ToString();

        ResetOrder();
    }


    private void ResetOrder()
    {
        currentMood = CustomerMood.Happy;
        counter = 0;
        hasOrder = false;
    }
}

