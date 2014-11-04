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
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Model{

		private GameObject obj;
		private ConfNode conf;
		private Bounds bounds;
		private List<GameObject> attachedComponents;
		public Model (GameObject mesh){
			conf = new ConfNode(mesh.name);
			this.init (mesh);
		}
		public Model (GameObject mesh, ConfNode config){
			if(config != null){
				this.conf = config;
			}
			this.init (mesh);
		}
		public void init(GameObject mesh){
			if(this.conf ==null){
				this.conf = new ConfNode(mesh.name);
			}
			this.attachedComponents = new List<GameObject>();
			this.obj =  mesh;
			this.bounds = GetMeshHierarchyBounds(this.obj);
			this.scale();
			this.center ();
			if(this.conf !=null && this.conf.objects !=null){
				foreach(string name in this.conf.objects){
					GameObject target = GameObject.Find(name);
					if(target !=null){
						this.attachedComponents.Add(target);
					}
				}
			}
		}

		public Quaternion getRotation (){
			return this.obj.transform.rotation;
		}

		public void setRotation(float x,float y, float z){
			this.setRotation(new Vector3(x,y,z));
		}
		public void setRotation(Vector3 rot){
			Vector3 modifier = this.conf.getAxes();
			//Debug.Log(modifier);
			rot.x = rot.x*modifier.x;
			rot.y = rot.y*modifier.y;
			rot.z = rot.z*modifier.z;
			this.obj.transform.RotateAround(bounds.center,rot,rot.magnitude*100*Time.deltaTime);
			//this.obj.transform.LookAt(Vector3.zero);
			//this.obj.transform.Rotate(rot*Time.deltaTime);
		}
		public Vector3 scale(){
			Vector3 currentScale = this.obj.transform.localScale;
			Vector3 currentSize = bounds.size;
			Vector3 targetSize = new Vector3(1,1,1);
			Vector3 ratio = new Vector3(targetSize.x / currentSize.x, targetSize.y / currentSize.y, targetSize.z / currentSize.z);
			float minRatio = Math.Min(ratio.x, Math.Min(ratio.y, ratio.z));
			Vector3 newScale = (currentScale* minRatio);
			if(this.obj.transform){
				this.obj.transform.localScale = newScale;
				this.bounds = GetMeshHierarchyBounds(this.obj); //Recalculate bounds as they were updated
			}
			return newScale;
		}

		private Bounds GetMeshHierarchyBounds (GameObject go){
			var bounds = new Bounds (); // Variable non utilisée, mais elle doit etre instanciée -- TODO A vérifier
			if (go.renderer != null) {
				bounds = go.renderer.bounds;
				Debug.Log ("Found parent bounds: " + bounds);
			}
			foreach (var c in go.GetComponentsInChildren<MeshRenderer>()) {
				if (bounds.size == Vector3.zero) {
					bounds = c.bounds;
				} else {
					bounds.Encapsulate (c.bounds);
				}
			}
			return bounds;
		}

		public void center(){
			this.obj.transform.position= -bounds.center;
			this.bounds = GetMeshHierarchyBounds(obj); //must recalculate bounds
		}

		public void setActive(bool active){
			foreach(GameObject component in this.attachedComponents){
				component.SetActive(active);
			}
			this.obj.transform.rotation = Quaternion.identity;
			center ();
			this.obj.SetActive (active);
		}
	}
}
