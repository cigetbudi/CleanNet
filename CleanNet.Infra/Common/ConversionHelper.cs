namespace CleanNet.Infra.Common
{
    public static class ConversionHelper
{
    public static string IntToString(int value) => value.ToString();

    public static string FloatToString(float value, string format = null)
    {
        if (string.IsNullOrEmpty(format))
            return value.ToString();
        return value.ToString(format);
    }

    public static bool TryStringToInt(string value, out int result)
    {
        return int.TryParse(value, out result);
    }

    public static int StringToInt(string value, int defaultValue = 0)
    {
        return int.TryParse(value, out var result) ? result : defaultValue;
    }

    public static bool TryStringToFloat(string value, out float result)
    {
        return float.TryParse(value, out result);
    }

    public static float StringToFloat(string value, float defaultValue = 0)
    {
        return float.TryParse(value, out var result) ? result : defaultValue;
    }
}
}