1. `UniWebView 4.2.0` 是一个用于 Unity 的插件，可以在 Unity 项目中嵌入原生 Web 视图，适用于 Android 和 iOS 平台。以下是该版本的一些主要特性和使用方面的介绍：

   ### UniWebView 4.2.0 特性

   1. **跨平台支持**：支持在 Android 和 iOS 上显示 Web 内容，确保了跨平台一致性。
   2. **JavaScript 与 Unity 通信**：提供双向的 JavaScript-Unity 通信功能，便于 Web 页面与 Unity 应用之间的数据交换。
   3. **内置浏览功能**：提供内置的浏览功能，包括导航控制（后退、前进、刷新）、加载进度、页面错误处理等。
   4. **自定义用户界面**：可以自定义 Web 视图的外观和位置，方便与 Unity 界面相结合。
   5. **安全性和隐私设置**：支持 HTTPS、Cookie 管理以及其他隐私相关的功能。

   ### 基本使用步骤

   #### 1. 导入 UniWebView

   将 UniWebView 4.2.0 导入 Unity 项目。导入后，可以在 Unity Editor 中找到插件相关的组件和设置。

   #### 2. 创建 Web 视图

   要在场景中创建 Web 视图，可通过以下代码实现：

   ```
   csharp复制代码using UnityEngine;
   using UniWebView;
   
   public class WebViewController : MonoBehaviour
   {
       private UniWebView webView;
   
       void Start()
       {
           webView = gameObject.AddComponent<UniWebView>();
           webView.Frame = new UniWebViewEdgeInsets(50, 50, 50, 50); // 设置 Web 视图边距
           webView.OnPageFinished += OnPageFinished; // 加载完成事件
           webView.Load("https://www.example.com"); // 加载指定 URL
           webView.Show(); // 显示 Web 视图
       }
   
       private void OnPageFinished(UniWebView webView, int statusCode, string url)
       {
           if (statusCode == 200)
           {
               Debug.Log("Page Loaded Successfully.");
           }
           else
           {
               Debug.LogError("Failed to load the page. Status code: " + statusCode);
           }
       }
   
       void OnDestroy()
       {
           webView.OnPageFinished -= OnPageFinished;
       }
   }
   ```

   Unity 使用UGUI设置Web显示区域

   ```
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
   
   ```

   

   #### 3. 与 JavaScript 交互

   可通过以下代码向 Web 页面发送消息：

   ```
   csharp
   
   
   复制代码
   webView.EvaluateJavaScript("document.body.style.backgroundColor = 'red';");
   ```

   而要从 Web 页面向 Unity 发送消息，可以在 JavaScript 中使用：

   ```
   javascript
   
   
   复制代码
   UniWebViewBridge.send("UnityMethod", "参数1", "参数2");
   ```

   然后在 Unity 端创建一个方法接收消息：

   ```
   csharp复制代码void UnityMethod(string param1, string param2)
   {
       Debug.Log("Received message from JS: " + param1 + ", " + param2);
   }
   ```

   ### 常见问题和注意事项

   - **WebView 显示问题**：确保 WebView 在正确的层级位置，以免被其他 UI 覆盖。
   - **Android 权限**：在 Android 上，可能需要请求 Internet 和网络状态权限。
   - **iOS 安全设置**：在 iOS 上，需要在 `Info.plist` 文件中配置 `NSAppTransportSecurity` 以允许 HTTP 请求。

   通过 `UniWebView 4.2.0`，可以在 Unity 中灵活地加载和显示 Web 内容，适合需要嵌入网页的游戏或应用。如果需要进一步的定制或集成，插件的文档提供了详细的配置选项和 API。
