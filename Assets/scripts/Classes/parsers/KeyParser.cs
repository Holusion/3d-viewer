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
	public class KeyParser {

		private Models models;
		public KeyParser (Models models){
			this.models = models;
			//pos = Input.mousePosition;
		}
		public bool update(){
			Model model = models.getCurrent();
			//Horizontal rotation
			if(Input.GetKey("q")){
				model.setRotation(0,1,0);
			}else if(Input.GetKey("d")){
				model.setRotation(0,-1,0);
			}
			//roll
			if(Input.GetKey("a")){
				model.setRotation(0,0,1);
			}else if(Input.GetKey("e")){
				model.setRotation(0,0,-1);
			}
			//vertical rotation
			if(Input.GetKey("z")){
				model.setRotation(1,0,0);
			}else if(Input.GetKey("s")){
				model.setRotation(-1,0,0);
			}
			//Switch
			if(Input.GetKeyDown("r")){
				models.next();
			}
			return true;
		}
	}
}

