using UnityEngine;
using System.Collections;
using WiimoteApi;
using WiimoteApi.Util;

public class WiiMoteTest : MonoBehaviour
{
	private Wiimote wiimote = null;
	private Vector2 accelerations;
	public long timesPerSecond = 5;
	private float lastUpdate;
	public Rigidbody2D shape;
	private float[] gravity = new float[3];

	enum Direction
	{
		up,
		down,
		left,
		right}

	;
	// Use this for initialization
	void Start ()
	{
		lastUpdate = Time.fixedTime;
	}

	void Awake ()
	{
		if (!WiimoteManager.HasWiimote ()) { 
			Debug.Log ("shit is asleep");
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (WiimoteManager.HasWiimote ()) {
			if (Time.fixedTime - lastUpdate > 1 / timesPerSecond) {
				updateWiiMote ();
			}
		} else {
			WiimoteManager.FindWiimotes ();
		}
	}

	void updateWiiMote ()
	{
		float angle; 


		wiimote = WiimoteManager.Wiimotes [0];
		wiimote.SendDataReportMode (InputDataType.REPORT_BUTTONS_ACCEL);
	
		wiimote.SendPlayerLED (true, false, false, true);
		int ret;
		do {
			ret = wiimote.ReadWiimoteData ();
			accelerations = getAccelerationVector ();
			angle = getAngle (accelerations);
			shape.MoveRotation (angle * Time.deltaTime);

			int direction = (isMoving (accelerations)) ? (int)getDirection (accelerations) : 0;
			Debug.Log (direction);
			if (wiimote.Button.a) {

			}
			if (wiimote.Button.b) {
				wiimote.Accel.CalibrateAccel (0);

			}
			if (wiimote.Button.minus) {
				//FinishedWithWiimotes();
			}
		} while (ret > 0);
	}

	bool isMoving (Vector2 accValues)
	{
		decimal x = decimal.Round ((decimal)accValues.x);
		decimal y = decimal.Round ((decimal)accValues.y);

		return ((x > (decimal)0.5 || x < -(decimal)0.5) || (y > (decimal)0.5 || y < -(decimal)0.5));
	}

	Direction getDirection (Vector2 accValues)
	{
		decimal x = decimal.Round ((decimal)accValues.x);
		decimal y = decimal.Round ((decimal)accValues.y);
		if (y > x) {
			if (y < 0)
				return Direction.up;
			else
				return Direction.down;
		} else {
			if (x < 0)
				return Direction.right;
			else
				return Direction.left;
			
		}
			
	}

	Vector3 get3dVector ()
	{
		float accel_x;
		float accel_y;
		float accel_z;
		float[] accValues = wiimote.Accel.GetCalibratedAccelData ();

		accel_x = accValues [0];
		accel_y = -accValues [2];
		accel_z = -accValues [1];
		return new Vector3 (accel_x, accel_y, accel_z);
	}

	float getAngle (Vector2 acc)
	{
		float angle = Mathf.Atan (acc.y / acc.x);
		return Mathf.Rad2Deg * angle;
	}

	Vector2 smoothenValues (float[] values)
	{

		float alpha = 0.8f;

		gravity [0] = alpha * gravity [0] + (1 - alpha) * values [0];
		gravity [1] = alpha * gravity [1] + (1 - alpha) * values [1];

		values [0] = values [0] - gravity [0];
		values [1] = values [1] - gravity [1];
		return new Vector2 (values [0], values [1]);
	}

	Vector2 getAccelerationVector ()
	{
		float accel_x;
		float accel_y;
		float[] accValues = wiimote.Accel.GetCalibratedAccelData ();
		accel_x = accValues [0];
		accel_y = -accValues [2];

		return smoothenValues (new float[2] { accel_x, accel_y });
	}

	void OnApplicationQuit ()
	{
		FinishedWithWiimotes ();
		Debug.Log ("quit");
	}

	void FinishedWithWiimotes ()
	{
		foreach (Wiimote remote in WiimoteManager.Wiimotes) {
			WiimoteManager.Cleanup (remote);
		}
	}
}
