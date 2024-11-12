using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button openbtn, closebtn;
    public InputField inputField;
    
    public UniWebView webView;
    public RectTransform targetRectTransform;  // 目标 UGUI 元素
    string url= "https://www.baidu.com/";
    void Start()
    {

        SetWebViewFrame();

        webView.CleanCache();

        inputField.onValueChanged.AddListener(InputUrl);

        openbtn.onClick.AddListener(Open);
    }

    public void Open() 
    {
        // 加载网页
        webView.Load(url);
        webView.Show();
    }


    public void Close() 
    {
        webView.Hide();
    }

    private void InputUrl(string value)
    { 
     url = value;
    }

    //设置Web显示区域
    private void SetWebViewFrame()
    {
        // 将 RectTransform 的屏幕坐标转换为 WebView 的位置和大小
        Vector3[] corners = new Vector3[4];
        targetRectTransform.GetWorldCorners(corners);

        // 获取屏幕坐标
        float x = corners[0].x;
        float y = Screen.height - corners[1].y;  // 注意 Unity 和 WebView 的坐标系不同
        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;

        // 设置 WebView 的 Frame
        webView.Frame = new Rect(x, y, width, height);
    }
}
