using UnityEngine;

public class Cube : MonoBehaviour
{
    private ColorGenerator _colorGenerator = new ColorGenerator();

    private int _maximumChance = 100;

    public float SizeCoefficient { private set; get; }
    public float ChanceSurvival {  private set; get; }

    public void Initialize(int maximumChance, float chanceSurvival, float sizeCoefficient)
    {
        _maximumChance = maximumChance;
        SizeCoefficient = sizeCoefficient;
        ChanceSurvival = chanceSurvival;

        transform.localScale = Vector3.one * SizeCoefficient;

        GetComponent<MeshRenderer>().material.color = _colorGenerator.GenerateRandomColor();
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
}