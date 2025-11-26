using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class StationSwitchPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool atStation;
    private bool moving;
    private float direction;
    private Transform currentStation;
    [SerializeField] private float speed;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void Move(Transform point)
    {
        agent.isStopped = false;
        agent.SetDestination(point.transform.position);
        atStation = false;
        currentStation = point;
    }
    private void Update()
    {
        if (transform.position.x == agent.destination.x && transform.position.z == agent.destination.z)
        {
            atStation = true;
            if (currentStation != null)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, currentStation.rotation, 0.06f);
                agent.isStopped = true;
            }       
        }
        if(moving)
        {
            LeftRightMove();
        }
    }

    public void InStationMove(InputAction.CallbackContext context)
    {
        if(context.performed && atStation)
        {
            moving = true;
            direction = context.ReadValue<float>()/100 * speed;
        }
        if(context.canceled)
        {
            moving = false;
            direction = 0;
        }
    }

    public void LeftRightMove()
    {
        if (transform.eulerAngles.y > 269 && transform.eulerAngles.y < 271)
        {
            transform.position += new Vector3(0, 0, direction);
            transform.position = new Vector3(transform.position.x,transform.position.y,Mathf.Clamp(transform.position.z, currentStation.position.z - currentStation.localScale.x / 2, currentStation.position.z + currentStation.localScale.x / 2));
        }
        if (transform.eulerAngles.y > 89 && transform.eulerAngles.y < 91)
        {
            transform.position += new Vector3(0, 0, -direction);
            transform.position = new Vector3(transform.position.x,transform.position.y,Mathf.Clamp(transform.position.z, currentStation.position.z - currentStation.localScale.x / 2, currentStation.position.z + currentStation.localScale.x / 2));
        }
        if (transform.eulerAngles.y > -1 && transform.eulerAngles.y < 1)
        {
            transform.position += new Vector3(direction, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, currentStation.position.x - currentStation.localScale.x / 2, currentStation.position.x + currentStation.localScale.x / 2),transform.position.y,transform.position.z);
        }
        if (transform.eulerAngles.y > 179 && transform.eulerAngles.y < 181)
        {
            transform.position += new Vector3(-direction, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, currentStation.position.x - currentStation.localScale.x / 2, currentStation.position.x + currentStation.localScale.x / 2), transform.position.y, transform.position.z);
        }
    }
}
