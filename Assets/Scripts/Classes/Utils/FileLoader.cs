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

namespace AssemblyCSharp.Utils
{
	public class FileLoader{
		private static UnityEngine.Object[] Objects;
		public FileLoader (){

		}
		public static UnityEngine.Object[] getObjects(){
			if(Objects ==null){
				Objects = Resources.LoadAll("Objects", typeof(GameObject));
			}
			return Objects;
		}
		/**
		 * @param filename : base config file name. Should not have an extension. For example config.json will be "config".
		 */
		public static TextAsset getConfig(string filename){
			return (TextAsset) Resources.Load (filename, typeof(TextAsset));
		}
		/**
		 * Defaults to "config", aka config.json
		 */
		public static TextAsset getConfig(){
			return getConfig ("config");
		}
	}
}
