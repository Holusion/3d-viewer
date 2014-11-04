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
	public class LeapParser3 {
		private Controller controller;
		private Models models;
		private Vector3 rot;
		private Smoother smoother;
		private Frame previousFrame;
		private float timer;


		//private Vector previousPos;
		public LeapParser3 (Models models){
			smoother = new Smoother();
			this.models = models;
			rot = new Vector3(0,0,0);
			timer = 0f;
			controller = new Controller();

			controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
			//Enabling gestures
			//controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
			
		}
		/**
		 * return true if leap is available and parsing suceed. False otherwise.
		 * Simply a	replacement, instead of using onFrame, thus syncing with app's pace, if called in update() method.
		 * */
		public bool update (){
			Frame frame;
			GestureList gestures;
			if(!controller.IsServiceConnected()){
				return false;
			}
			if(!controller.IsConnected){
				return false;
			}
			frame = controller.Frame();

			swipe (frame);
			bool ret = rotation(frame);
			//previousFrame = frame;
			return ret;
		}


		private bool swipe(Frame frame){
			GestureList gestures;

			if(timer>=0f){
				timer -= Time.deltaTime;
				return false;
			}else{
				gestures = frame.Gestures();
				foreach (Gesture gesture in gestures){
					Debug.Log("gesture");
					if(gesture.Duration >=200000 && timer<=0f){
						timer =1.5f;
						models.next();
						return true;
					}
				}
			}

			return false;
		}

		private bool rotation(Frame frame){
			Hand hand = frame.Hands.Frontmost;
			Model model = models.getCurrent();
			Vector velocity; //= hand.PalmVelocity;
			//We have at least progressed 1 frame
			if (hand.PinchStrength>=1f || hand.GrabStrength>=1f){
				smoother.Push(getRotation(hand));
			}else if(hand.PinchStrength >0.8f){
				//Might be a pinch ... or not. So just minimize effect.
				smoother.Push(getRotation(hand),0.2f);
			}else{
				smoother.Push(Vector3.zero,0.15f);
			}
			//Debug.Log(smoother.Movement);
			model.setRotation(smoother.Movement*Time.deltaTime);
			
			
			
			if(!smoother.Movement.Equals(Vector3.zero)){
				return true;
			}else{
				return false;
			}
		}		
		private Vector3 getRotation(Hand hand){
			Vector3 ret;
			if(hand.IsValid && previousFrame != null && previousFrame.Hand(hand.Id).IsValid && hand.TimeVisible >0.1f){
				float framePeriod, z;
				Vector velocity;
				framePeriod =  (float)((hand.Frame.Timestamp - previousFrame.Timestamp)/1000)/1000f;
				velocity = hand.Translation(previousFrame)*0.7f/framePeriod;
				z = hand.RotationAngle(previousFrame,Vector.Backward)*30f/framePeriod;
				if(!float.IsNaN(velocity.x)){ //TODO UGLY Hack
					ret = new Vector3(velocity.y,velocity.x,z);
				}else{
					ret = new Vector3(0,0,0);
				}
			}else{
				ret = new Vector3(0,0,0);
			}
			//this.previousPos = hand.StabilizedPalmPosition;
			this.previousFrame = hand.Frame;
			//Debug.Log(ret);

			return ret;
		}


	}
}

