using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class Main : MonoBehaviour {
	private LeapParser controller;
	private MouseParser mouseParser;
	private KeyParser keyParser;
	private Model position;
	private Models  objects;
	private int current = 0;
	// Use this for initialization
	void Start () {
		/* models init */
		GameObject def = GameObject.FindWithTag("default");
		objects = new Models(def);
		def.SetActive(false);
		controller = new LeapParser(objects);
		mouseParser = new MouseParser(objects);
		keyParser = new KeyParser(objects);
	}
	
	// Update is called once per frame
	void Update () {
		if(!controller.update ()){
			if(!mouseParser.update ()){
				keyParser.update();
			}
		}
	}

}
