using System;
using System.Collections.Generic;

public static class TypeUtilities
{
    static Dictionary<Type, Type[]> _bases = new();
    static Dictionary<Type, Type[]> _interfaces = new();

    public static Type[] GetBasesForType(Type type)
    {
        if (!_bases.TryGetValue(type, out var val))
        {
            _bases[type] = val = GenerateBasesForType(type);
        }
        return val;
    }
        
    static Type[] GenerateBasesForType(Type t)
    {
        List<Type> types = new List<Type>();
        while (t != null && t != typeof(object) && t != typeof(ValueType))//Iterate over all types, add them to the Hashset.
        {
            types.Add(t);
            t = t.BaseType;
        }
        return types.ToArray();
    }

    public static Type[] GetInterfacesForType(Type type)
    {
        if (!_interfaces.TryGetValue(type, out var val))
        {
            _interfaces[type] = val = GenerateInterfacesForType(type);
        }
        return val;
    }

    static Type[] GenerateInterfacesForType(Type t)
    {
        return t.GetInterfaces();
    }
}