using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
	IEnumerator Start()
	{
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");

		string data = "{\"code\": \"D2c2\"}";
		byte[] bdata = System.Text.Encoding.ASCII.GetBytes(data.ToCharArray());

		print ("wtf");

		WWW w = new WWW("http://192.168.1.103:5000/matches/cards/", bdata, headers);
		yield return w;

		JSONObject card = new JSONObject(w.text);
		print (card);
	}
	
	void Update()
	{
	}
}
