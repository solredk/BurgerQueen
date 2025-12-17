using System.Collections;
using TMPro;
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
    public OrderManager orderPoints;
    private float scoreTimer;
    public bool stageEnd = false;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        //satisfactionSlider = FindFirstObjectByType<Slider>();
        orderPoints = FindFirstObjectByType<OrderManager>();

    }

   


    private void Update()
    {
        scoreTimer += Time.deltaTime;
        //currentCustomerScript = FindFirstObjectByType<Customer>();

        //satisfactionSlider.value = customerSatisfaction;

        //if (takingToLong)
        //{
        //    StartCoroutine(TookToLong());
        //}

        scoreText.text = "Score:" + score;


        if (orderPoints.orderGivePoints)
        {
            StartCoroutine(pointsGoDown());
        }else if (orderPoints.orderGiveReverse)
        {
            StartCoroutine(pointsGoUp());
        }
      

        if (stageEnd)
        {
            timeBonus();
        }


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

        stageEnd = false;
    }

    IEnumerator pointsGoUp()
    {
        score += 10;
        yield return new WaitForEndOfFrame();
        orderPoints.orderGiveReverse = false;
    }

    IEnumerator pointsGoDown()
    {
        score -= 10;
        yield return  new WaitForEndOfFrame();
        orderPoints.orderGivePoints = false;
    }



    IEnumerator TookToLong()
    {
        customerSatisfaction -= 2;
        yield return new WaitForEndOfFrame();
        takingToLong = false;
        yield break;
    }
}
