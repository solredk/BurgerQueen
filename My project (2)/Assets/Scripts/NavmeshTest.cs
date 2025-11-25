using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavmeshTest : MonoBehaviour
{
    [SerializeField] private  NavMeshAgent agent;
    [SerializeField] private Vector2 inputs;

    void Update()
    {
            Ray ray = Camera.main.ScreenPointToRay(inputs);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);

            }
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        inputs = context.ReadValue<Vector2>();
    }
}
