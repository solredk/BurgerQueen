using TMPro;
using UnityEngine;

public class TashBin : MonoBehaviour
{
    public int trashFilled;
    public int trashRequirement;
    public Animator lidAnimator;
    public TextMeshProUGUI trashText;

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
        trashText.text = "Trash:" + trashFilled + "/" + trashRequirement;
    }
}
