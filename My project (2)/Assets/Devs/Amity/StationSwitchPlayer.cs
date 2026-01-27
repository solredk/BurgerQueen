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
    [SerializeField] private Transform currentStation;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void Move(Transform point)
    {
        agent.isStopped = false;
        agent.SetDestination(point.transform.position);
        atStation = false;
        moving = false;
        currentStation = point;
    }
    private void FixedUpdate()
    {
        
        if (transform.position.x == agent.destination.x && transform.position.z == agent.destination.z)
        {
            if (currentStation != null)
            {
                agent.isStopped = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, currentStation.rotation, turnSpeed);
                if (currentStation.gameObject.tag == "Respawn")
                {
                    if (currentStation.eulerAngles.y - transform.eulerAngles.y < 1 && currentStation.eulerAngles.y - transform.eulerAngles.y > -1)
                    {
                        agent.enabled = false;
                        transform.position = currentStation.GetComponent<TeleportPoints>().endPoint.transform.position;
                        transform.rotation = currentStation.GetComponent<TeleportPoints>().endPoint.transform.rotation;
                        transform.Rotate(transform.rotation.x, transform.rotation.y - 180, transform.rotation.z);
                        agent.enabled = true;
                        currentStation = null;
                    }
                }
                else
                {
                    atStation = true;
                    moving = true;
                }
            }
        }
        if(moving)
        {
            LeftRightMove();
        }
    }

    public void InStationMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            direction = context.ReadValue<float>()/100 * speed;
            if(atStation)
            {
                moving = true;
            }
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
