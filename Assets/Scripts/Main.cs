using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using AssemblyCSharp.Configure; 
using AssemblyCSharp.Utils;
public class Main : MonoBehaviour {
	private List<BaseParser> parsers;
	private Models  objects;
	private float timeInactive;
	private Options options;
	private IdleParser idleParser;
	private Queue<string> messages;
	// Use this for initialization
	void Start () {

		messages = new Queue<string>();

		/* models init */
		Screen.showCursor = false;
		//Find Default gameObject and config file
		GameObject def = GameObject.FindWithTag("default");
		ConfigReader reader = new ConfigReader ();
		objects = new Models(def,reader);
		def.SetActive(false);
		parsers = new List<BaseParser>();
		//Interaction parsers ////////////////
		parsers.Add(new LeapParser(objects));
		parsers.Add(new MouseParser(objects));
		parsers.Add(new KeyParser(objects));
		parsers.Add(new TcpParser(objects));
		idleParser = new IdleParser(objects);
		this.options = reader.Options;
		timeInactive = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(messages.Count >0){
			Debug.Log(messages.Dequeue());
		}
		bool isUpdated = false;
		foreach (BaseParser parser in parsers){
			if(parser.update()){
				isUpdated = true;
				break;
			}
		}
		if(isUpdated == true){
			timeInactive = 0;
		}else{
			timeInactive +=Time.deltaTime ;

		}
		if (this.options.autoRotation>0 && timeInactive > this.options.autoRotation) {
			//We're inactive...
			idleParser.update(timeInactive);
		}

		if (this.options.switchAfter > 0 && timeInactive > this.options.switchAfter+this.options.autoRotation) {
			this.objects.next();
			timeInactive =this.options.autoRotation;
		}

		if (this.options.exitAfter > 0 && timeInactive > this.options.exitAfter) {
			//Application.Quit(); //Does not work : bug in unity
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		if(Input.GetKeyDown("t")){
			UnityClient client = new UnityClient(messages);
			client.Send("meshes",true);
		}
	}
}
