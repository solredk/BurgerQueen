using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoxCrush : MonoBehaviour
{
 
   public GameObject currentBox;
   public UnityEngine.UI.Slider boxCrushSlider;
    
   public TashBin theTrash;
   public bool miniGameInprogress = true;
   public bool runningRoutine = true;
   private int boxRequirement;
    private int boxDone;
    private bool onlyOneAdded = false;
    [SerializeField]
    private Animator boxAnimations;
 

    private void Start()
    {
        currentBox = GameObject.FindGameObjectWithTag("Box");
        theTrash = FindFirstObjectByType<TashBin>();
        boxAnimations = currentBox.GetComponent<Animator>();
        boxCrushSlider.value = 1;
        boxRequirement = 3;

        boxRequirement = 3;
    }

    private void FixedUpdate()
    {
        if (boxCrushSlider.value == 0 && onlyOneAdded == false)
        {
            onlyOneAdded = true;
            boxDone += 1;

        }
    }


    private void Update()
    {


        if (boxCrushSlider.value == 0 && runningRoutine)
        {
            StartCoroutine(Slide());
        }

        if(boxCrushSlider.value != 0)
        {
         
            runningRoutine = true;
            boxCrushSlider.interactable = true;
            
        }

        theTrash.trashText.text = "Crushed" + ":" + boxDone + "/" + boxRequirement;
    
        currentBox = GameObject.FindGameObjectWithTag("Box");
        currentBox.transform.localScale = new Vector3(1, boxCrushSlider.value, 1);
    }


   IEnumerator Slide()
    {
        boxAnimations.SetBool("Minigame", true);
        boxCrushSlider.interactable = false;
        yield return new WaitForSeconds(0.7f);
        boxAnimations.SetBool("Minigame", false);
        boxCrushSlider.value = 1;
        runningRoutine = false;
        onlyOneAdded = false;
        if (boxDone == boxRequirement)
        {
            theTrash.boxesEnded = true;
            theTrash.miniGame = true;
            Destroy(currentBox);
            Destroy(gameObject);
        }
        yield break;
    }
}
