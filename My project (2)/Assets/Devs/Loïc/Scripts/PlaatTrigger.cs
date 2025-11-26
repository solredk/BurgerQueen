using UnityEngine;

public class PlaatTrigger : MonoBehaviour
{
    private BurgerAssambleManager m_AssambleManager;

    private void Start()
    {
        m_AssambleManager = FindAnyObjectByType<BurgerAssambleManager>();
        print(m_AssambleManager);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_AssambleManager.IsTriggered();
    }
}
