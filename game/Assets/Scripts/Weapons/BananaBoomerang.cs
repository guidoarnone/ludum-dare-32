using UnityEngine;
using System.Collections;

public class BananaBoomerang : MonoBehaviour {

	public float speedAbs;
	public float range;
	public float distanceThreshold;

	private Vector3 direction;
	public Vector3 origin;
	public Vector3 turningPoint;

	public GameObject particleEmitter;

	private enum Segment {forwards, backwards, done};
	private Segment currentSegment;

	void Start() 
	{
		currentSegment = Segment.forwards;
		origin = transform.position;
		turningPoint = origin + direction * range;
		transform.LookAt (transform.position + Vector3.down);  
	}

	void Update () 
	{
		float deltaX = speed()*Time.deltaTime;
		transform.Translate (direction * deltaX, Space.World);
		checkSegment ();

		if (currentSegment.Equals(Segment.done)) 
		{
			burial(); // lets this poor banana do whatever it needs before dying. :'(
			Destroy(gameObject);
		}

	}

	private void checkSegment() 
	{
		if(currentSegment.Equals(Segment.forwards) && isNearTurningPoint())
		{
			currentSegment = Segment.backwards;
		}
		else if(currentSegment.Equals(Segment.backwards) && isNearOrigin())
		{
			currentSegment = Segment.done;
		}
	}

	private bool isNearTurningPoint()
	{
		return (Vector3.Distance (transform.position, turningPoint) < distanceThreshold);
	}

	private bool isNearOrigin()
	{
		return (Vector3.Distance (transform.position, origin) < distanceThreshold);
	}

	public void setDirection(Vector3 d)
	{
		direction = d;
		transform.LookAt(transform.position + d);
	}

	private float speed()
	{
		switch (currentSegment)
		{
			case Segment.forwards: 
				return speedAbs;
			case Segment.backwards:
				return (-1)*speedAbs;
			case Segment.done:
			default:
				return 0;
		}
	}

	private void burial()
	{
		Instantiate(particleEmitter, transform.position, Quaternion.identity);
	}

}
