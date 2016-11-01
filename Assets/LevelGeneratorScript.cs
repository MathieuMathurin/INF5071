using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorScript : MonoBehaviour {

	const int LAYER = 9;

	public GameObject myPrefab;
	public int stepHeight;



	private int count = 5;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		int x = Random.Range (-15, 15);
		spawnPlatform (x, this.count);
		this.count += this.stepHeight;
	}

	private void spawnPlatform(float x, float y) {
		Vector3 position = new Vector3 (x, y, 0);
		GameObject go = (GameObject)Instantiate(myPrefab, position, Quaternion.identity, this.transform.parent);
		go.layer = LevelGeneratorScript.LAYER;
		go.transform.localScale = new Vector3 (3, 1, 1);
		go.transform.parent = this.transform.parent;


	}
}