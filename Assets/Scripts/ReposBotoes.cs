using UnityEngine;
using System.Collections;

public class ReposBotoes : MonoBehaviour {

	public Camera interfaceCam;

	//Necessario atribuir um empty gameobject como pai de caad grupo de botoes, o qual tera a posicao no extremo da camera
	public GameObject botoesDireitaSuperior;
	public GameObject botoesEsquerdaSuperior;
	public GameObject botoesDireitaInferior;
	public GameObject botoesEsquerdaInferior;

	public GameObject fitScreen;
	
	
	void Start(){
		Vector3 pos;
		
		if(botoesDireitaSuperior){			
			pos = interfaceCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
			pos.z = botoesDireitaSuperior.transform.position.z;
			botoesDireitaSuperior.transform.position = pos;
		}

		if(botoesEsquerdaSuperior){			
			pos = interfaceCam.ViewportToWorldPoint(new Vector3(0, 1, 0));
			pos.z = botoesEsquerdaSuperior.transform.position.z;
			botoesEsquerdaSuperior.transform.position = pos;
		}

		if(botoesDireitaInferior){			
			pos = interfaceCam.ViewportToWorldPoint(new Vector3(1, 0, 0));
			pos.z = botoesDireitaInferior.transform.position.z;
			botoesDireitaInferior.transform.position = pos;
		}

		if(botoesEsquerdaInferior){			
			pos = interfaceCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
			pos.z = botoesEsquerdaInferior.transform.position.z;
			botoesEsquerdaInferior.transform.position = pos;
		}

		if(fitScreen){
			BoxCollider box = fitScreen.GetComponent<BoxCollider>();
			box.enabled = true;
			Vector3 bounds = box.bounds.size;
			box.enabled = false;
			//print (bounds);
			Vector3 screenSize = interfaceCam.ViewportToWorldPoint(new Vector3(1, 1, 0)) - interfaceCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
			//print (new Vector3(screenSize.x / bounds.x, screenSize.y / bounds.y, 1) + " / " +  screenSize);
			float scale = Mathf.Min(screenSize.x / bounds.x, screenSize.y / bounds.y);
			fitScreen.transform.localScale = new Vector3(scale, scale, 1);
		}
	}
}
