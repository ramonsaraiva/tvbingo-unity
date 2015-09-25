
using UnityEngine;

using ZXing;
using ZXing.QrCode;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class QRGenerator : MonoBehaviour
{
	// Texture for encoding test
	public Texture2D toEncodeTexture;

	public string textToEncode;

	void Start()
	{
		CreateQRCode(textToEncode);
	}

	public void CreateQRCode(string textForEncoding)
	{
		if(textForEncoding != null)
		{
			var color32 = Encode(textForEncoding, toEncodeTexture.width, toEncodeTexture.height);
			toEncodeTexture.SetPixels32(color32);
			toEncodeTexture.Apply();

			byte[] bytes = toEncodeTexture.EncodeToPNG();		
		
			// For testing purposes, also write to a file in the project folder
			File.WriteAllBytes(Application.dataPath + "/../QRCode.png", bytes);
		}
	}


	private static Color32[] Encode(string textForEncoding, int width, int height)
	{
		var writer = new BarcodeWriter {
			Format = BarcodeFormat.QR_CODE,
			Options = new QrCodeEncodingOptions {
				Height = height,
				Width = width
			}
		};
		return writer.Write(textForEncoding);
	}
}
