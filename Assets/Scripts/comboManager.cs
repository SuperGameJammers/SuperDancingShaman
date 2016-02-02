using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class comboManager: MonoBehaviour
{
	public Text[] comboText;				//Campo de texto
	public Color defaultColor, wrongColor;	//Colores texto

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
				foreach (var item in comboText)
				{
					item.color = defaultColor; //Colorea el texto
					item.text = SV.combo + " Combo!"; //Muestra el combo actual
				}
			}
			else
			{
				positionComboText (); //Posiciona el combo de manera aleatoria
				foreach (var item in comboText)
				{
					item.color = defaultColor; //Colorea el texto
					item.text = SV.combo + " Well done!"; //Muestra un mensaje
				}
			}
		}
		else if (Input.anyKeyDown)
		{
			SV.combo = 0; //Resetea el combo
			positionComboText (); //Posiciona el combo de manera aleatoria
			foreach (var item in comboText)
			{
				item.color = wrongColor; //Colorea el texto
				item.text = " Oops!"; //Muestra un mensaje
			}
		}
	}

	void positionComboText ()
	{
		foreach (var item in comboText)
		{
			item.rectTransform.rotation = Quaternion.Euler (0, 0, Random.Range (-45, 45)); //Establece la rotacion de manera aleatoria
			item.fontSize = Random.Range (30, 35); //Establece el tamaño de la fuente de manera aleatoria
		}
	}
}

