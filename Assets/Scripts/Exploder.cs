using UnityEngine;

public class Exploder : MonoBehaviour
{
    private const int MaximumChance = 100;

    [SerializeField, Min(0)] private float _chanceSurvival = 100;
    [SerializeField] private int _explosionPower = 10;
    [SerializeField] private int _explosionRange = 5;
    [SerializeField] private int _explosionCountSpawn = 5;

    private void OnValidate()
    {
        if (_chanceSurvival > MaximumChance)
        {
            _chanceSurvival = MaximumChance;
        }
    }

    private void Start()
    {
        GenerateRandomColor();
    }

    private void OnMouseDown()
    {
        if (SurvivalCheck())
        {
            Explosion();
        }

        Destroy(gameObject);
    }

    public bool SurvivalCheck()
    {
        return _chanceSurvival >= RandomValue(0, MaximumChance);
    }

    private void Explosion()
    {
        const float ReductionCoefficient = 2f;

        _chanceSurvival /= ReductionCoefficient;

        CreateNewObject(_explosionCountSpawn);
        InteractionOnOthers();
    }

    private void InteractionOnOthers()
    {
        Collider[] nearestObjects = Physics.OverlapSphere(transform.position, _explosionRange);

        foreach (var obj in nearestObjects)
        {
            if (obj.attachedRigidbody == null)
                continue;

            Vector3 direction = obj.transform.position - transform.position;
            float fixedPowerByScale = _explosionPower / transform.localScale.magnitude;

            obj.attachedRigidbody.AddForce(direction.normalized * fixedPowerByScale);
        }
    }

    private void CreateNewObject(int count)
    {
        const float ReductionSizeRatio = 1.2f;

        for (int i = 0; i < count; i++)
        {
            GameObject newGameobject = Instantiate(gameObject);

            newGameobject.transform.localScale /= ReductionSizeRatio;
        }
    }

    private void GenerateRandomColor()
    {
        const int RGBMaxValue = 255;
        
        MeshRenderer meshRender = GetComponent<MeshRenderer>();

        float r = (float)RandomValue(0, RGBMaxValue) / RGBMaxValue;
        float g = (float)RandomValue(0, RGBMaxValue) / RGBMaxValue;
        float b = (float)RandomValue(0, RGBMaxValue) / RGBMaxValue;

        meshRender.material.color = new Color(r, g, b);
    }

    private int RandomValue(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
}
