using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class arrowManager: MonoBehaviour
{
	public GameObject arrow;                    //Flecha
	public Transform anchorPointArrow;          //Punto de generacion de la flecha
	public Text remeaningText;                  //FLechas restantes
	public int arrowsNumber;                    //NUmero de flechas


	private GameObject arrowGenerated;			//Flecha generada
	private int[] arrowRotation;				//Matriz de rotaciones de la flehca


	void Start ()
	{
		SV.remeaningArrows = arrowsNumber; //Establece el numero maximo de flechas
		SV.combo = 0; //Resetea el combo

		arrowRotation = new int[4]; //Establece la longitud de la matriz de flechas
		int angle = 0; //Angulo de rotacion de la flecha

		//Recorre la matriz
		for (int i = 1; i < arrowRotation.Length; i++)
		{
			angle += 90; //Establece los posibles angulos
			arrowRotation[i] = angle; //Establece el angulo para cada elemento
		}

		generateArrow (); //Genera una flecha
	}

	void Update ()
	{
		//Si se presiona la tecla correspondiente a la flecha
		if (Input.GetKeyDown (SV.arrowDirection))
		{
			arrowGenerated.GetComponent<Animator> ().SetBool ("delete", true); //Activa la animacion
			Destroy (arrowGenerated, 0.5f); //Destruye la flecha

			SV.remeaningArrows--; //Resta una flecha al total
			remeaningText.GetComponent<Text> ().text = SV.remeaningArrows + " More"; //Muestra el numero de flechas restantes

			generateArrow (); //Genera una flecha
		}
	}

	//Se llama cuando se quiere generar una flecha
	void generateArrow ()
	{
		int randomRotation = Random.Range (0, 3); //Devuelve una rotacion aleatoria para la flecha

		//Genera una flecha segun el numero dado
		arrowGenerated = Instantiate (arrow, anchorPointArrow.position, Quaternion.Euler (0, 0, arrowRotation[randomRotation])) as GameObject;

		//Devuelve el valor que tomara la tecla segun la flecha generada
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
