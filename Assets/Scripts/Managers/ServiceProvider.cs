using System;
using System.Collections.Generic;

public static class ServiceProvider
{
    private static readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

    public static void AddService<T>(T service) where T : IService
    {
        _services.Add(typeof(T), service);
    }

    public static T GetService<T>() where T : IService
    {
        return (T) _services[typeof(T)];
    }
}