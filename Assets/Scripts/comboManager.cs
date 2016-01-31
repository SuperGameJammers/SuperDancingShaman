using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class comboManager: MonoBehaviour
{
	public Text comboText;	//Campo de texto

	void Update ()
	{
		//Si se presiona la tecla correspondiente a la flecha
		if (Input.GetKeyDown (SV.arrowDirection))
		{
			SV.combo++; //Suma al combo

			//Si el combo es mayor o igual a 3
			if (SV.combo >= 3)
			{
				positionComboText (); //Posiciona el combo de manera aleatoria
				comboText.text = SV.combo + " Combo!"; //Muestra el combo actual
			}
			else
			{
				positionComboText (); //Posiciona el combo de manera aleatoria
				comboText.text = "Well done"; //Vacia el campo de texto
			}
		}
		else if (Input.anyKeyDown)
		{
			SV.combo = 0; //Resetea el combo
			positionComboText (); //Posiciona el combo de manera aleatoria
			comboText.text = "Oops";
		}
	}

	void positionComboText ()
	{
		comboText.rectTransform.rotation = Quaternion.Euler (0, 0, Random.Range (-45, 45)); //Establece la rotacion de manera aleatoria
		comboText.fontSize = Random.Range (20, 30); //Establece el tamaño de la fuente de manera aleatoria
	}
}

