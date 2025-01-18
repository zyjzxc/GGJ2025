using UnityEngine;


public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5.0f; // 滚动速度，可以根据需要调整
    public Renderer backgroundRenderer1; // 第一个背景图的渲染器
    public Renderer backgroundRenderer2; // 第二个背景图的渲染器
    public float backgroundSize; // 背景图的高度


    void Start()
    {
        // 获取背景图的高度
        backgroundSize = backgroundRenderer1.bounds.size.y;


        // 确保两个背景图初始位置相邻
        backgroundRenderer2.transform.position = new Vector3(backgroundRenderer1.transform.position.x, backgroundRenderer1.transform.position.y + backgroundSize, backgroundRenderer1.transform.position.z);
    }


    void Update()
    {
        // 计算背景图的偏移量
        scrollSpeed = GameManager._instance.upSpeed;
        float offset = -backgroundRenderer1.transform.position.y + Time.deltaTime * scrollSpeed * 0.8f;


        // 计算背景图的新位置
        float newPosition1 = offset % (2 * backgroundSize);
        float newPosition2 = (newPosition1 - backgroundSize);


        // 更新背景图的位置
        backgroundRenderer1.transform.position = new Vector3(backgroundRenderer1.transform.position.x, -newPosition1, backgroundRenderer1.transform.position.z);
        backgroundRenderer2.transform.position = new Vector3(backgroundRenderer2.transform.position.x, -newPosition2, backgroundRenderer2.transform.position.z);


        // 边界检查和重置逻辑
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