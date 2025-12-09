using UnityEngine;

public class StationSwitchButton : MonoBehaviour
{
    private StationSwitchPlayer player;
    private void Start()
    {
        player = FindFirstObjectByType<StationSwitchPlayer>().GetComponent<StationSwitchPlayer>();
    }
    public void MoveHere(Transform point)
    {
        if (point == null) return; else
        {
            player.Move(point);
        }
    }
}
