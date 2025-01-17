using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoop : MonoBehaviour
{
    private float scrollSpeed; // ��ֱ�������ٶ�
    private RectTransform rectTransform; // �洢����ͼ��� RectTransform ���
    private float imageHeight; // �洢����ͼ��ĸ߶�

    void Start()
    {
        // ��ȡ����ͼ��� RectTransform ���
        rectTransform = GetComponent<RectTransform>();
        // ��ȡ����ͼ��ĸ߶�
        imageHeight = rectTransform.rect.height;
    }

    void Update()
    {
        scrollSpeed = MainManager._instance.BgSpeed;
        // ������ֱ�����ϵ���λ��
        float newYPosition = rectTransform.anchoredPosition.y - scrollSpeed * Time.deltaTime;
        // ���� RectTransform ��λ��
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newYPosition);

        // ������ͼ�����������߶�����ʱ���������¶�λ��������ʵ��ѭ������
        if (rectTransform.anchoredPosition.y < -imageHeight)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + imageHeight * 2);
        }
    }
}
