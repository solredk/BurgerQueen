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
    private float rotateZ = 0;
    
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
                if (transform.position.y> Wc.transform.position.y+0.3f)
                {
                    rotateZ -= Time.deltaTime*160;
                    transform.localRotation = Quaternion.Euler(0, 0, rotateZ);
                    if (rotateZ < -180)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, -180);
                        Debug.Log("trou");
                    }
                }
                else if(transform.position.y < Wc.transform.position.y)
                {
                    rotateZ += Time.deltaTime * 160;
                    transform.localRotation = Quaternion.Euler(0, 0, rotateZ);
                    if (rotateZ < 5&& rotateZ > -5)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
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
    IEnumerator spraying()
    {
        SprayTrigger.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SprayTrigger.SetActive(false);
    }
}
