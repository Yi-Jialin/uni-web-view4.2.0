using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button openbtn, closebtn;
    public InputField inputField;
    
    public UniWebView webView;
    public RectTransform targetRectTransform;  // Ŀ�� UGUI Ԫ��
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
        // ������ҳ
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

    //����Web��ʾ����
    private void SetWebViewFrame()
    {
        // �� RectTransform ����Ļ����ת��Ϊ WebView ��λ�úʹ�С
        Vector3[] corners = new Vector3[4];
        targetRectTransform.GetWorldCorners(corners);

        // ��ȡ��Ļ����
        float x = corners[0].x;
        float y = Screen.height - corners[1].y;  // ע�� Unity �� WebView ������ϵ��ͬ
        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;

        // ���� WebView �� Frame
        webView.Frame = new Rect(x, y, width, height);
    }
}
