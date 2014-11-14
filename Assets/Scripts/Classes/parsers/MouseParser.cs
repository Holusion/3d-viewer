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
		public bool update(){
			bool isActive =false;
			Model model = models.getCurrent();
			if(Input.GetMouseButton(0)){
				isActive = true;
				Vector3 newPos = Input.mousePosition;
				Vector3 diff = newPos-pos;
				diff.x = (float)Math.Floor(diff.x/30f)*30f;
				diff.y = (float)Math.Floor(diff.y/30f)*30f;
				if(Math.Abs(diff.y)>Math.Abs(diff.x) && Math.Abs(diff.x)<100){
					diff.x = 0;
				}else if(Math.Abs(diff.y) <100){
					diff.y= 0;
				}
				Vector3 res = new Vector3(diff.y,diff.x,0);
				model.setRotation(res);
			}else{
				pos = Input.mousePosition;
				model.setRotation(0,0,0);
			}
			if(Input.GetMouseButtonDown(1)){
				isActive = true;
				models.next();
			}
			return isActive;
		}
	}
}
