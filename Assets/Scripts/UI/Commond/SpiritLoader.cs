using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 精灵加载器
/// </summary>
[RequireComponent(typeof(Image))]
public class SpiritLoader : MonoBehaviour
{
    private Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    private string spiritPath;

    public string SpiritPath
    {
        get => spiritPath;
        set
        {
            spiritPath = value;

            if (string.IsNullOrEmpty(spiritPath))
            {
                Debuger.LogError("精灵加载失败，路径为空");
                return;
            }
            
            LoadSpiritAsset(spiritPath);
        }
    }

    private void LoadSpiritAsset(string path)
    {
        Sprite sprite = GameEnter.Resources.LoadAsset<Sprite>(Constant.ResourcesPath.GetSpirit(path));

        if (sprite == null)
        {
            Debuger.LogError("加载精灵失败！");
        }

        image.sprite = sprite;
    }
}
