namespace Shared;

public static class WolverineExtensions
{
    public static string GetNameForAzureSeviceBus<T>()
        => typeof(T).FullName!.ToLower().Replace(".", "-");
}
