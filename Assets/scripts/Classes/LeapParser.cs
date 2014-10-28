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
	public class LeapParser {
		private Controller controller;
		private Models models;
		public LeapParser (Models models){
			this.models = models;
			controller = new Controller();
			//Enabling gestures
			controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
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
			Vector normal = hand.PalmNormal;
			Vector origin = hand.PalmPosition;


			return true;
		}
	}
}

