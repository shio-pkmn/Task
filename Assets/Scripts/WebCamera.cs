using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamera : MonoBehaviour
{
    // Web�J����
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
        // Web�J�����̊J�n
        //WebCamDevice[] devices = webCamTexture.devices;
        rawImage = GetComponent<RawImage>();
        webCamTexture = new WebCamTexture(WIDTH, HEIGHT, FPS);
        rawImage.texture = webCamTexture;
        webCamTexture.Play();

        // �V���b�^�[�{�^���ɃN���b�N�C�x���g��ǉ�
        if (captureButton != null)
        {
            captureButton.onClick.AddListener(TakeShot);
        }
    }

    public void TakeShot()
    {
        // Web�J�������L���v�`������Texture2D�ɕϊ�
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        // Texture2D����o�C�g�z��ɕϊ�
        byte[] bytes = photo.EncodeToPNG();

        // �ۑ���̃t�@�C���p�X
        string filePath = Application.persistentDataPath + "/capturedbyUnity.png";

        // �o�C�g�z����t�@�C���ɏ�������
        System.IO.File.WriteAllBytes(filePath, bytes);
        Debug.Log("�ʐ^��ۑ����܂����F" + filePath);
    }
}
