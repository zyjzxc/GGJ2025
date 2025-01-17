using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoop : MonoBehaviour
{
    private float scrollSpeed; // 竖直滚动的速度
    private RectTransform rectTransform; // 存储背景图像的 RectTransform 组件
    private float imageHeight; // 存储背景图像的高度

    void Start()
    {
        // 获取背景图像的 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
        // 获取背景图像的高度
        imageHeight = rectTransform.rect.height;
    }

    void Update()
    {
        scrollSpeed = MainManager._instance.BgSpeed;
        // 计算竖直方向上的新位置
        float newYPosition = rectTransform.anchoredPosition.y - scrollSpeed * Time.deltaTime;
        // 更新 RectTransform 的位置
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newYPosition);

        // 当背景图像滚动到自身高度以下时，将其重新定位到顶部，实现循环滚动
        if (rectTransform.anchoredPosition.y < -imageHeight)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + imageHeight * 2);
        }
    }
}
