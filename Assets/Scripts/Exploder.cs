using UnityEditor;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionPower = 10;
    [SerializeField] private int _explosionRange = 5;

    public void Explosion(Vector3 positon)
    {
        Collider[] nearestObjects = Physics.OverlapSphere(positon, _explosionRange);

        foreach (var obj in nearestObjects)
        {
            if (obj.attachedRigidbody == null)
                continue;

            Vector3 direction = obj.transform.position - positon;
            float fixedPowerByScale = _explosionPower / transform.localScale.magnitude;

            obj.attachedRigidbody.AddForce(direction.normalized * fixedPowerByScale);
        }
    }

}
