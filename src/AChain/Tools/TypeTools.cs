namespace AChain.Tools;

public static class TypeTools
{
    public static void ExecuteForEachType(Action<Type> action)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                action.Invoke(type);
            }
        }
    }

    public static Type[] GetGenericTypesByBaseType(Type type)
    {
        return type.BaseType?.GenericTypeArguments
               ?? throw new InvalidOperationException($"Incorrectly configured type: {type.Name}");
    }
}
