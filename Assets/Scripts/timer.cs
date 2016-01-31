using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer: MonoBehaviour
{
	public int currentTime;	//Tiempo actual
	public Text timeText;	//Campo de texto

	void Start ()
	{
		countTime (); //Empieza a contar
	}

	void countTime ()
	{
		currentTime++; //Suma al tiempo 
		timeText.text = currentTime.ToString (); //Muestra el texto
		Invoke ("countTime", 1); //Continua contando
	}
}
