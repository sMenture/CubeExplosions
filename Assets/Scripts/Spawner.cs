using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    
    public Cube SpawnObject(Vector3 position, float sizeCoefficient = 1f, float chanceSurvival = 100f)
    {
        const int MaximumChance = 100;

        GameObject selectedGameobject = Instantiate(_prefab, transform);
        selectedGameobject.transform.position = position;

        Cube explodingObject = selectedGameobject.AddComponent<Cube>();
        chanceSurvival = chanceSurvival > 0 ? chanceSurvival : MaximumChance;

        explodingObject.Initialize(MaximumChance, chanceSurvival, sizeCoefficient);
        return explodingObject;
    }
}