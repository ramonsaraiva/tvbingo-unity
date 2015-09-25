using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour {

	void OnMouseDown(){
		gameObject.GetComponent<TextMesh>().color = Color.cyan;
	}
}
