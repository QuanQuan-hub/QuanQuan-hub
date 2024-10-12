using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class AssetManager : MonoSigleton<AssetManager>
{
    public override void OnCreate()
    {
        // 初始化资源系统
        YooAssets.Initialize();
        // 创建默认的资源包
        var package = YooAssets.CreatePackage("DefaultPackage");
        YooAssets.SetDefaultPackage(package);
    }


    private void OnDestroy()
    {
        DestroyPackage();
    }
    private void DestroyPackage()
    {
        // 先销毁资源包
        var package = YooAssets.GetPackage("DefaultPackage");
        DestroyOperation operation = package.DestroyAsync();
        // 然后移除资源包
        if (YooAssets.RemovePackage("DefaultPackage"))
        {
            Debug.Log("移除成功！");
        }
    }
}
