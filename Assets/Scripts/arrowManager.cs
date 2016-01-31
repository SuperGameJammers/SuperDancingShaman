using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class arrowManager: MonoBehaviour
{
	public GameObject arrow;
	public Text remeaningText;

	public Transform anchorPointArrow;
	public int arrowsNumber;


	private GameObject arrowGenerated;
	private int[] arrowRotation;


	void Start ()
	{
		SV.remeaningArrows = arrowsNumber;
		SV.streak = 0;

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
		if (Input.GetKeyDown (SV.arrowDirection))
		{
			arrowGenerated.GetComponent<Animator> ().SetBool ("delete", true);
			Destroy (arrowGenerated, 0.5f);

			SV.remeaningArrows--;
			remeaningText.GetComponent<Text> ().text = SV.remeaningArrows + " More";

			generateArrow ();
		}
	}

	void generateArrow ()
	{
		int randomRotation = Random.Range (0, 3);

		arrowGenerated = Instantiate (arrow, anchorPointArrow.position, Quaternion.Euler (0, 0, arrowRotation[randomRotation])) as GameObject;

		switch (randomRotation)
		{
			case 0:
				SV.arrowDirection = KeyCode.UpArrow;
				break;
			case 1:
				SV.arrowDirection = KeyCode.LeftArrow;
				break;
			case 2:
				SV.arrowDirection = KeyCode.DownArrow;
				break;
			case 3:
				SV.arrowDirection = KeyCode.RightArrow;
				break;
		}
	}
}
