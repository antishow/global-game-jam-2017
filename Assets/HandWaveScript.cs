using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWaveScript : MonoBehaviour {

	public float blendOne = 0f;

	public float blendTwo = 0f;

	public float blendThree = 0f;

	public float blendSpeed = 1f;

	public bool oneBlendOut = true;

	public bool oneBlendIn = false;

	public bool blendOneRunning = false;



	public bool twoBlendOut = false;

	public bool twoBlendIn = false;

	public bool blendTwoRunning = false;



	public bool threeBlendOut = false;

	public bool threeBlendIn = false;

	public bool blendThreeRunning = false;





	SkinnedMeshRenderer skinnedMeshRenderer;

	Mesh skinnedMesh;

	// Use this for initialization

	void Start ()
	{

		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

		skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;

	}



	// Update is called once per frame

	void Update ()
	{

		if (blendOneRunning)
		{

			if (blendOne > 100)
			{

				oneBlendOut = false;

				oneBlendIn = true;

			}
			else if (blendOne <= 0)
			{

				oneBlendOut = true;

				oneBlendIn = false;

			}





			if (oneBlendOut && blendOne < 100)
			{

				blendOne += blendSpeed * Time.deltaTime;

				skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);

			}
			else if (oneBlendIn && blendOne > 0)
			{

				blendOne -= blendSpeed * Time.deltaTime;

				skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);

			}



			if (oneBlendIn && blendOne <= 0)
			{

				blendOne = 0;

				blendOneRunning = false;

				blendTwoRunning = true;

			}

		}



		if (blendTwoRunning)
		{

			if (blendTwo > 100)
			{

				twoBlendOut = false;

				twoBlendIn = true;

			}
			else if (blendTwo <= 0)
			{

				twoBlendOut = true;

				twoBlendIn = false;

			}



			if (twoBlendOut && blendTwo < 100)
			{

				blendTwo += blendSpeed * Time.deltaTime;

				skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);

			}
			else if (twoBlendIn && blendTwo > 0)
			{

				blendTwo -= blendSpeed * Time.deltaTime;

				skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);

			}



			if (twoBlendIn && blendTwo <= 0)
			{

				blendTwo = 0;

				blendTwoRunning = false;

				blendThreeRunning = true;

			}

		}



		if (blendThreeRunning)
		{

			if (blendThree > 100)
			{

				threeBlendOut = false;

				threeBlendIn = true;

			}
			else if (blendThree <= 0)
			{

				threeBlendOut = true;

				threeBlendIn = false;

			}



			if (threeBlendOut && blendThree < 100)
			{

				blendThree += blendSpeed * Time.deltaTime;

				skinnedMeshRenderer.SetBlendShapeWeight(2, blendThree);

			}
			else if (threeBlendIn && blendThree > 0)
			{

				blendThree -= blendSpeed * Time.deltaTime;

				skinnedMeshRenderer.SetBlendShapeWeight(2, blendThree);

			}



			if (threeBlendIn && blendThree <= 0)
			{

				blendThree = 0;

				blendThreeRunning = false;

				blendOneRunning = true;

			}

		}

	}
}
