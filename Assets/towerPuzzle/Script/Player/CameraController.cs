using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject Player;
    private Vector3 vec;

    void Start ()
    {
         vec = this.transform.position - Player.transform.position;
	}
	
    void Update ()
    {
        this.transform.position = Player.transform.position + vec; 
	}
}
