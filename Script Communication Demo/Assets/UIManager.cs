using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text name, location, gender, company, age;

	private Name _name;
	private AGL _agl;
	// Use this for initialization
	void Start () {
		_name = GetComponent<Name>();
		_agl = GameObject.Find("Main Camera").GetComponent<AGL>();
		name.text = "Name: " + _name.name;
		age.text = "Age: " + _agl.age;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
