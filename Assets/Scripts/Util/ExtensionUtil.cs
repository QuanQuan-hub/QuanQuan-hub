using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionUtil
{
    // 有则返回，没有则创建新组件
    public static T AddComponentIfNotExist<T>(this GameObject go) where T : Component
    {
        T c = go.GetComponent<T>();
        if (c == null)
            c = go.AddComponent<T>();
        return c;
    }
    public static T AddOrReplaceComponent<T>(this GameObject go) where T : Component
    {
        T c = go.GetComponent<T>();
        if (c != null)
            GameObject.DestroyImmediate(c);
        else
            c = go.AddComponent<T>();
        return c;
    }
}
