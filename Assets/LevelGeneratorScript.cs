using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorScript : MonoBehaviour {

	const int LAYER = 9;
	const int BUFFER_SIZE = 16;

	public GameObject myPrefab;
	public int stepHeight;

	private int currentOffset = 0;
	private Queue<GameObject> buffer = new Queue<GameObject> ();
	//private GameObject lastInserted;

	// Use this for initialization
	void Start () {
		GameObject go = spawnPlatform(Random.Range (-15, 15), getCurrentY());
		//this.lastInserted = go;
		this.buffer.Enqueue (go);
		for (int i = 0; i < LevelGeneratorScript.BUFFER_SIZE - 1; ++i) {
			go = spawnPlatform(Random.Range (-15, 15), getCurrentY());
			//this.lastInserted = go;
			this.buffer.Enqueue (go);
		}
	}

	// Update is called once per frame
	void Update () {
		float smallestY = this.buffer.Peek ().transform.position[1];
		float treshold = Camera.main.transform.position[1] - (LevelGeneratorScript.BUFFER_SIZE * this.stepHeight) + 40;
		//Debug.Log ("smallestY : " + smallestY);
		//Debug.Log ("treshold : " + treshold);

		//Debug.Log ("Camera.main.transform.position :");

		if (treshold > smallestY) {
			Destroy( this.buffer.Dequeue () );
			GameObject go = spawnPlatform(Random.Range (-15, 15), getCurrentY());
		    //this.lastInserted = go;
			this.buffer.Enqueue (go);
		}
		
	}

	private GameObject spawnPlatform(float x, float y) {
		Vector3 position = new Vector3 (x, y, 0);
		GameObject go = (GameObject)Instantiate(myPrefab, position, Quaternion.identity, this.transform.parent);
		go.layer = LevelGeneratorScript.LAYER;
		go.transform.localScale = new Vector3 (3, 1, 1);
		go.transform.parent = this.transform.parent;
		go.name = "platform x -> " + x + "; y -> " + y;
		return go;
	}

	private int getCurrentY() {
		this.currentOffset += this.stepHeight;
		return this.currentOffset;
	}
}