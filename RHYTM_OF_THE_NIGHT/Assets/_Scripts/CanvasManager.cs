using UnityEngine;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{

    public	static CanvasManager	Instance				= null;

	public	float					SpawnDistance			= 5f;

	public	GameObject				m_ObjToSpawn			= null;

	public	Transform				m_Mosca					= null;

	/*
	public HUD HUDref;

	public Sprite DisabledBlue, HighlightedBlue, EnabledBlue;
	public Sprite DisabledRed, HighlightedRed, EnabledRed;
	*/


	private void Awake()
	{
		Instance = this;

		FMOD_BeatListener.Instance.OnMark += OnMark;
		FMOD_BeatListener.Instance.OnBeat += OnBeat;

//		m_Mosca = new GameObject().transform;

		m_Mosca = GameObject.CreatePrimitive(PrimitiveType.Sphere ).transform;
		m_Mosca.localScale = Vector3.one * 10f;
		m_Mosca.SetParent( transform );
		m_Mosca.localPosition = Vector3.zero;
	}



	public	void	Restart()
	{
		

	}


	private	void	OnBeat( int i )
	{
		
	}


	private	void	OnMark( string markName )
	{
		if ( enabled == false )
			return;

		Nextbutton();
	}

	// Parameters
    public float maximumLinearVelocity = 1;
    public float maximumAngularVelocity = 90;

    public float maximumLinearAcceleration = 1;
    public float maximumLinearDeceleration = 1;
    public float maximumAngularAcceleration = 90;

    // State
    private Vector2 linearVelocity;
    private float angularVelocity;

    private float currentLinearVelocity;

    private float linearAcceleration;
    private float angularAcceleration;
	
	private Vector2 targetLinearVelocityPercent;
	

	private	void	Update()
	{
		if ( FMOD_BeatListener.Instance.IsPaused == false )
			return;

		// Get the steering output
		Vector2 totalSteering = targetLinearVelocityPercent;

		// Set accelerations based on steering target
		var targetVelocityPercent = totalSteering; // [0,1]
		var targetVelocity = targetVelocityPercent * maximumLinearVelocity;

		if (targetVelocity.sqrMagnitude > currentLinearVelocity * currentLinearVelocity)
		{
			linearAcceleration = maximumLinearAcceleration;
		}
		else if (targetVelocity.sqrMagnitude < currentLinearVelocity * currentLinearVelocity)
		{
			linearAcceleration = -maximumLinearDeceleration;
		}
		else
		{
			linearAcceleration = 0;
		}

		Vector3 crossVector = Vector3.Cross(targetVelocityPercent, m_Mosca.transform.right);
		float angle = Mathf.Abs(Vector3.Angle(targetVelocityPercent, m_Mosca.transform.right));
		int rotationDirection = -(int)Mathf.Sign(crossVector.z);

		float accelerationRatio = Mathf.Clamp(angle, 0, 10) / 5.0f;
		angularAcceleration = rotationDirection * maximumAngularAcceleration * accelerationRatio;

		// Velocity update
		currentLinearVelocity  += linearAcceleration * Time.deltaTime;
		currentLinearVelocity = Mathf.Clamp(currentLinearVelocity, 0, maximumLinearVelocity);
		linearVelocity = (Vector2)m_Mosca.transform.right * currentLinearVelocity;

		angularVelocity += angularAcceleration * Time.deltaTime;
		angularVelocity = Mathf.Clamp(angularVelocity, -maximumAngularVelocity, maximumAngularVelocity);
		angularVelocity = angularVelocity * accelerationRatio;


		// Position / rotation update
		m_Mosca.transform.position += (Vector3)(linearVelocity * Time.deltaTime);
		m_Mosca.transform.localEulerAngles += Vector3.forward * angularVelocity * Time.deltaTime;
	}


	
	GameObject prevSpot = null;
	private void Nextbutton()
	{
		float width = Screen.width;
		float height = Screen.height;

		Vector3 spawnPoint = new Vector3( Random.Range( 32, width - 32 ), Random.Range( 32, height - 32 ), 0f );

		if ( prevSpot != null )
		{
			Vector3 direction = ( spawnPoint - prevSpot.transform.position ).normalized;
			spawnPoint = prevSpot.transform.position + direction * SpawnDistance;
		}
		targetLinearVelocityPercent = ( spawnPoint - m_Mosca.transform.position ).normalized;

		prevSpot = Instantiate( m_ObjToSpawn, m_Mosca.transform.position, Quaternion.identity, transform );
	}

}
