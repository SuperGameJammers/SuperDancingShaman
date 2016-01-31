using UnityEngine;
using System.Collections;
using WiimoteApi;
using WiimoteApi.Util;

public class WiiMoteTest : MonoBehaviour {
	private Wiimote wiimote = null;
	private Vector2 accelerations; 
	// Use this for initialization
	void Start () {
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
		wiimote.SendPlayerLED(true, false, false, true);
		int ret;
		do
		{
			ret = wiimote.ReadWiimoteData();
			//Debug.Log(wiimote.MotionPlus.PitchSpeed);
			if (wiimote.Button.a) {
				accelerations = getAccelerationVector();
				Debug.Log("2d vector:" +accelerations);
				Debug.Log("3d vector:" + get3dVector());
				angle = getAngle(accelerations);
				Debug.Log("angle:" + angle);	
			}
			if(wiimote.Button.b){
				wiimote.Accel.CalibrateAccel (0);

			}
			if(wiimote.Button.minus){
				FinishedWithWiimotes();
			}
	


		} while (ret > 0);
		}
	Vector3 get3dVector(){
			float accel_x;
			float accel_y;
			float accel_z;
						float[] accValues = wiimote.Accel.GetCalibratedAccelData();

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
	Vector2 getAccelerationVector(){
		float accel_x;
		float accel_y;
						float[] accValues = wiimote.Accel.GetCalibratedAccelData();

		Debug.Log ("acc:" + accValues [0]);

		accel_x = accValues[0];
		accel_y = -accValues[2];

		return new Vector2(accel_x, accel_y).normalized;
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
