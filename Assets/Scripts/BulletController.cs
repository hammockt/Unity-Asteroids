﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		//Set the bullet to destory itself after 1 second
		Destroy (gameObject, 1.0f);

		//Push the bullet in the direction is is facing
		GetComponent<Rigidbody2D> ().AddForce (transform.up * 400);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
