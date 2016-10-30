using UnityEngine;
using System.Collections;

public class movingUP : MonoBehaviour {

	public float speed = 0.1f;
	private float position = 0.0f;
	private float nextActionTime = 0.0f;
	public float period = 0.01f;
	private float acceleration = 0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (0, position, -10);	
		position += speed;
		if (Time.time > nextActionTime ) {
			nextActionTime += period;
			speed += acceleration;
		}
	}
}
