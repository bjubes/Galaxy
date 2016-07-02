using UnityEngine;
using System.Collections;

public class FollowUV : MonoBehaviour {

	public float parralax = 0.25f;

	void Update () {

		MeshRenderer mr = GetComponent<MeshRenderer>();

		Material mat = mr.material;

		Vector2 offset = mat.mainTextureOffset;

		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.y / transform.localScale.y / parralax;

		mat.mainTextureOffset = offset;


		//scale down based on ortho size
		Vector3 quadScale = transform.localScale;
		quadScale.y = Camera.main.orthographicSize * 2;
		quadScale.x = quadScale.y * Camera.main.aspect;
		transform.localScale = quadScale;
	}

}
