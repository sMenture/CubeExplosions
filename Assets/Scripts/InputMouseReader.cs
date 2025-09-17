using System;
using UnityEngine;

public class InputMouseReader : MonoBehaviour
{
    private const int CurrentMouseIndex = 0;

    [SerializeField] private ObjectContoller _objectContoller;

    public event Action<GameObject> TouchingObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(CurrentMouseIndex))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TouchingObject?.Invoke(hit.collider.gameObject);
            }
        }
    }
}