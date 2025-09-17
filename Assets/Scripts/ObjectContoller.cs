using System.Collections.Generic;
using UnityEngine;

public class ObjectContoller : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private InputMouseReader _inputMouseReader;

    [SerializeField] private GameObject _prefab;

    private List<ExplodingObject> _explodingObject = new List<ExplodingObject>();
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

    private void TouchObject(GameObject gameObject)
    {
        const float ReductionCoefficient = 2f;

        if (gameObject == null) return;

        if (FindByGameobject(gameObject, out ExplodingObject explodingObject))
        {
            if (explodingObject.SurvivalCheck())
            {
                explodingObject.CalculateNewChanceSurvival();

                for (int i = 0; i < _explosionCountSpawn; i++)
                {
                    float newSizeCoefficient = explodingObject.SizeCoefficient / ReductionCoefficient; 

                    CreateNewExplodingObject(explodingObject.GameObject.transform.position, newSizeCoefficient, explodingObject.ChanceSurvival);
                }

                _exploder.Explosion(gameObject.transform.position);
            }

            DestroyExplodingObject(explodingObject);
        }
    }

    private void CreateNewExplodingObject(Vector3 spawnPosition, float sizeCoefficient = 1, float chanceSurvival = -1)
    {
        const int MaximumChance = 100;

        ExplodingObject explodingObject = new ExplodingObject();
        GameObject selectedGameobject = Instantiate(_prefab, transform);
        chanceSurvival = chanceSurvival > 0 ? chanceSurvival : MaximumChance;

        selectedGameobject.transform.position = spawnPosition;

        explodingObject.Initialize(selectedGameobject, MaximumChance, chanceSurvival, sizeCoefficient);
        _explodingObject.Add(explodingObject);
    }

    private void DestroyExplodingObject(ExplodingObject explodingObject)
    {
        _explodingObject.Remove(explodingObject);

        Destroy(explodingObject.GameObject);
    }

    private bool FindByGameobject(GameObject gameObject, out ExplodingObject explodingObject)
    {

        foreach (var item in _explodingObject)
        {
            if (item.GameObject == gameObject)
            {
                explodingObject = item;
                return true;
            }
        }

        explodingObject = null;
        return false;
    }
}
