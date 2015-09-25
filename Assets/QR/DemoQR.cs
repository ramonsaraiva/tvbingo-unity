using UnityEngine;
using System.Collections;

public class DemoQR : MonoBehaviour
{
	public QRCodeReader qrCodeReader;

	public Renderer quad;

	private bool initialize;

	// Use this for initialization
	IEnumerator Start()
	{
		yield return new WaitForSeconds(1);
		qrCodeReader.OnResult += OnQRResult;
		qrCodeReader.StartCameraRender();
	}

	void OnQRResult(string obj)
	{
		Debug.Log(obj);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!initialize)
		{
			if(qrCodeReader.WebCamRender.isPlaying)
			{
				initialize = true;
				quad.material.mainTexture = qrCodeReader.WebCamRender;
			}
		}
	}
}
