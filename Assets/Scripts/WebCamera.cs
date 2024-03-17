using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamera : MonoBehaviour
{
    // Webカメラ
    //private static int INPUT_SIZE = 256;
    private static int WIDTH = 960;
    private static int HEIGHT = 540;
    private static int FPS = 30;

    // UI
    RawImage rawImage;
    WebCamTexture webCamTexture;
    private Button captureButton;

    // Start is called before the first frame update
    void Start()
    {
        // Webカメラの開始
        //WebCamDevice[] devices = webCamTexture.devices;
        rawImage = GetComponent<RawImage>();
        webCamTexture = new WebCamTexture(WIDTH, HEIGHT, FPS);
        rawImage.texture = webCamTexture;
        webCamTexture.Play();

        // シャッターボタンにクリックイベントを追加
        if (captureButton != null)
        {
            captureButton.onClick.AddListener(TakeShot);
        }
    }

    public void TakeShot()
    {
        // WebカメラをキャプチャしてTexture2Dに変換
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        // Texture2Dからバイト配列に変換
        byte[] bytes = photo.EncodeToPNG();

        // 保存先のファイルパス
        string filePath = Application.persistentDataPath + "/capturedbyUnity.png";

        // バイト配列をファイルに書き込む
        System.IO.File.WriteAllBytes(filePath, bytes);
        Debug.Log("写真を保存しました：" + filePath);
    }
}
