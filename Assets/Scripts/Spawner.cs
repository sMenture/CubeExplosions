using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    public Cube SpawnObject(Vector3 position, float sizeCoefficient = 1f, float chanceSurvival = 100f)
    {
        GameObject newObject = Instantiate(_prefab, position, Quaternion.identity, transform);
        Cube cube = newObject.GetComponent<Cube>();

        if (cube == null)
        {
            cube = newObject.AddComponent<Cube>();
        }

        cube.Initialize(100, chanceSurvival, sizeCoefficient);
        return cube;
    }
}