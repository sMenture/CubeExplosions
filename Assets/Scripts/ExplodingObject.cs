using UnityEngine;

public class ExplodingObject
{
    private int _maximumChance = 100;

    public float SizeCoefficient { set; get; }
    public float ChanceSurvival { set; get; }
    public GameObject GameObject { set; get; }

    public void Initialize(GameObject gameObject, int maximumChance, float chanceSurvival, float sizeCoefficient)
    {
        _maximumChance = maximumChance;
        SizeCoefficient = sizeCoefficient;
        ChanceSurvival = chanceSurvival;
        GameObject = gameObject;

        GameObject.transform.localScale = Vector3.one * SizeCoefficient;

        GenerateRandomColor();
    }

    public void CalculateNewChanceSurvival()
    {
        const float ReductionCoefficient = 2f;

        ChanceSurvival /= ReductionCoefficient;
    }

    public bool SurvivalCheck()
    {
        return ChanceSurvival >= RandomValue(0, _maximumChance);
    }

    private int RandomValue(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    private void GenerateRandomColor()
    {
        const int RGBMaxValue = 255;

        MeshRenderer meshRender = GameObject.GetComponent<MeshRenderer>();

        float r = (float)RandomValue(0, RGBMaxValue) / RGBMaxValue;
        float g = (float)RandomValue(0, RGBMaxValue) / RGBMaxValue;
        float b = (float)RandomValue(0, RGBMaxValue) / RGBMaxValue;

        meshRender.material.color = new Color(r, g, b);
    }
}