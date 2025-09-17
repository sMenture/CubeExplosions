using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionPower = 10;
    [SerializeField] private int _explosionRange = 5;

    public void Explosion(Vector3 position, Vector3 scale)
    {
        float scaleMagnitude = scale.magnitude;

        float calculatedRange = _explosionRange / scaleMagnitude;
        float calculatedPower = _explosionPower / scaleMagnitude;

        Collider[] nearestObjects = Physics.OverlapSphere(position, calculatedRange);

        foreach (var obj in nearestObjects)
        {
            if (obj.attachedRigidbody == null)
                continue;

            float calculatedDistance = Vector3.Distance(obj.transform.position, position);
            Vector3 direction = obj.transform.position - position;
            direction /= calculatedDistance;

            obj.attachedRigidbody.AddForce(direction.normalized * calculatedPower * calculatedDistance);
        }
    }
}