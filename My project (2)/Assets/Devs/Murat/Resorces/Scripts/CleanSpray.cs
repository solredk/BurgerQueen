using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class CleanSpray : MonoBehaviour
{
    Grabing Grabing;
    [SerializeField] private ParticleSystem SprayEffect;
    [SerializeField] private GameObject Wc;
    
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

        yield return new WaitForSeconds(0.4f);
    }
}
