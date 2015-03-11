using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

[RequireComponent (typeof (Camera))]
public class HSymmetry : MonoBehaviour {
	private static string productName = null;
	private static string productSerial = null;
	private static Regex findCameraType = null;
	private string type = null;
	public HSymmetry(){
		Regex parseHostname = new Regex(@"(prisme|focus|opera)-([0-9]*)$", RegexOptions.IgnoreCase);
		if( productName == null ){
			Match m = parseHostname.Match(System.Net.Dns.GetHostName());
			if(m.Groups.Count == 3){ //Don't make defensive code here : just check for normal case
				productName = m.Groups[1].Captures[0].Value.ToLower();
				productSerial = m.Groups[2].Captures[0].Value.ToLower();
			}else{
				productName = "undefined";
				productSerial = "undefined";
			}
		}
		if(findCameraType == null){
			findCameraType = new Regex(@"(?:camera_)(front|right|left|back)$", RegexOptions.IgnoreCase);
		}
	}

	void Start () {
		Match m = findCameraType.Match(this.gameObject.name);
		if(m.Success && m.Groups.Count == 2 && m.Groups[1].Captures.Count == 1){
			this.type = m.Groups[1].Captures[0].Value.ToLower();
		}else{
			this.type = "undefined";
		}

		switch (productName) {
		case "prisme":

			break;
		case "focus":

			break;
		case "opera":

			break;
		default:
			Debug.LogWarning("hostname invalid or indetermined. Can't setup cameras");
			break;
		}

	}
	void OnPreCull () {
		Matrix4x4 scale;
		if(camera.aspect >2){
			scale = Matrix4x4.Scale (new Vector3 (-1, 1, 1));
		}else{
			 scale = Matrix4x4.Scale (new Vector3 (1, -1, 1));
		}
		camera.ResetWorldToCameraMatrix ();
		camera.ResetProjectionMatrix ();
		camera.projectionMatrix = camera.projectionMatrix * scale;
	}
	void OnPreRender () {
		GL.SetRevertBackfacing (true);
	}
	void OnPostRender () {
		GL.SetRevertBackfacing (false);
	}
}




