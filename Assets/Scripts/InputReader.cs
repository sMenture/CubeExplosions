using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const int CurrentMouseIndex = 0;
    private Raycaster raycaster = new Raycaster();

    public event Action<Cube> TouchingObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(CurrentMouseIndex))
        {
            if (raycaster.ScreenPointToRaycastHit(Input.mousePosition, out RaycastHit hit) && hit.collider.TryGetComponent<Cube>(out var getCube))
            {
                TouchingObject?.Invoke(getCube);
            }
        }
    }
}