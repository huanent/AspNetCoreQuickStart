using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class InjectSingletonAttribute : Attribute
{
    public InjectSingletonAttribute(Type type = null)
    {
        Type = type;
    }

    public Type Type { get; private set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class InjectScopedAttribute : Attribute
{
    public InjectScopedAttribute(Type type = null)
    {
        Type = type;
    }

    public Type Type { get; private set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class InjectTransientAttribute : Attribute
{
    public InjectTransientAttribute(Type type = null)
    {
        Type = type;
    }

    public Type Type { get; private set; }
}
