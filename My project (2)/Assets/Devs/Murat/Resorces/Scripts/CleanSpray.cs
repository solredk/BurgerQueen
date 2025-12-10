using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class CleanSpray : MonoBehaviour
{
    Grabing Grabing;
    [SerializeField] private ParticleSystem SprayEffect;
    [SerializeField] private GameObject Wc;
    [SerializeField] private GameObject SprayTrigger;
    
    void Start()
    {
        Grabing = FindObjectOfType<Grabing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grabing.Helditem == gameObject)
        {
            if (Keyboard.current.fKey.isPressed)
            {
                SprayEffect.Play();
                StartCoroutine(spraying());
            }
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.LookAt(Wc.transform.position);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        
    }
    IEnumerator spraying()
    {
        SprayTrigger.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SprayTrigger.SetActive(false);
    }
}
