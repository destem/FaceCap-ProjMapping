using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeFaceController : MonoBehaviour 
{
	public int playerIndex;
	public float smoothFactor = 10f;

	private SkinnedMeshRenderer skinnedMeshRenderer;
	private Mesh skinnedMesh;

	private int blendShapeCount;
	private string[] blendShapeNames;
	private float[] blendShapeValues;

	private KinectManager kinectManager;
	private FacetrackingManager faceManager;
	private Dictionary<KinectInterop.FaceShapeAnimations, float> dictAnimUnits = new Dictionary<KinectInterop.FaceShapeAnimations, float>();

	[Header("Gain factors")]
	public float jawDownMult;
	public float jawLeftMult;
	public float jawRightMult;
	public float cheekLeftMult;
	public float cheekRightMult;
	public float browDownLeftMult;
	public float browUpLeftMult;
	public float browDownRightMult;
	public float browUpRightMult;
	public float smileLeftMult;
	public float smileRightMult;
	public float blinkLeftMult;
	public float blinkRightMult;
	public float midmouthMult;
	public float depressorLeftMult;
	public float depressorRightMult;
	public float stretcherLeftMult;
	public float stretcherRightMult;
	public float frownLeftMult;
	public float frownRightMult;


	private static readonly Dictionary<string, KinectInterop.FaceShapeAnimations> blendShape2AnimUnit = new Dictionary<string, KinectInterop.FaceShapeAnimations>
	{
		{"BrowsDown_Right", KinectInterop.FaceShapeAnimations.RighteyebrowLowerer},  // 0
		{"CheekPuff_Left", KinectInterop.FaceShapeAnimations.LeftcheekPuff},  // 1
		{"Jaw_Left", KinectInterop.FaceShapeAnimations.JawSlideRight}, // 2
		{"Jaw_Down", KinectInterop.FaceShapeAnimations.JawOpen},  // 3
		{"Smile_Right", KinectInterop.FaceShapeAnimations.LipCornerPullerRight},  // 4

		{"BrowsUp_Right", KinectInterop.FaceShapeAnimations.RighteyebrowLowerer},  // 5
		{"Jaw_Right", KinectInterop.FaceShapeAnimations.JawSlideRight},  // 6
		{"Blink_Right", KinectInterop.FaceShapeAnimations.RighteyeClosed},  // 7
		{"Midmouth_Left", KinectInterop.FaceShapeAnimations.LipPucker},  // 8 ???
		{"Midmouth_Right", KinectInterop.FaceShapeAnimations.LipPucker},  // 8 ???

		{"LowerLipDown_Right", KinectInterop.FaceShapeAnimations.LowerlipDepressorRight},  // 9

		{"BrowsUp_Left", KinectInterop.FaceShapeAnimations.LefteyebrowLowerer},  // 10
		{"Smile_Left", KinectInterop.FaceShapeAnimations.LipCornerPullerLeft},  // 11
		{"CheekPuff_Right", KinectInterop.FaceShapeAnimations.RightcheekPuff},  // 12
		{"BrowsDown_Left", KinectInterop.FaceShapeAnimations.LefteyebrowLowerer},  // 13
		{"LipLowerDown_Left", KinectInterop.FaceShapeAnimations.LowerlipDepressorLeft},  // 14

		{"Blink_Left", KinectInterop.FaceShapeAnimations.LefteyeClosed},  // 15
		{"Lips_Stretch_L", KinectInterop.FaceShapeAnimations.LipStretcherLeft},  // 16
		{"Frown_Right", KinectInterop.FaceShapeAnimations.LipCornerDepressorRight},  // 17
		{"Frown_Left", KinectInterop.FaceShapeAnimations.LipCornerDepressorLeft},  // 18
		{"Lips_Stretch_R", KinectInterop.FaceShapeAnimations.LipStretcherRight},  // 19
	};

	private Dictionary<string, float> blendShape2Multiplier;



	void Awake()
	{
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
		skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
		blendShape2Multiplier = new Dictionary<string, float>
		{
			{"Jaw_Left", -1f * jawLeftMult}, 
			{"BrowsUp_Right", -1f * browUpRightMult},  
			{"BrowsUp_Left", -1f * browUpLeftMult},  
			{"Jaw_Down", jawDownMult},
			{"BrowsDown_Right", browDownRightMult},
			{"CheekPuff_Left", cheekLeftMult},
			{"CheekPuff_Right", cheekRightMult},
			{"Smile_Right", smileRightMult},
			{"Smile_Left", smileLeftMult},
			{"Jaw_Right", jawRightMult},
			{"Blink_Right", blinkRightMult},
			{"Blink_Left", blinkLeftMult},
			{"Midmouth_Left", midmouthMult},
			{"Midmouth_Right", midmouthMult},
			{"LowerLipDown_Right", depressorRightMult},
			{"LowerLipDown_Left", depressorLeftMult},
			{"Lips_Stretch_L", stretcherLeftMult},
			{"Lips_Stretch_R", stretcherRightMult},
			{"Frown_Right", frownRightMult},
			{"Frown_Left", frownLeftMult}
		};
	}


	void Start () 
	{
		// init blend shape names
		blendShapeCount = skinnedMesh.blendShapeCount;
		blendShapeNames = new string[blendShapeCount];
		blendShapeValues = new float[blendShapeCount];

		for (int i = 0; i < blendShapeCount; i++) 
		{
			blendShapeNames[i] = skinnedMesh.GetBlendShapeName(i);
		}

		// reference to KinectManager
		kinectManager = KinectManager.Instance;
	}
	
	void Update () 
	{
		blendShape2Multiplier ["Jaw_Left"] = -1f * jawLeftMult;
		blendShape2Multiplier ["BrowsUp_Right"] = -1f * browUpRightMult; 
		blendShape2Multiplier ["BrowsUp_Left"] = -1f * browUpLeftMult;  
		blendShape2Multiplier ["Jaw_Down"] = jawDownMult;
		blendShape2Multiplier ["BrowsDown_Right"] = browDownRightMult;
		blendShape2Multiplier ["CheekPuff_Left"] = cheekLeftMult;
		blendShape2Multiplier ["CheekPuff_Right"] = cheekRightMult;
		blendShape2Multiplier ["Smile_Right"] = smileRightMult;
		blendShape2Multiplier ["Smile_Left"] = smileLeftMult;
		blendShape2Multiplier ["Jaw_Right"] = jawRightMult;
		blendShape2Multiplier ["Blink_Right"] = blinkRightMult;
		blendShape2Multiplier ["Blink_Left"] = blinkLeftMult;
		blendShape2Multiplier ["Midmouth_Left"] = midmouthMult;
		blendShape2Multiplier ["Midmouth_Right"] = midmouthMult;
		blendShape2Multiplier ["LowerLipDown_Right"] = depressorRightMult;
		blendShape2Multiplier ["LowerLipDown_Left"] = depressorLeftMult;
		blendShape2Multiplier ["Lips_Stretch_L"] = stretcherLeftMult;
		blendShape2Multiplier ["Lips_Stretch_R"] = stretcherRightMult;
		blendShape2Multiplier ["Frown_Right"] = frownRightMult;
		blendShape2Multiplier ["Frown_Left"] = frownLeftMult;

		// reference to face manager
		if (!faceManager) 
		{
			faceManager = FacetrackingManager.Instance;
		}

		if (kinectManager && kinectManager.IsInitialized() && faceManager && faceManager.IsFaceTrackingInitialized()) 
		{
			// check for tracked user
			long userId = kinectManager.GetUserIdByIndex(playerIndex);

			if (userId != 0 && kinectManager.IsUserTracked(userId)) 
			{
				if (faceManager.GetUserAnimUnits(userId, ref dictAnimUnits)) 
				{
					// process the blend shapes -> anim units
					for (int i = 0; i < blendShapeCount; i++) 
					{
						if (blendShape2AnimUnit.ContainsKey (blendShapeNames [i])) {
							KinectInterop.FaceShapeAnimations faceAnim = blendShape2AnimUnit [blendShapeNames [i]];
							float animValue = dictAnimUnits [faceAnim];

							// check for multiplier
							float mul = 1f;
							if (blendShape2Multiplier.ContainsKey (blendShapeNames [i])) {
								mul = blendShape2Multiplier [blendShapeNames [i]];
							}

							if (animValue * mul < 0f) {
								animValue = 0f;
							}

							// lerp to the new value
							blendShapeValues [i] = Mathf.Lerp (blendShapeValues [i], animValue * mul * 100f, smoothFactor * Time.deltaTime);
							skinnedMeshRenderer.SetBlendShapeWeight (i, blendShapeValues [i]);

							//rotation
							Vector3 angles = kinectManager.GetJointOrientation (userId, (int)KinectInterop.JointType.Head, true).eulerAngles;
							transform.parent.parent.rotation = Quaternion.Euler(new Vector3(-angles.x, -angles.y, angles.z));
						}
					}
				}
			}
		}

	}


}
