namespace ThesisProject.Domain.Common;
public static class CommonGuard
{
    public static void StringNotNullOrEmpty(string nameOfClass, string nameOfProperty, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"Property {nameOfProperty} is null or empty string in class {nameOfClass}");
        }
    }

    public static void NumberNotZeroOrNegativeNumber(string nameOfClass, string nameOfProperty, decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"Property {nameOfProperty} is 0 or negative number in class {nameOfClass}");
        }
    }
    public static void GuidNotEmpty(string nameOfClass, string nameOfProperty, Guid value)
    {
        var emptyGuid = Guid.Empty;
        if (value == emptyGuid)
        {
            throw new ArgumentException($"Property {nameOfProperty} is empty guid in class {nameOfClass}");
        }
    }

    public static void IEnumerableNotNullOrEmpty<T>(string nameOfClass, string nameOfProperty, IEnumerable<T> value)
    {
        if (value == null)
        {
            throw new ArgumentException($"Property {nameOfProperty} is null IEnumerable in class {nameOfClass}");
        }

        if (value.Count() is 0)
        {
            throw new ArgumentException($"Property {nameOfProperty} is empty IEnumerable in class {nameOfClass}");
        }
    }

    public static void ObjectNotNull<T>(string nameOfClass, string nameOfProperty, T value)
    {
        if (value is null)
        {
            throw new ArgumentException($"Property {nameOfProperty} is null object in class {nameOfClass}");
        }
    }
}
