using UnityEngine;

public class Raycaster
{
    public bool ScreenPointToRaycastHit(Vector3 positon, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(positon);
        
        return Physics.Raycast(ray, out hit);
    }
}
