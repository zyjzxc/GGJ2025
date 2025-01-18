using UnityEngine;


public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5.0f; // �����ٶȣ����Ը�����Ҫ����
    public Renderer backgroundRenderer1; // ��һ������ͼ����Ⱦ��
    public Renderer backgroundRenderer2; // �ڶ�������ͼ����Ⱦ��
    public float backgroundSize; // ����ͼ�ĸ߶�


    void Start()
    {
        // ��ȡ����ͼ�ĸ߶�
        backgroundSize = backgroundRenderer1.bounds.size.y;


        // ȷ����������ͼ��ʼλ������
        backgroundRenderer2.transform.position = new Vector3(backgroundRenderer1.transform.position.x, backgroundRenderer1.transform.position.y + backgroundSize, backgroundRenderer1.transform.position.z);
    }


    void Update()
    {
        // ���㱳��ͼ��ƫ����
        scrollSpeed = GameManager._instance.upSpeed;
        float offset = -backgroundRenderer1.transform.position.y + Time.deltaTime * scrollSpeed * 0.8f;


        // ���㱳��ͼ����λ��
        float newPosition1 = offset % (2 * backgroundSize);
        float newPosition2 = (newPosition1 - backgroundSize);


        // ���±���ͼ��λ��
        backgroundRenderer1.transform.position = new Vector3(backgroundRenderer1.transform.position.x, -newPosition1, backgroundRenderer1.transform.position.z);
        backgroundRenderer2.transform.position = new Vector3(backgroundRenderer2.transform.position.x, -newPosition2, backgroundRenderer2.transform.position.z);


        // �߽���������߼�
        if (backgroundRenderer1.transform.position.y <= -backgroundSize)
        {
            backgroundRenderer1.transform.position += new Vector3(0, backgroundSize, 0);
        }
        if (backgroundRenderer2.transform.position.y <= -backgroundSize)
        {
            backgroundRenderer2.transform.position += new Vector3(0, backgroundSize, 0);
        }
    }
}