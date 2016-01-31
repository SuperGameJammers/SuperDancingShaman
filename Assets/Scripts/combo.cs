using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class combo: MonoBehaviour
{
	public Text comboText;

	void Update ()
	{
		if (Input.GetKeyDown (SV.arrowDirection))
		{
			SV.streak++;

			if (SV.streak >= 3)
			{
				comboText.rectTransform.rotation = Quaternion.Euler (0, 0, Random.Range (-45, 45));
				comboText.fontSize = Random.Range (20, 30);
				comboText.text = SV.streak + " Combo!";
			}
			else
			{
				comboText.text = "";
			}
		}
		else if (Input.anyKeyDown)
		{
			SV.streak = 0;

			comboText.rectTransform.rotation = Quaternion.Euler (0, 0, Random.Range (-45, 45));
			comboText.fontSize = Random.Range (20, 30);
			comboText.text = "Oops";
		}
	}
}

