using TMPro;
using UnityEngine;

public class UIHitPoints : MonoBehaviour
{
    [SerializeField] private TMP_Text hitpointsTextmesh;

    public void UpdateHitpoints(int newHitpoints)
    {
        hitpointsTextmesh.text = newHitpoints.ToString();
    }
}
