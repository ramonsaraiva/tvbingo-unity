using UnityEngine;

public class WebCamHelper
{

	public static WebCamTexture GetDevice(bool frontFace, int w, int h)
	{
		WebCamTexture webCameraTexture = null; 
		foreach(var cam in WebCamTexture.devices)
		{
			Debug.Log(cam);
			if((cam.isFrontFacing && frontFace) || (!cam.isFrontFacing && !frontFace))
			{    	
				Debug.Log(cam.name);
				webCameraTexture = new WebCamTexture(cam.name, w, h);
				webCameraTexture.deviceName = cam.name;
				break;
			}
		}

		return webCameraTexture;
	}

	public static Texture2D MirrorTexture(Texture2D originalTexture, bool horizontal, bool vertical)
	{
		Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);

		Color[] originalPixels = originalTexture.GetPixels(0);
		Color[] newPixels = newTexture.GetPixels(0);
		for(int y = 0; y < originalTexture.height; y++)
		{
			for(int x = 0; x < originalTexture.width; x++)
			{
				int newX = horizontal ? (newTexture.width - 1 - x) : x;
				int newY = vertical ? (newTexture.height - 1 - y) : y;
				newPixels[(newY * newTexture.width) + newX] = originalPixels[(y * originalTexture.width) + x];
			}
		}
		newTexture.SetPixels(newPixels, 0);
		newTexture.Apply();
		return newTexture;
	}

	public static Color32[] MirrorTexture(Color32[] originalPixels, int width, int height, bool horizontal, bool vertical)
	{
		Color32[] newPixels = new Color32[originalPixels.Length];
		for(int y = 0; y < height; y++)
		{
			for(int x = 0; x < width; x++)
			{
				int newX = horizontal ? (width - 1 - x) : x;
				int newY = vertical ? (height - 1 - y) : y;
				newPixels[(newY * width) + newX] = originalPixels[(y * width) + x];
			}
		}


		return newPixels;
	}
}


