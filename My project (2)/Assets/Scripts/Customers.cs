using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customers : MonoBehaviour
{
    private enum CustomerMood
    {
        Happy,
        Neutral,
        Angry
    }

    [SerializeField] private TextMeshProUGUI patienceText;

    [SerializeField] private Image moodImage;

    [SerializeField] private Sprite[] moodSprites;

    [SerializeField] private int scoreMultiplier;
    
    private CustomerMood currentMood;

    private float counter = 0;

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
                ScoreManager.Instance.AddScore(10 * scoreMultiplier);
                break;
            case CustomerMood.Neutral:
                ScoreManager.Instance.AddScore(5 * scoreMultiplier);
                break;
            case CustomerMood.Angry:
                ScoreManager.Instance.AddScore(0 * scoreMultiplier);
                break;
        }

        ResetOrder();
    }

    private void ResetOrder()
    {
        currentMood = CustomerMood.Happy;
        counter = 0;
        hasOrder = false;
    }
}

