using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TashBin : MonoBehaviour
{
    public int trashFilled;
    public int trashRequirement;
    public GameObject[] trashTotal;
    public Animator lidAnimator;
    public TextMeshProUGUI trashText;
    public GameObject trashbag;
    public int numberOffTrash;
    public BoxCrush crushMinigame;
    public bool miniGame = false;
    public bool boxesEnded = false;
    private int reachTrash;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "trashBag")
        {
            trashFilled += 1;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (boxesEnded)
        {
            trashText.text = "Trash:" + trashFilled + "/" + trashRequirement;
            if (trashFilled == trashRequirement)
            {
                lidAnimator.SetBool("Closing", true);
            }
        }
        if (miniGame)
        {
            trashMinigame();


        }
        
    }

    public void trashMinigame() {

        trashRequirement = trashTotal.Length;
        numberOffTrash = Random.Range(5, 7);
        reachTrash = Random.Range(-20, -30);

        for (int i = 0; i < numberOffTrash; i++)
        {
            reachTrash = Random.Range(-25, -28);
            Instantiate(trashbag, new Vector3(reachTrash, -7, 15), Quaternion.identity);

        }
        trashTotal = GameObject.FindGameObjectsWithTag("trashBag");
        trashRequirement = trashTotal.Length;

        miniGame = false;
    }
}
