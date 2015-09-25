using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCodeReader : MonoBehaviour
{
	private BarcodeReader barcodeReader;

	private WebCamTexture webCamRender;

	private Color32[] cameraTexture;

	public int W, H;

	float currentTime, timer;

	public int intervalDecode;

	public float aspectRatio;

	public System.Action<string> OnResult;

	void Start()
	{
		SetupReader();
		SetupCamRender();
		enabled = false;
	}

	private void SetupReader()
	{
		List<BarcodeFormat> formats = new List<BarcodeFormat>();
		formats.Add(BarcodeFormat.QR_CODE);
		// create a reader with a custom luminance source
		barcodeReader = new BarcodeReader {
			AutoRotate = true,
			TryHarder = false, 
			PureBarcode = false, 
			PossibleFormats = formats
		};

	}

	private void SetupCamRender()
	{
		webCamRender = WebCamHelper.GetDevice(false, 640, 428);			
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
		aspectRatio = W / (float)H;
	}



	void Update()
	{
		currentTime = Time.time;

		timer += Time.deltaTime;
		if(timer > intervalDecode)
		{
			CheckResolution();

			DecodeQR();
			timer = 0;

			//quadRenderer.transform.SetRotationZ(-webCamRender.videoRotationAngle);
		
			
			//float scaleY = webCamRender.videoVerticallyMirrored ? -1.0f : 1.0f;
			//quadRenderer.transform.localScale = new Vector3(aspectRatio * 1, scaleY, 1);

		}
	}

	void GetCameraTexture()
	{					
		if(webCamRender == null || !webCamRender.isPlaying)
			return;

		if(webCamRender.videoVerticallyMirrored)
			cameraTexture = WebCamHelper.MirrorTexture(webCamRender.GetPixels32(), W, H, false, false);
		else
			cameraTexture = webCamRender.GetPixels32();	
	}

	void DecodeQR()
	{
		GetCameraTexture();
		if(cameraTexture == null)
			return;
		
		Debug.Log("Scan Now at " + currentTime + " with " + cameraTexture.Length + " pixels");

		// decode the current frame
		var result = barcodeReader.Decode(cameraTexture, W, H);

		if(result != null)
		{
			Debug.Log(result.Text);
			if(OnResult != null)
				OnResult(result.Text);
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

	public WebCamTexture WebCamRender
	{
		get{ return webCamRender; }
	}
}
