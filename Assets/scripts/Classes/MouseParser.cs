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

namespace AssemblyCSharp
{
	public class MouseParser {
		private Vector3 pos;
		private Models models;
		public MouseParser (Models models){
			this.models = models;
			pos = Input.mousePosition;
		}
		public void update(){
			Model model = models.getCurrent();
			if(Input.GetMouseButton(0)){
				Vector3 newPos = Input.mousePosition;
				model.setRotation(newPos.x-pos.x,newPos.y-pos.y,newPos.z-pos.z);
			}else{
				pos = Input.mousePosition;
				model.setRotation(0,0,0);
			}
			if(Input.GetMouseButtonDown(1)){
				models.next();
			}
		}
	}
}

