using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using AssemblyCSharp.Configure; 
public class Main : MonoBehaviour {
	private LeapParser3 controller;
	private MouseParser mouseParser;
	private KeyParser keyParser;
	private IdleParser idleParser;
	private Models  objects;
	private float timeInactive;
	private Options options;
	// Use this for initialization
	void Start () {
		/* models init */
		Screen.showCursor = false;
		//Find Default gameObject and config file
		GameObject def = GameObject.FindWithTag("default");
		ConfigReader reader = new ConfigReader ();
		objects = new Models(def,reader);
		def.SetActive(false);

		//Interaction parsers ////////////////
		controller = new LeapParser3(objects);
		mouseParser = new MouseParser(objects);
		keyParser = new KeyParser(objects);
		idleParser = new IdleParser (objects);

		this.options = reader.Options;
		timeInactive = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(controller.update () ==true
		|| mouseParser.update () ==true
		|| keyParser.update() == true){
			timeInactive = 0;
		}else{
			timeInactive +=Time.deltaTime ;

		}
		if (this.options.autoRotation>0 && timeInactive > this.options.autoRotation) {
			//We're inactive...
			idleParser.update(timeInactive);
		}

		if (this.options.switchAfter > 0 && timeInactive > this.options.switchAfter) {
			this.objects.next();
			timeInactive =0f;
		}

		if (this.options.exitAfter > 0 && timeInactive > this.options.exitAfter) {
			//Application.Quit(); //Does not work : bug in unity
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}
	}
}
