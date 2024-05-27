public static class ArrayExtensions
{
    // choose
    public static T Choose<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}