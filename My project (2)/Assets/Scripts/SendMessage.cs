using UnityEngine;
using TMPro;

public class SendMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    
    private void Awake()
    {
        if (messageText == null)
        {
            messageText = GetComponent<TextMeshProUGUI>();
        }
    }
    
    private void Start()
    {
        UpdateMessage("Drag the Old Oil can into the Old oil");
    }
    
    public void UpdateMessage(string newMessage)
    {
        if (messageText != null)
        {
            messageText.text = newMessage;
        }
    }

}