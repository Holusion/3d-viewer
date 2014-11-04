//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using Leap;
using UnityEngine;
namespace AssemblyCSharp
{
	public class LeapParser2 {
		private Controller controller;
		private Models models;
		private Vector3 rot;

		public LeapParser2 (Models models){
			this.models = models;
			rot = new Vector3(0,0,0);
			controller = new Controller();
			//Enabling gestures
			//controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
			
		}
		/**
		 * return true if leap is available and parsing suceed. False otherwise.
		 * Simply a	replacement, instead of using onFrame, thus syncing with app's pace, if called in update() method.
		 * */
		public bool update (){
			if(!controller.IsServiceConnected()){
				return false;
			}
			if(!controller.IsConnected){
				return false;
			}
			Frame frame = controller.Frame();

			//HandList hands = frame.Hands;
			//PointableList pointables = frame.Pointables;
			//FingerList fingers = frame.Fingers;
			//ToolList tools = frame.Tools;
			Hand hand = frame.Hands.Frontmost;
			//change (hand);
			return rotation(hand);
			
			
		}


		private float setRot(float old, float cmd){

			float ret;
			if(old ==0){ 
				//Debug.Log("begin movement");
				Debug.Log(cmd);
				ret = cmd /4;
			}else if(Math.Abs(cmd) > Math.Abs(old)*2f){ 
				//Debug.Log("smoothing");
				ret = old*2f;
			}else{
				//Debug.Log("move");
				ret = cmd;
			}
			return ret;
		}
		private bool rotation(Hand hand){
			float x = 0, y=0,z=0;
			Model model = models.getCurrent();
			Vector velocity = hand.PalmVelocity;
			//Don't play with recently detected hand!
			if(hand.TimeVisible<0.5f){
				return false;
			}
			Frame previousFrame = controller.Frame(5);
			Debug.Log("frame");
			z = -hand.RotationAngle(previousFrame,Vector.Backward)*1500f;

			//Debug.Log(nX);
			Vector3 newRot = new Vector3(-velocity.y/3f,velocity.x/3f,z);
			float translation = hand.TranslationProbability(previousFrame);
			float rotation = hand.RotationProbability(previousFrame);
			if (hand.PinchStrength>0.7f || hand.GrabStrength>0.8f){
				if(translation > rotation*1.5f){
					rot.x = setRot (rot.x,newRot.x);
					rot.y = setRot (rot.y,newRot.y);
					rot.z = 0;
				}else if(rotation > translation*1.5f) {
					rot.x = 0;
					rot.y = 0;
					rot.z = setRot (rot.z,newRot.z);
				}else{
					//If we're not really confident, just ignore everything
					rot.x = 0;
					rot.y = 0;
					rot.z = 0;
				}
			}else{
				rot.x = 0;
				rot.y = 0;
				rot.z = 0;
			}
			rot = rot*2; //Scale up to match with keyboard control sensibility.
			model.setRotation(rot*Time.deltaTime);
			if(!rot.Equals(Vector3.zero)){
				return true;
			}else{
				return false;
			}
		}
		private float trim(float val,float trim){
			if(Math.Abs(val)>=Math.Abs(trim)){
				return Math.Sign(val) * ( Math.Abs(val)-Math.Abs(trim) );
			}else{
				return 0f;
			}
		}
		
	}
}

