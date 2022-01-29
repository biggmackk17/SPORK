using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** sway the head up and down */
public class SwayUpDown : MonoBehaviour
{
	public Animator anim;

	public float upDownFrequency = 0.25f;
	public float swayMultiplier = 1.0f;

	public Vector2 swayRange = new Vector2(-0.5f, 1.0f);

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float upDown = Mathf.Lerp(swayRange.x, swayRange.y, Mathf.PerlinNoise(Time.time, 0.0f)) * swayMultiplier;
		anim.SetFloat("headUpDown", upDown);
	}
}
