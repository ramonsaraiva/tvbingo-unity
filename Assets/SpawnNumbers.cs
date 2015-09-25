using UnityEngine;
using System.Collections;

public class SpawnNumbers : MonoBehaviour {

	public Vector2 init;
	public Vector2 offset;

	public GameObject number;

	GameObject[,] card = new GameObject[3,9];

	// Use this for initialization
	void Start () {
		for(int y = 0; y < 3; ++y){
			for(int x = 0; x < 9; ++x){
				GameObject go = Instantiate(number, 
				                            init + new Vector2 (offset.x * x, offset.y * y), 
				                            number.transform.rotation) as GameObject;
				go.GetComponent<TextMesh>().text = (x + 10 * y).ToString();
			}
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
