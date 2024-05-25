﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace BetterWutheringWaves.Model;

/// <summary>
/// 由于 C# 的 DI 过于难用，bgi代码中依旧存在使用大量原始单例的对象
/// 给他们实现一个通用的单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class
{
    protected static T? _instance;
    protected static object? syncRoot;

    public static T Instance => LazyInitializer.EnsureInitialized(ref _instance, ref syncRoot, CreateInstance);

    protected static T CreateInstance()
    {
        return (T)Activator.CreateInstance(typeof(T), true)!;
    }

    public static void DestroyInstance()
    {
        _instance = null;
    }
}
