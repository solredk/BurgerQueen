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
    private float patienceCounter = 0f;
    private int patience = 1000;
    
    private bool hasBeenServed = false;
    public bool hasOrdered = false;

    public CustomerMood currentMood;

    private void Start()
    {
        StartCoroutine(WaitToOrder());
    }

    private void UpdateMood()
    {
        if (patience >= 700)
            currentMood = CustomerMood.Happy;

        else if (patience < 700 && patience >= 300)
            currentMood = CustomerMood.Neutral;

        else if (patience < 300)
            currentMood = CustomerMood.Angry;
    }

    private void Decreasepatience(int amount)
    {
        patience -= amount;
        UpdateMood();
    }

    public void Ordercheck(int quality)
    {
        hasBeenServed = true;

        if (quality == 100)
            patience += 100;

        else if (quality > 50 && quality < 100)
            patience += 50;

        else if (quality < 50)
            patience = 0;
    }

    private IEnumerator WaitToOrder()
    {
        while (hasOrdered == false)
        {
            patienceCounter += Time.deltaTime;
            if (patienceCounter >= 10)
            {
                Decreasepatience(10);
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
            if (patienceCounter >= 10)
            {
                Decreasepatience(10);
                patienceCounter = 0f;
            }
            yield return null;
        }
        yield break;
    }

}
