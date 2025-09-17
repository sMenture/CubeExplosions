using System.Collections.Generic;
using UnityEngine;

public class ExplodingObjectSpawner : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private InputReader _inputMouseReader;

    private List<Cube> _explodingObject = new List<Cube>();
    private int _explosionCountSpawn = 5;

    private void OnEnable()
    {
        _inputMouseReader.TouchingObject += TouchObject;
    }

    private void OnDisable()
    {
        _inputMouseReader.TouchingObject -= TouchObject;
    }

    private void Start()
    {
        CreateNewExplodingObject(Vector3.one);
    }

    private void TouchObject(Cube cube)
    {
        const float ReductionCoefficient = 2f;

        if (cube == null) return;

        if (cube.SurvivalCheck())
        {
            cube.CalculateNewChanceSurvival();

            for (int i = 0; i < _explosionCountSpawn; i++)
            {
                float newSizeCoefficient = cube.SizeCoefficient / ReductionCoefficient;

                _spawner.SpawnObject(cube.transform.position, newSizeCoefficient, cube.ChanceSurvival);
                CreateNewExplodingObject(cube.transform.position, newSizeCoefficient, cube.ChanceSurvival);
            }

            _exploder.Explosion(gameObject.transform.position);
        }

        DestroyExplodingObject(cube);
    }

    private void CreateNewExplodingObject(Vector3 spawnPosition, float sizeCoefficient = 1, float chanceSurvival = -1)
    {
        _explodingObject.Add(_spawner.SpawnObject(spawnPosition, sizeCoefficient, chanceSurvival));
    }

    private void DestroyExplodingObject(Cube explodingObject)
    {
        _explodingObject.Remove(explodingObject);

        Destroy(explodingObject.gameObject);
    }
}
