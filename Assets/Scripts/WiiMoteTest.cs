using UnityEngine;
using System.Collections;
using WiimoteApi;
using WiimoteApi.Util;

public class WiiMoteTest : MonoBehaviour
{
	private Wiimote wiimote = null;
	private Vector2 accelerations, oldAccelerations, velocity;
	public long timesPerSecond = 5;
	public decimal movementThreshold = 0.5m;
	private float lastUpdate;
	private float[] gravity = new float[3];

	enum Direction
	{
		up = 0,
		down = 1,
		left = 2,
		right = 3}

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


		wiimote = WiimoteManager.Wiimotes [0];
		wiimote.SendDataReportMode (InputDataType.REPORT_BUTTONS_ACCEL);
	
		wiimote.SendPlayerLED (true, false, false, true);
		int ret;
		do {
			ret = wiimote.ReadWiimoteData ();
			oldAccelerations = accelerations;
			accelerations = getAccelerationVector ();

			velocity = calculateVelocity (oldAccelerations, accelerations);
			int direction = (isMoving (velocity)) ? (int)getDirection (velocity) : 5;
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

	Vector2 calculateVelocity (Vector2 old, Vector2 current)
	{
		float timeSinceUpdate = (timesPerSecond / 1);

		return (current - old) * timeSinceUpdate;
	}

	bool isMoving (Vector2 accValues)
	{
		decimal x = (decimal)accValues.x;
		decimal y = (decimal)accValues.y;
		return ((x > movementThreshold || x < -movementThreshold) || (y > movementThreshold || y < -movementThreshold));
	}

	Direction getDirection (Vector2 accValues)
	{
		decimal x = ((decimal)accValues.x);
		decimal y = ((decimal)accValues.y);
		if (Mathf.Abs (accValues.y) > Mathf.Abs (accValues.x)) {
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
		accel_y = -accValues [1];
		accel_z = -accValues [2];
		return new Vector3 (accel_x, accel_y, accel_z);
	}

	float getAngle (Vector2 acc)
	{
		float angle = Mathf.Atan (acc.y / acc.x);
		return Mathf.Rad2Deg * angle;
	}



	Vector2 getAccelerationVector ()
	{
		float accel_x;
		float accel_y;
		float[] accValues = wiimote.Accel.GetCalibratedAccelData ();
		accel_x = accValues [0];
		accel_y = -accValues [2];

		return  new Vector2 (accel_x, accel_y);
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
