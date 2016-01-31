using UnityEngine;
using System.Collections;
using WiimoteApi;
using WiimoteApi.Util;

public class WiiMoteTest : MonoBehaviour {
	private Wiimote wiimote = null;
	private Vector2 accelerations; 
	public long timesPerSecond;
	private float lastUpdate;
	// Use this for initialization
	void Start () {
		lastUpdate = Time.fixedTime;
	}
	void Awake(){
		if (!WiimoteManager.HasWiimote()) { 
			Debug.Log ("shit is asleep");
		}
	}
	// Update is called once per frame
	void Update () {
		Debug.Log ("Ms to update" + 1/timesPerSecond);
		Debug.Log ("LastUpdate:" + lastUpdate);
		Debug.Log ("time now:" + Time.fixedTime);
		if (Time.fixedTime - lastUpdate  > 1/timesPerSecond) {
			updateWiiMote ();
		}
	}
	void updateWiiMote(){
		float angle; 
		if ( !WiimoteManager.HasWiimote()) { 

			WiimoteManager.FindWiimotes ();
			wiimote = WiimoteManager.Wiimotes[0];
			wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
			return;
		}
		wiimote.SendPlayerLED(true, false, false, true);
		int ret;
		do
		{
			ret = wiimote.ReadWiimoteData();
			accelerations = getAccelerationVector();
			Debug.Log("2d vector:" +accelerations);
			Debug.Log("3d vector:" + get3dVector());
			angle = getAngle(accelerations);
			Debug.Log("angle:" + angle);
			//Debug.Log(wiimote.MotionPlus.PitchSpeed);
			if (wiimote.Button.a) {

			}
			if(wiimote.Button.b){
				wiimote.Accel.CalibrateAccel (0);

			}
			if(wiimote.Button.minus){
				//FinishedWithWiimotes();
			}
		} while (ret > 0);
		Debug.Log ("Exit ret");
	}
	Vector3 get3dVector(){
			float accel_x;
			float accel_y;
			float accel_z;
						float[] accValues = wiimote.Accel.GetCalibratedAccelData();

			accel_x = accValues[0];
			accel_y = -accValues[2];
			accel_z = -accValues[1];
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
