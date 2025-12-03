using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager1 : MonoBehaviour
{
    public bool takingToLong = false;
    private bool ignoreCustomer = false;
    private bool nextCustomer;
    private int customerSatisfaction = 10;
    public Slider satisfactionSlider;
    public int score;
    public Customer currentCustomerScript;
    [SerializeField]
    private float scoreTimer;

    private void Start()
    {
        satisfactionSlider = FindFirstObjectByType<Slider>();

    }

   


    private void Update()
    {
        scoreTimer += Time.deltaTime;
        currentCustomerScript = FindFirstObjectByType<Customer>();

        satisfactionSlider.value = customerSatisfaction;

        if (takingToLong)
        {
            StartCoroutine(TookToLong());
        }

        //if (currentCustomerScript.currentMood == CustomerMood.Angry && !ignoreCustomer)
        //{
        //    GoingDownSatisfaction();
        //    ignoreCustomer = true;
        //}
       

        //if (currentCustomerScript.hasBeenServed && !ignoreCustomer) {
        //    ignoreCustomer = true;
        //    switch (currentCustomerScript.currentMood) { 
        //        case CustomerMood.Happy:
        //            customerSatisfaction += 2;
        //            break;
        //            case CustomerMood.Neutral:
        //            customerSatisfaction += 1;
        //            break;
            
        //    }
        
        //}


    }

    public void GoingDownSatisfaction()
    {
        takingToLong = true;
    }

    public void timeBonus()
    {
        if(scoreTimer < 60)
        {
            score += 100;
        }else if (scoreTimer < 300)
        {
            score += 50;
        }
        else
        {
            score += 0;
        }
    }

   
    IEnumerator TookToLong()
    {
        customerSatisfaction -= 2;
        yield return new WaitForEndOfFrame();
        takingToLong = false;
        yield break;
    }
}
