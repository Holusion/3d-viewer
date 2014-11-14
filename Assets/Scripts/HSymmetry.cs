using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class HSymmetry : MonoBehaviour {

	void OnPreCull () {
		Matrix4x4 scale = Matrix4x4.Scale (new Vector3 (-1, 1, 1));

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




