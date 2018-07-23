using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Colorful;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Playables;

[ExecuteInEditMode]

//[CustomEditor(typeof(SceneControl))]
public class SequencerGUI : EditorWindow {

	int editCue = 1;
	int builtCues = 1;

    GameObject Playable;
    PlayableDirector timeline;

	SceneControl mySceneControl = null;
	DanceControl myDanceControl = null;
	//camera parameters

	KeyCode ExecuteKey;
	float cueTime;
	float playbackTime;

	
	SceneControl.CameraMovement camMoves;
	SceneControl.CameraEffects camEffects;

	//DanceControl.Themes theme;
	//DanceControl.MotionClip clip;
	DanceControl.BodyMats bodyTex;
	SceneControl.FloorTex floorTex;

	float audioFadeTime; //audioFadeOutTime
	float volume; //musicVolume

	float clipSpeed;
	float clipOffset;
			
	float camMoveTime;
	float camMoveAmount;
	float camVerticalMoveAmount;
	bool cameraFollowing;

	Vector3 cameraPosition;
    Vector3 light1Position;
    Vector3 light2Position;
    Vector3 light3Position;

    float lightMoveTime;
	float lightFadeTime;
	float lightVerticalMoveAmount;
	Color light1Color;
	Color light2Color;
	Color light3Color;

	bool motionGroup;
	bool cameraGroup;
	bool bodyTexturesGroup;
	bool floorTexturesGroup;
	bool groupEnabled;

	private static Texture playBtn;
	private static Texture stopBtn;
	private static Texture powerBtn;
	private static Texture jumpBtn;
	
	private GameObject dirtyObject;
	public GameObject[] allDirty;

	//public bool inPlayMode;
    private bool showCameraControls;
    private bool showLightControls;
    private bool showLightOne;
    private bool showLightTwo;
    private bool showLightThree;
    private bool showVisualEffects;
    private bool showAudioControls;


    [MenuItem ("Window/Cue Panel")]

	    public static void  ShowWindow () {
        	EditorWindow.GetWindow(typeof(SequencerGUI));
	}

	void OnEnable(){

		EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        Debug.Log("marking dirty");

		editCue = 1;
		playBtn = Resources.Load("playButton") as Texture;
		stopBtn = Resources.Load("stopButton") as Texture;
		powerBtn = Resources.Load("powerButton") as Texture;
		jumpBtn = Resources.Load("curvedArrow") as Texture;

			mySceneControl = GameObject.Find ("SCENE CONTROL").GetComponent<SceneControl> ();

	//	myDanceControl = GameObject.Find ("DANCE CONTROL").GetComponent<DanceControl> ();

        timeline = GameObject.FindWithTag("timeline").GetComponent<PlayableDirector>();

        LoadCueData();
	

	builtCues = mySceneControl.lastCueNumber;

	
	}


	void OnDisable(){

	}


void Start(){
	editCue = 1;
	LoadCueData();
}



void Update(){
		playbackTime = mySceneControl.audioControl.time;
		//playbackTime = timeline.time;

		if(playbackTime >= mySceneControl.cueTimes[mySceneControl.cueToRun]){

			editCue=mySceneControl.cueToRun;

			LoadCueData();
		}
}
	
	
	
	
	void OnGUI () {

			Repaint();


		if (mySceneControl == null) {
			mySceneControl = GameObject.Find ("SCENE CONTROL").GetComponent<SceneControl> ();
			ExecuteKey = mySceneControl.ExecuteKey;
		}

/*		if (myDanceControl == null) {
			myDanceControl = GameObject.Find ("DANCE CONTROL").GetComponent<DanceControl> ();
			ExecuteKey = myDanceControl.ExecuteKey;
		}*/

		//GUILayout.Label 
EditorGUILayout.TextField ("PLAYBACK", EditorStyles.boldLabel);
			GUILayout.BeginHorizontal("box");
 		

		if (GUILayout.Button(powerBtn)){
            mySceneControl.jumpCueNumber = editCue;
           mySceneControl.inPlayMode  = true;
			EditorApplication.isPlaying = true;
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            Debug.Log("marking dirty");
            //Playable = GameObject.FindWithTag("timeline");

        mySceneControl.timeline.initialTime = mySceneControl.cueTimes[mySceneControl.jumpCueNumber];
        }

		if (GUILayout.Button(playBtn)){

            

            if (mySceneControl.inPlayMode){

                //  timeline = GameObject.FindWithTag("timeline").GetComponent<PlayableDirector>();
                //  Playable.SetActive(true);
                //  timeline.enabled = true;
                //  timeline.time = mySceneControl.cueTimes[mySceneControl.jumpCueNumber];
                //timeline.initialTime = mySceneControl.cueTimes[mySceneControl.jumpCueNumber];
               // timeline.Play();
            
            mySceneControl.cueToRun = editCue;
		//	myDanceControl.cueToRun = editCue;
			mySceneControl.GetComponent<AudioSource>().time = mySceneControl.cueTimes[editCue];
               // mySceneControl.timeline.time = mySceneControl.cueTimes[mySceneControl.jumpCueNumber];
                mySceneControl.StartPlayback();
                mySceneControl.timeline.initialTime = mySceneControl.cueTimes[mySceneControl.jumpCueNumber];
                mySceneControl.lastCueNumber = builtCues;
		}
    

		}


		if (GUILayout.Button(stopBtn)){
			EditorApplication.isPlaying = false;
			mySceneControl.inPlayMode = false;
            
         //   timeline.enabled = true;
            timeline.Stop();
            Debug.Log("Stopped Play Mode");
          //  timeline = GameObject.FindWithTag("timeline").GetComponent<PlayableDirector>();
          //  timeline.enabled = true;
        }

		GUILayout.EndHorizontal();

		EditorGUILayout.Space();


	//	ExecuteKey = (KeyCode)EditorGUILayout.EnumPopup("Execute Key:", ExecuteKey, EditorStyles.boldLabel);
		EditorGUILayout.TextField ("CUES", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal("box");

		EditorGUILayout.BeginVertical("Button");


GUILayout.BeginHorizontal("box");
		EditorGUILayout.TextField ("Cue Number: " + editCue + " of " + builtCues, EditorStyles.boldLabel);
			GUILayout.TextField ("Current Playback Time: "+ Mathf.Round(mySceneControl.GetComponent<AudioSource>().time), EditorStyles.boldLabel);

GUILayout.EndHorizontal();

		cueTime = EditorGUILayout.FloatField ("Run Cue at Time: ", cueTime);


		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
 		GUILayout.EndHorizontal();


		GUILayout.BeginHorizontal("box");
			
		
		if (GUILayout.Button("Prev Cue")){
			if (editCue > 1){
			editCue--;
			LoadCueData();
		}
		}


		if (builtCues > editCue){
	    	if (GUILayout.Button("Next Cue")){
				editCue++;
				LoadCueData();
			}
		} 
		else {
				if (GUILayout.Button("At Last Cue")){
				//editCue++;
				//builtCues++;
				//LoadCueData();
			}
		}


		
    //    GUILayout.EndHorizontal();

    //    	EditorGUILayout.Space();

    //    GUILayout.BeginHorizontal("box");


	//	if (GUILayout.Button("Load Cue Data")) {
	//		LoadCueData();
	//	}

		if (GUILayout.Button("Save Cue")) {

		//	myDanceControl.Theme = theme;
			
		//	myDanceControl.bodyMaterials[editCue] = bodyTex;
		//	myDanceControl.MotionClips[editCue] = clip;
		//	myDanceControl.ClipSpeeds[editCue] = clipSpeed;
		//	myDanceControl.ClipOffsets[editCue] = clipOffset;
			

			mySceneControl.ExecuteKey = ExecuteKey;
			mySceneControl.cueTimes[editCue] = cueTime;
		//	myDanceControl.cueTimes[editCue] = cueTime;
			
			mySceneControl.cameraLoc[editCue] = mySceneControl.theCamera.transform.position;
			mySceneControl.cameraEffect [editCue] = camEffects;
			mySceneControl.cameraMoveTime[editCue] = camMoveTime;
			mySceneControl.camerafollowing[editCue] = cameraFollowing;

			mySceneControl.lightFadeTime[editCue] = lightFadeTime;
			mySceneControl.lightVerticalMoveAmount[editCue] = lightVerticalMoveAmount;
			mySceneControl.light1Loc[editCue] = mySceneControl.lights[0].transform.position;
			mySceneControl.light2Loc[editCue] = mySceneControl.lights[1].transform.position;
			mySceneControl.light3Loc[editCue] = mySceneControl.lights[2].transform.position;
			mySceneControl.light1Color[editCue] = light1Color;
			mySceneControl.light2Color[editCue] = light2Color;
			mySceneControl.light3Color[editCue] = light3Color;

				mySceneControl.lightLights[0].color = light1Color;
				mySceneControl.lightLights[1].color = light2Color;
				mySceneControl.lightLights[2].color = light3Color;

			mySceneControl.floorTexture[editCue] = floorTex;
			mySceneControl.musicVolume = volume;
			mySceneControl.audioFadeOutTime = audioFadeTime;
			mySceneControl.lastCueNumber = builtCues;
		}

			if (editCue <50){
			if (GUILayout.Button("Insert Cue")){
				List<Color> light1ColorArray = new List<Color>();
				List<Color> light2ColorArray = new List<Color>();
				List<Color> light3ColorArray = new List<Color>();
				List<Vector3> light1LocArray = new List<Vector3>();
				List<Vector3> light2LocArray = new List<Vector3>();
				List<Vector3> light3LocArray = new List<Vector3>();
				List<float> lightFadeTimeArray = new List<float>();
				List<float> clipSpeedArray = new List<float>();
				List<float> clipOffsetArray = new List<float>();
				List<bool> camFollowingArray = new List<bool>();
				List<SceneControl.CameraEffects> camEffectsArray = new List<SceneControl.CameraEffects>();
				List<SceneControl.FloorTex> floorTexArray = new List<SceneControl.FloorTex>();
				List<DanceControl.BodyMats> BodyTexArray = new List<DanceControl.BodyMats>();
				List<DanceControl.MotionClip> clipArray = new List<DanceControl.MotionClip>();


				List<float> camMoveTimeArray = new List<float>();
				List<float> camMoveAmountArray = new List<float>();
				List<float> cueTimeArray = new List<float>();

			for (int x = 0; x < 50-editCue; x++) {
				light1ColorArray.Add(mySceneControl.light1Color[editCue + x]);
				light2ColorArray.Add(mySceneControl.light2Color[editCue + x]);
				light3ColorArray.Add(mySceneControl.light3Color[editCue + x]);
				light1LocArray.Add(mySceneControl.light1Loc[editCue + x]);
				light2LocArray.Add(mySceneControl.light2Loc[editCue + x]);
				light3LocArray.Add(mySceneControl.light3Loc[editCue + x]);
				lightFadeTimeArray.Add(mySceneControl.lightFadeTime[editCue + x]);
				camFollowingArray.Add(mySceneControl.camerafollowing[editCue + x]);
				camEffectsArray.Add(mySceneControl.cameraEffect[editCue + x]);
				floorTexArray.Add(mySceneControl.floorTexture[editCue + x]);
				/*BodyTexArray.Add(myDanceControl.bodyMaterials[editCue + x]);
				clipArray.Add(myDanceControl.MotionClips[editCue + x]);
				clipSpeedArray.Add(myDanceControl.ClipSpeeds[editCue + x]);
				clipOffsetArray.Add(myDanceControl.ClipOffsets[editCue + x]);*/

				camMoveTimeArray.Add(mySceneControl.cameraMoveTime[editCue + x]);
				camMoveAmountArray.Add(mySceneControl.cameraMoveAmount[editCue + x]);
				cueTimeArray.Add(mySceneControl.cueTimes[editCue + x]);
			}

			for (int y = 0; y<50 - (editCue+1); y++) {
				mySceneControl.light1Color[editCue + y + 1] = light1ColorArray[y]; 
				mySceneControl.light2Color[editCue + y + 1] = light2ColorArray[y]; 
				mySceneControl.light3Color[editCue + y + 1] = light3ColorArray[y]; 	

				mySceneControl.light1Loc[editCue + y + 1] = light1LocArray[y]; 
				mySceneControl.light2Loc[editCue + y + 1] = light2LocArray[y]; 
				mySceneControl.light3Loc[editCue + y + 1] = light3LocArray[y]; 
				mySceneControl.lightFadeTime[editCue + y + 1] = lightFadeTimeArray[y]; 
				mySceneControl.camerafollowing[editCue + y + 1] = camFollowingArray[y]; 
				mySceneControl.cameraEffect[editCue + y + 1] = camEffectsArray[y]; 
				mySceneControl.floorTexture[editCue + y + 1] = floorTexArray[y]; 
			/*	myDanceControl.bodyMaterials[editCue + y + 1] = BodyTexArray[y]; 
				myDanceControl.MotionClips[editCue + y + 1] = clipArray[y]; 
				myDanceControl.ClipSpeeds[editCue + y + 1] = clipSpeedArray[y]; 
				myDanceControl.ClipOffsets[editCue + y + 1] = clipOffsetArray[y]; */
				mySceneControl.cueTimes[editCue + y + 1] = cueTimeArray[y]; 

				mySceneControl.cameraMoveTime[editCue + y + 1] = camMoveTimeArray[y]; 
				mySceneControl.cameraMoveAmount[editCue + y + 1] = camMoveAmountArray[y]; 
			//	myDanceControl.cueTimes[editCue + y + 1] = cueTimeArray[y]; 
			} 
			editCue++;
			builtCues++;

		/*	myDanceControl.Theme = theme;
			myDanceControl.bodyMaterials[editCue] = bodyTex;
			myDanceControl.MotionClips[editCue] = clip;
			myDanceControl.ClipSpeeds[editCue] = clipSpeed;
			myDanceControl.ClipOffsets[editCue] = clipOffset;
				myDanceControl.cueTimes[editCue] = cueTime;*/
			mySceneControl.ExecuteKey = ExecuteKey;
			mySceneControl.cueTimes[editCue] = cueTime;
			
			mySceneControl.cameraLoc[editCue] = mySceneControl.theCamera.transform.position;
			mySceneControl.cameraEffect [editCue] = camEffects;
			mySceneControl.cameraMoveTime[editCue] = camMoveTime;
			mySceneControl.camerafollowing[editCue] = cameraFollowing;
			mySceneControl.lightFadeTime[editCue] = lightFadeTime;
			mySceneControl.lightVerticalMoveAmount[editCue] = lightVerticalMoveAmount;
			mySceneControl.light1Loc[editCue] = mySceneControl.lights[0].transform.position;
			mySceneControl.light2Loc[editCue] = mySceneControl.lights[1].transform.position;
			mySceneControl.light3Loc[editCue] = mySceneControl.lights[2].transform.position;
			mySceneControl.light1Color[editCue] = light1Color;
			mySceneControl.light2Color[editCue] = light2Color;
			mySceneControl.light3Color[editCue] = light3Color;
			mySceneControl.floorTexture[editCue] = floorTex;
			mySceneControl.musicVolume = volume;
			mySceneControl.audioFadeOutTime = audioFadeTime;
			mySceneControl.lastCueNumber = builtCues;
		}
	}
			if(builtCues>1){
		if (GUILayout.Button("Delete Cue")){
				List<Color> light1ColorArrayDel = new List<Color>();
				List<Color> light2ColorArrayDel = new List<Color>();
				List<Color> light3ColorArrayDel = new List<Color>();

				List<Vector3> light1LocArrayDel = new List<Vector3>();
				List<Vector3> light2LocArrayDel = new List<Vector3>();
				List<Vector3> light3LocArrayDel = new List<Vector3>();
				List<float> lightFadeTimeArrayDel = new List<float>();
				List<float> clipSpeedArrayDel = new List<float>();
				List<float> clipOffsetArrayDel = new List<float>();
				List<bool> camFollowingArrayDel = new List<bool>();
				List<SceneControl.CameraEffects> camEffectsArrayDel = new List<SceneControl.CameraEffects>();
				List<SceneControl.FloorTex> floorTexArrayDel = new List<SceneControl.FloorTex>();
				List<DanceControl.BodyMats> BodyTexArrayDel = new List<DanceControl.BodyMats>();
				List<DanceControl.MotionClip> clipArrayDel = new List<DanceControl.MotionClip>();

				List<float> camMoveTimeArrayDel = new List<float>();
				List<float> camMoveAmountArrayDel = new List<float>();
				List<float> cueTimeArrayDel = new List<float>();


			for (int x = 0; x < 50-editCue; x++) {
				light1ColorArrayDel.Add(mySceneControl.light1Color[editCue + x]);
				light2ColorArrayDel.Add(mySceneControl.light2Color[editCue + x]);
				light3ColorArrayDel.Add(mySceneControl.light3Color[editCue + x]);

				light1LocArrayDel.Add(mySceneControl.light1Loc[editCue + x]);
				light2LocArrayDel.Add(mySceneControl.light2Loc[editCue + x]);
				light3LocArrayDel.Add(mySceneControl.light3Loc[editCue + x]);
				lightFadeTimeArrayDel.Add(mySceneControl.lightFadeTime[editCue + x]);
				camFollowingArrayDel.Add(mySceneControl.camerafollowing[editCue + x]);
				camEffectsArrayDel.Add(mySceneControl.cameraEffect[editCue + x]);
				floorTexArrayDel.Add(mySceneControl.floorTexture[editCue + x]);
		/*		BodyTexArrayDel.Add(myDanceControl.bodyMaterials[editCue + x]);
				clipArrayDel.Add(myDanceControl.MotionClips[editCue + x]);
				clipSpeedArrayDel.Add(myDanceControl.ClipSpeeds[editCue + x]);
				clipOffsetArrayDel.Add(myDanceControl.ClipOffsets[editCue + x]);
*/
				camMoveTimeArrayDel.Add(mySceneControl.cameraMoveTime[editCue + x]);
				camMoveAmountArrayDel.Add(mySceneControl.cameraMoveAmount[editCue + x]);
				cueTimeArrayDel.Add(mySceneControl.cueTimes[editCue + x]);
				//Debug.Log (camMoveTimeArray[x]);
			}

			for (int y = 0; y<50 - (editCue+1); y++) {
				mySceneControl.light1Color[editCue + y] = light1ColorArrayDel[y+1]; 
				mySceneControl.light2Color[editCue + y] = light2ColorArrayDel[y+1]; 
				mySceneControl.light3Color[editCue + y] = light3ColorArrayDel[y+1]; 

				mySceneControl.light1Loc[editCue +y] = light1LocArrayDel[y+1]; 
				mySceneControl.light2Loc[editCue +y] = light2LocArrayDel[y+1]; 
				mySceneControl.light3Loc[editCue +y] = light3LocArrayDel[y+1]; 
				mySceneControl.lightFadeTime[editCue +y] = lightFadeTimeArrayDel[y+1]; 
				mySceneControl.camerafollowing[editCue +y] = camFollowingArrayDel[y+1]; 
				mySceneControl.cameraEffect[editCue +y] = camEffectsArrayDel[y+1]; 
				mySceneControl.floorTexture[editCue +y] = floorTexArrayDel[y+1]; 
/*				myDanceControl.bodyMaterials[editCue +y] = BodyTexArrayDel[y+1]; 
				myDanceControl.MotionClips[editCue +y] = clipArrayDel[y+1]; 
				myDanceControl.ClipSpeeds[editCue +y] = clipSpeedArrayDel[y+1]; 
				myDanceControl.ClipOffsets[editCue +y] = clipOffsetArrayDel[y+1]; 
				myDanceControl.cueTimes[editCue + y] = cueTimeArrayDel[y+1]; */
				mySceneControl.cueTimes[editCue +y] = cueTimeArrayDel[y+1];

				mySceneControl.cameraMoveTime[editCue + y] = camMoveTimeArrayDel[y+1]; 
				mySceneControl.cameraMoveAmount[editCue + y] = camMoveAmountArrayDel[y+1]; 
				mySceneControl.cueTimes[editCue + y] = cueTimeArrayDel[y+1]; 
				

				//Debug.Log (camMoveTimeArrayDel[y+1]);
				//Debug.Log (y);
			} 

			builtCues--;
			mySceneControl.lastCueNumber=builtCues;
			if (builtCues < editCue){
				editCue--;
			}
			
			LoadCueData();
		}
	}

			else{
					if (GUILayout.Button("No Cue")){
	
		}
	}


        GUILayout.EndHorizontal();

	EditorGUILayout.Space();

        showCameraControls = EditorGUILayout.Foldout(showCameraControls, "CAMERA");//, EditorStyles.boldLabel);

        if (showCameraControls)
        {
            EditorGUILayout.BeginVertical("Button");
            GUILayout.BeginHorizontal("box");
            EditorGUILayout.PrefixLabel("Camera Move Time");
            camMoveTime = EditorGUILayout.FloatField(camMoveTime);
            //  cameraFollowing = EditorGUILayout.Toggle("Camera Following", cameraFollowing);
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();




            if (GUILayout.Button("Move Up"))
            {
                cameraPosition = mySceneControl.theCamera.transform.position;
                mySceneControl.theCamera.transform.Translate(Vector3.up * 0.25f, Space.Self);
            }

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Move Left"))
            {
                cameraPosition = mySceneControl.theCamera.transform.position;
                mySceneControl.theCamera.transform.Translate(Vector3.right * -0.5f, Space.Self);
            }

            if (GUILayout.Button("Move Right"))
            {
                cameraPosition = mySceneControl.theCamera.transform.position;
                mySceneControl.theCamera.transform.Translate(Vector3.right * 0.5f, Space.Self);
            }

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("Move In"))
            {
                cameraPosition = mySceneControl.theCamera.transform.position;
                mySceneControl.theCamera.transform.Translate(Vector3.forward * 0.5f, Space.Self);
            }
            if (GUILayout.Button("Move Out"))
            {
                cameraPosition = mySceneControl.theCamera.transform.position;
                mySceneControl.theCamera.transform.Translate(Vector3.forward * -0.5f, Space.Self);
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Move Down"))
            {
                cameraPosition = mySceneControl.theCamera.transform.position;
                mySceneControl.theCamera.transform.Translate(Vector3.up * -0.25f, Space.Self);
            }

            
        }


  /*      EditorGUILayout.TextField ("DANCE CONTROLS", EditorStyles.boldLabel);
	//cameraGroup = EditorGUILayout.BeginToggleGroup ("Camera Controls", cameraGroup);
	EditorGUILayout.BeginVertical("Button");

			theme = (DanceControl.Themes) EditorGUILayout.EnumPopup("Theme:", theme);
			clip = (DanceControl.MotionClip) EditorGUILayout.EnumPopup("Motion Clip:", clip);

			GUILayout.BeginHorizontal("box");
			EditorGUILayout.PrefixLabel("Clip Speed");
			clipSpeed = EditorGUILayout.FloatField(clipSpeed);
			EditorGUILayout.PrefixLabel("Clip Offset");
			clipOffset = EditorGUILayout.FloatField(clipOffset);
			GUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();*/

			EditorGUILayout.Space();


        //EditorGUILayout.TextField ("LIGHTING CONTROLS", EditorStyles.boldLabel);
        showLightControls = EditorGUILayout.Foldout(showLightControls, "LIGHTING");//, EditorStyles.boldLabel);

        if (showLightControls)
        {
            EditorGUILayout.BeginVertical("Button");
            //	GUILayout.BeginHorizontal("box");
            light1Color = EditorGUILayout.ColorField("Light #1 Color", light1Color);

            showLightOne = EditorGUILayout.Foldout(showLightOne, "Light #1 Position");//, EditorStyles.boldLabel);

            if (showLightOne)
            {
                if (GUILayout.Button("Move Up"))
                {
                    light1Position = mySceneControl.lightOne.transform.position;
                    mySceneControl.lightOne.transform.Translate(Vector3.up * 0.25f, Space.Self);
                }

                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Move Left"))
                {
                    light1Position = mySceneControl.theCamera.transform.position;
                    mySceneControl.lightOne.transform.Translate(Vector3.right * -0.5f, Space.Self);
                }

                if (GUILayout.Button("Move Right"))
                {
                    light1Position = mySceneControl.theCamera.transform.position;
                    mySceneControl.lightOne.transform.Translate(Vector3.right * 0.5f, Space.Self);
                }

                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal("box");

                if (GUILayout.Button("Move In"))
                {
                    light1Position = mySceneControl.theCamera.transform.position;
                    mySceneControl.lightOne.transform.Translate(Vector3.forward * 0.5f, Space.Self);
                }
                if (GUILayout.Button("Move Out"))
                {
                    light1Position = mySceneControl.theCamera.transform.position;
                    mySceneControl.lightOne.transform.Translate(Vector3.forward * -0.5f, Space.Self);
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Move Down"))
                {
                    light1Position = mySceneControl.theCamera.transform.position;
                    mySceneControl.lightOne.transform.Translate(Vector3.up * -0.25f, Space.Self);
                }
            }

                light2Color = EditorGUILayout.ColorField("Light #2 Color", light2Color);

            showLightTwo = EditorGUILayout.Foldout(showLightTwo, "Light #2 Position");//, EditorStyles.boldLabel);

            if (showLightTwo)
            {
                if (GUILayout.Button("Move Up"))
                {
                    light2Position = mySceneControl.lightTwo.transform.position;
                    mySceneControl.lightTwo.transform.Translate(Vector3.up * 0.25f, Space.Self);
                }

                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Move Left"))
                {
                    light2Position = mySceneControl.lightTwo.transform.position;
                    mySceneControl.lightTwo.transform.Translate(Vector3.right * -0.5f, Space.Self);
                }

                if (GUILayout.Button("Move Right"))
                {
                    light2Position = mySceneControl.lightTwo.transform.position;
                    mySceneControl.lightTwo.transform.Translate(Vector3.right * 0.5f, Space.Self);
                }

                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal("box");

                if (GUILayout.Button("Move In"))
                {
                    light2Position = mySceneControl.lightTwo.transform.position;
                    mySceneControl.lightTwo.transform.Translate(Vector3.forward * 0.5f, Space.Self);
                }
                if (GUILayout.Button("Move Out"))
                {
                    light2Position = mySceneControl.lightTwo.transform.position;
                    mySceneControl.lightTwo.transform.Translate(Vector3.forward * -0.5f, Space.Self);
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Move Down"))
                {
                    light2Position = mySceneControl.lightTwo.transform.position;
                    mySceneControl.lightTwo.transform.Translate(Vector3.up * -0.25f, Space.Self);
                }
            }

            light3Color = EditorGUILayout.ColorField("Light #3 Color", light3Color);

            showLightThree = EditorGUILayout.Foldout(showLightThree, "Light #3 Position");//, EditorStyles.boldLabel);

            if (showLightThree)
            {
                if (GUILayout.Button("Move Up"))
                {
                    light3Position = mySceneControl.lightThree.transform.position;
                    mySceneControl.lightThree.transform.Translate(Vector3.up * 0.25f, Space.Self);
                }

                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Move Left"))
                {
                    light3Position = mySceneControl.lightThree.transform.position;
                    mySceneControl.lightThree.transform.Translate(Vector3.right * -0.5f, Space.Self);
                }

                if (GUILayout.Button("Move Right"))
                {
                    light3Position = mySceneControl.lightThree.transform.position;
                    mySceneControl.lightThree.transform.Translate(Vector3.right * 0.5f, Space.Self);
                }

                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal("box");

                if (GUILayout.Button("Move In"))
                {
                    light3Position = mySceneControl.lightThree.transform.position;
                    mySceneControl.lightThree.transform.Translate(Vector3.forward * 0.5f, Space.Self);
                }
                if (GUILayout.Button("Move Out"))
                {
                    light3Position = mySceneControl.lightThree.transform.position;
                    mySceneControl.lightThree.transform.Translate(Vector3.forward * -0.5f, Space.Self);
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Move Down"))
                {
                    light3Position = mySceneControl.lightThree.transform.position;
                    mySceneControl.lightThree.transform.Translate(Vector3.up * -0.25f, Space.Self);
                }
            }


            lightFadeTime = EditorGUILayout.FloatField("Light Fade Time", lightFadeTime);
            //	GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }


         

EditorGUILayout.Space();


		//EditorGUILayout.TextField ("Visual Effects and Audio", EditorStyles.boldLabel);
        showVisualEffects = EditorGUILayout.Foldout(showVisualEffects, "VISUAL EFFECTS AND AUDIO");//, EditorStyles.boldLabel);

        if (showVisualEffects)
        {


            EditorGUILayout.BeginVertical("Button");
            camEffects = (SceneControl.CameraEffects)EditorGUILayout.EnumPopup("Camera Effects:", camEffects);

            bodyTex = (DanceControl.BodyMats)EditorGUILayout.EnumPopup("Body Texture:", bodyTex);
            floorTex = (SceneControl.FloorTex)EditorGUILayout.EnumPopup("Floor Texture:", floorTex);
            EditorGUILayout.EndVertical();

            //EditorGUILayout.TextField ("AUDIO CONTROLS", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Button");
            GUILayout.BeginHorizontal("box");
            volume = EditorGUILayout.FloatField("Audio Volume:", volume);
            audioFadeTime = EditorGUILayout.FloatField("Audio Fade Out Length:", audioFadeTime);
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();


        }
           }

void LoadCueData(){
	/*		theme = myDanceControl.Theme;
			bodyTex = myDanceControl.bodyMaterials[editCue];
			clip = myDanceControl.MotionClips[editCue];
			clipSpeed = myDanceControl.ClipSpeeds[editCue];
			clipOffset = myDanceControl.ClipOffsets[editCue];*/

			cueTime = mySceneControl.cueTimes[editCue];
     
			if(!mySceneControl.inPlayMode){
				Debug.Log("Loading Cue Data Not in Play Mode");
				mySceneControl.theCamera.transform.position = mySceneControl.cameraLoc[editCue];
				mySceneControl.lights[0].transform.position = mySceneControl.light1Loc[editCue];
				mySceneControl.lights[1].transform.position = mySceneControl.light2Loc[editCue];
				mySceneControl.lights[2].transform.position = mySceneControl.light3Loc[editCue];
				timeline.time = mySceneControl.cueTimes[editCue];
				mySceneControl.lightLights[0].color = mySceneControl.light1Color[editCue];
				mySceneControl.lightLights[1].color = mySceneControl.light2Color[editCue];
				mySceneControl.lightLights[2].color = mySceneControl.light3Color[editCue];
			}


			camEffects = mySceneControl.cameraEffect[editCue];
			camMoveTime = mySceneControl.cameraMoveTime[editCue];
			cameraFollowing = mySceneControl.camerafollowing[editCue];
			lightFadeTime = mySceneControl.lightFadeTime[editCue];
			lightVerticalMoveAmount = mySceneControl.lightVerticalMoveAmount[editCue];
			light1Color = mySceneControl.light1Color[editCue];
			light2Color = mySceneControl.light2Color[editCue];
			light3Color = mySceneControl.light3Color[editCue];
			volume = mySceneControl.musicVolume;
			audioFadeTime = mySceneControl.audioFadeOutTime;

			floorTex = mySceneControl.floorTexture[editCue];


			switch(mySceneControl.floorTexture[editCue]){
		 	  case SceneControl.FloorTex.Tiles: 
	    	 mySceneControl.floor.GetComponent<Renderer>().material = mySceneControl.floorTextures[0];
 			break;
 			 case SceneControl.FloorTex.Streaks: 
	    	 mySceneControl.floor.GetComponent<Renderer>().material = mySceneControl.floorTextures[1];
    	 	break; 
    		 case SceneControl.FloorTex.Circles: 
	    	 mySceneControl.floor.GetComponent<Renderer>().material = mySceneControl.floorTextures[2];
    	 	break; 
    		 case SceneControl.FloorTex.Glitch: 
	     	mySceneControl.floor.GetComponent<Renderer>().material = mySceneControl.floorTextures[3];
	    	 break;
	    	 default:
	       Debug.Log("NOTHING");
	        break;
			}

/*		 switch(myDanceControl.bodyMaterials[editCue])
		 {
	     case DanceControl.BodyMats.Glass: 
	    // print (mocapBody.name);
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[0];
    	 break; 
    	 case DanceControl.BodyMats.Brick: 
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[1];
    	 break; 
    	 case DanceControl.BodyMats.Circuits: 
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[2];
    	 break; 
    	 case DanceControl.BodyMats.Lines: 
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[3];
    	 break;
    	 case DanceControl.BodyMats.Metal: 
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[4];
    	 break;
    	 case DanceControl.BodyMats.Glitch: 
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[5];
    	 break;
    	 case DanceControl.BodyMats.White: 
	     myDanceControl.mocapBody.GetComponent<Renderer>().material = myDanceControl.mats[6];
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}*/

			switch(mySceneControl.cameraEffect[editCue]){
		 	 case SceneControl.CameraEffects.Bloom: 
		 	 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
		 		mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
 			break;

 			 case SceneControl.CameraEffects.Glitch: 
 			  	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 		mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 

 			case SceneControl.CameraEffects.WaveDistortion: 
 			 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 			
 			case SceneControl.CameraEffects.Kuwahara: 
 			 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 			
 			case SceneControl.CameraEffects.AnalogTV: 
 			 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 			
 			case SceneControl.CameraEffects.LoFi: 
 			 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = true;
 			break;

 			case SceneControl.CameraEffects.Blur: 
 			 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
 			break; 			

 			case SceneControl.CameraEffects.Pixelate: 
 			 	mySceneControl.theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Glitch>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	mySceneControl.theCamera.GetComponent<Pixelate>().enabled = true;
		 	 	mySceneControl.theCamera.GetComponent<LoFiPalette>().enabled = false;
 			break;

		 	 default:
		         Debug.Log("NOTHING");
		         break;
		 	}

		





		//	mySceneControl.floor.GetComponent<Renderer>().material = mySceneControl.floorTextures[0];


		//	mySceneControl.SetTextures(editCue);

			Repaint();
			
}

}
