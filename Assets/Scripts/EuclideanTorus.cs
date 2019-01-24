using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EuclideanTorus : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//teleport the game object
		if (transform.position.x > 9) {
			transform.position = new Vector3 (-9, transform.position.y, 0);
		} else if (transform.position.x < -9) {
			transform.position = new Vector3 (9, transform.position.y, 0);
		} else if (transform.position.y > 6) {
			transform.position = new Vector3 (transform.position.x, -6, 0);
		} else if (transform.position.y < -6) {
			transform.position = new Vector3 (transform.position.x, 6, 0);
		}
	}
}
