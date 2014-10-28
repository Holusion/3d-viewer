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
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AssemblyCSharp
{
	public class Models{
		private List<Model> list;
		private int index;
		public Models (GameObject def){
			index = 0;
			list = new List<Model>();
			UnityEngine.Object[] listObjects = Resources.LoadAll ("Objects", typeof(GameObject));
			foreach (GameObject listObject in listObjects) {
				GameObject obj = (GameObject) GameObject.Instantiate (listObject, Vector3.zero, Quaternion.identity);
				scale (obj,def.collider);
				list.Add (new Model(obj));
			}
			if( list.Count == 0){
				Debug.Log ("no objects found. Please add objects to your Resources/Objects folder");
				list.Add (new Model((GameObject) GameObject.Instantiate (def, Vector3.zero, Quaternion.identity)));
			}
			this.setCurrent(0);
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
		private Vector3 scale(GameObject obj,Collider root){
			Vector3 currentScale = obj.transform.localScale;
			Vector3 currentSize = GetMeshHierarchyBounds(obj).size;
			Vector3 targetSize = (root.bounds.size);
			Vector3 ratio = new Vector3(targetSize.x / currentSize.x, targetSize.y / currentSize.y, targetSize.z / currentSize.z);
			float minRatio = Math.Min(ratio.x, Math.Min(ratio.y, ratio.z));
			Vector3 newScale = (currentScale* minRatio);
			if(obj.transform){
				obj.transform.localScale = newScale;
				
			}
			return newScale;
		}
		public Model getCurrent(){
			return list[index];
		}
		public void next(){
			this.list[index].setActive(false);
			this.setCurrent(index+1);
		}
		public void setCurrent(int value){
			if(value <list.Count && value >0){
				this.index = value;
			}else{
				this.index = 0;
			}
			this.list[index].setActive(true);
		}

	}
}
