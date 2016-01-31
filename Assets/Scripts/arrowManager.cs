using UnityEngine;
using System.Collections;

public class arrowManager: MonoBehaviour
{
	public GameObject arrow;	
	public Transform startPoint;


	private GameObject arrowGenerated;
	private int[] arrowRotation;
	private KeyCode arrowDirection;


	void Start ()
	{
		arrowRotation = new int[4];
		int angle = 0;
		for (int i = 1; i < arrowRotation.Length; i++)
		{
			angle += 90;
			arrowRotation[i] = angle;			
		}
		generateArrow ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (arrowDirection))
		{
			Debug.Log ("Bien hecho maldito");
			arrowGenerated.GetComponent<Animator> ().SetBool ("delete", true);
			Destroy (arrowGenerated, 0.5f);
			generateArrow ();
		}
		else if (Input.anyKeyDown)
		{
			Debug.Log ("Mal hecho");
		}
	}

	void generateArrow ()
	{
		int randomRotation = Random.Range (0, 3);

		arrowGenerated= Instantiate (arrow, startPoint.position, Quaternion.Euler (0, 0, arrowRotation[randomRotation])) as GameObject;
		
		switch (randomRotation)
		{
			case 0:
				arrowDirection = KeyCode.UpArrow;
				break;
			case 1:
				arrowDirection = KeyCode.LeftArrow;
				break;
			case 2:
				arrowDirection = KeyCode.DownArrow;
				break;
			case 3:
				arrowDirection = KeyCode.RightArrow;
				break;
		}

	}
}
