using System.Collections;
using UnityEngine;
public enum CustomerMood
{
    Happy,
    Neutral,
    Angry
}
public class Customer : MonoBehaviour
{
    private int patience = 1000;
    private float patienceCounter = 0f;

    private CustomerMood currentMood;
    public bool hasOrdered = false;
    public bool hasBeenServed = false;

    private void Start()
    {
        StartCoroutine(WaitToOrder());
    }

    private void UpdateMood()
    {
        if (patience >= 700)
        {
            currentMood = CustomerMood.Happy;
        }
        else if (patience < 700 && patience >= 300)
        {
            currentMood = CustomerMood.Neutral;
        }
        else if (patience < 300)
        {
            currentMood = CustomerMood.Angry;
        }
    }

    private void Decreasepatience(int amount)
    {
        patience -= amount;
        UpdateMood();
    }

    private IEnumerator WaitToOrder()
    {
        while (hasOrdered == false)
        {
            patienceCounter += Time.deltaTime;
            if (patienceCounter >= 30)
            {
                Decreasepatience(100);
                patienceCounter = 0f;
            }
            yield return null;
        }
            StartCoroutine(WaitsForOrder());
            yield break;
    }

    private IEnumerator WaitsForOrder()
    {
        while (hasBeenServed == false) 
        {
            patienceCounter += Time.deltaTime;
            if (patienceCounter >= 30)
            {
                Decreasepatience(100);
                patienceCounter = 0f;
            }
            yield return null;
        }
            yield break;
    }

}
