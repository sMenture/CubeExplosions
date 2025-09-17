using UnityEngine;

public class ColorGenerator
{
    private const int RGBMaxValue = 255;

    public Color GenerateRandomColor()
    {
        float r = (float)Random.Range(0, RGBMaxValue + 1) / RGBMaxValue;
        float g = (float)Random.Range(0, RGBMaxValue + 1) / RGBMaxValue;
        float b = (float)Random.Range(0, RGBMaxValue + 1) / RGBMaxValue;

        return new Color(r, g, b);
    }

    public Color GenerateRandomColorWithAlpha(float alpha = 1f)
    {
        Color color = GenerateRandomColor();
        color.a = alpha;
        return color;
    }
}