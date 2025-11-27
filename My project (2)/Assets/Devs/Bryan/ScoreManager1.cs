using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager1 : MonoBehaviour
{
    public bool takingToLong = false;
    private int customerSatisfaction = 10;
    public Slider satisfactionSlider;
    public int score;
    public Customer currentCustomerScript;

    private void Start()
    {
        satisfactionSlider = FindFirstObjectByType<Slider>();
        


    }


    private void Update()
    {

        satisfactionSlider.value = customerSatisfaction;

        if (takingToLong)
        {
            StartCoroutine(TookToLong());
        }

        if (currentCustomerScript.currentMood == CustomerMood.Angry)
        {
            takingToLong = true;
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
