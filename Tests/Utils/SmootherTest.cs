//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using NUnit.Framework;
using System;
using AssemblyCSharp.Utils;
namespace Tests
{
	[TestFixture()]
	public class SmootherTest{
		[Test()]
		public void TestAverage (){
			//Resulting value should be a ponderated average of previous ones.
			Smoother smoother = new Smoother();
		}
		[Test()]
		public void TestSmooth (){
			//When submitting a value, it should be smoothed toward the previous one.
		}
	}
}

