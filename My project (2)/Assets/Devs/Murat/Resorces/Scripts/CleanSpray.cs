using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class CleanSpray : MonoBehaviour
{
    [SerializeField] private int spray = 0;
    Grabing Grabing;
    [SerializeField] private ParticleSystem SprayEffect;
    [SerializeField] private GameObject Wc;
    [SerializeField] private GameObject SprayTrigger;
    [SerializeField] private bool inPosition = false;
    
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
            if (spray == 0)
            {
                transform.LookAt(Wc.transform.position);
            }else if (spray == 1)
            {
                if (transform.position.y> Wc.transform.position.y+0.3f&& !inPosition)
                {
                    Debug.Log(transform.rotation.z);
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + -1 * Time.deltaTime, transform.localRotation.w),360);
                    if (transform.localRotation.z > 100)
                    {
                        Debug.Log("trou");
                        inPosition = true;
                    }
                }
                else if(transform.position.y < Wc.transform.position.y && inPosition)
                {
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + 1 * Time.deltaTime, transform.rotation.w), 360);
                    if(transform.rotation.z < 5&& transform.localRotation.z > -5)
                    {
                        inPosition = false;
                    }
                }
            }
            
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        
    }
    IEnumerator ReversPosition()
    {
        inPosition = false;
        while (transform.localRotation.z >= 0)
        {
            transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.1f);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator spraying()
    {
        SprayTrigger.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SprayTrigger.SetActive(false);
    }
}
