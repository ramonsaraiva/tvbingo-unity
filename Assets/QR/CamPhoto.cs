using UnityEngine;
using System.Collections;
using System.IO;

public class CamPhoto : MonoBehaviour
{
	private WebCamTexture webCamRender;

	private Color32[] cameraTexture;

	public int W, H;


	public string fotoFilename;

	Texture2D photo;



	void Start()
	{
		SetupCamRender();
		enabled = false;
	}

	private void SetupCamRender()
	{
		webCamRender = WebCamHelper.GetDevice(false, 1080, 720);				
	}

	public void StartCameraRender()
	{
		if(webCamRender != null)
		{
			enabled = true;
			webCamRender.Play();
			CheckResolution();
		}
	}

	void GetCameraTexture()
	{					
		if(webCamRender.videoVerticallyMirrored)
			cameraTexture = WebCamHelper.MirrorTexture(webCamRender.GetPixels32(), W, H, false, false);
		else
			cameraTexture = webCamRender.GetPixels32();	
	}

	public void StopCameraRender()
	{
		OnDestroy();
		enabled = false;
	}

	private void CheckResolution()
	{
		W = webCamRender.width;
		H = webCamRender.height;
		if(W < 20)
		{
			W = webCamRender.requestedWidth;
			H = webCamRender.requestedHeight;
		}
	}

	void OnDisable()
	{
		if(webCamRender != null)
		{
			webCamRender.Pause();
		}
	}

	void OnDestroy()
	{
		if(webCamRender != null)
			webCamRender.Stop();
	}

	public Texture2D OnTakePhoto()
	{
		photo = new Texture2D(webCamRender.width, webCamRender.height);
		photo.SetPixels32(webCamRender.GetPixels32());
		photo.Apply();

		//SaveImg();

		return photo;
	}

	private void SaveImg()
	{
		string filePath = Application.persistentDataPath + "/" + fotoFilename + ".jpg";
		Debug.Log(filePath);

		//Encode to a PNG
		byte[] bytes = photo.EncodeToJPG();
		//Write out the PNG. Of course you have to substitute your_path for something sensible
		File.WriteAllBytes(filePath, bytes);
	}

	public WebCamTexture WebCamRender
	{
		get{ return webCamRender; }
	}
}
