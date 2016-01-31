using UnityEngine;
using System.Collections;
using WiimoteApi;
using WiimoteApi.Util;

public class WiiMoteTest : MonoBehaviour {
	private Wiimote wiimote = null;
	private Vector2 accelerations; 
	public Rigidbody2D rb2D;
	public Rigidbody rb3D;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		rb3D = GetComponent<Rigidbody> ();
	}
	void Awake(){
		if (!WiimoteManager.HasWiimote()) { 
			Debug.Log ("shit");
		}
	}
	// Update is called once per frame
	void Update () {
		float angle; 
		if ( !WiimoteManager.HasWiimote()) { 

			WiimoteManager.FindWiimotes ();
			return;
		}
		wiimote = WiimoteManager.Wiimotes[0];
		wiimote.ActivateWiiMotionPlus ();
		wiimote.SendPlayerLED(true, false, false, true);
		int ret;
		do
		{
			ret = wiimote.ReadWiimoteData();

			rb3D.transform.Rotate(get3dVector(wiimote.Accel.GetCalibratedAccelData ()));
			Debug.Log(wiimote.MotionPlus.PitchSpeed);
			accelerations = getAccelerationVector(wiimote.Accel.GetCalibratedAccelData ());
			Debug.Log(accelerations.x);
			angle = getAngle(accelerations);
			rb2D.MoveRotation(angle);
			if (wiimote.Button.a) {
				Debug.Log ("pressed");
				wiimote.Accel.CalibrateAccel (0);
			}
			if(wiimote.Button.minus){
				FinishedWithWiimotes();
			}
	


		} while (ret > 0);
		}
	Vector3 get3dVector(float[] accValues){
			float accel_x;
			float accel_y;
			float accel_z;

			accel_x = accValues[0];
			accel_y = -accValues[2];
		accel_z = -accValues[2];
		return new Vector3 (accel_x, accel_y, accel_z);
	}
	float getAngle(Vector2 acc){
		float angle = Mathf.Atan (acc.x / acc.y);
		Debug.Log ("angle is:" + angle);
		return angle;
	}
	Vector2 getAccelerationVector(float[] accValues){
		float accel_x;
		float accel_y;


		accel_x = accValues[0];
		accel_y = -accValues[2];

		return new Vector2(accel_x, accel_y);
	}
	void OnApplicationQuit(){
		FinishedWithWiimotes();
		Debug.Log ("quit");
	}

	void FinishedWithWiimotes() {
		foreach(Wiimote remote in WiimoteManager.Wiimotes) {
			WiimoteManager.Cleanup(remote);
		}
	}
}
