using UnityEngine;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

	[System.Serializable]
	public class DialogueSystem : Action
	{
		
		// Declare properties here
		public override ActionCategory Category { get { return ActionCategory.Custom; }}
		public override string Title { get { return "DialogueSystem"; }}
		public override string Description { get { return ""; }}


		// Declare variables here

		public GameObject objectToAffect;
		public string textToPrint;
		public int constantID;
		public int parameterID = -1;


		public override float Run ()
		{
			/* 
			 * This function is called when the action is performed.
			 * 
			 * The float to return is the time that the game
			 * should wait before moving on to the next action.
			 * Return 0f to make the action instantenous.
			 * 
			 * For actions that take longer than one frame,
			 * you can return "defaultPauseTime" to make the game
			 * re-run this function a short time later. You can
			 * use the isRunning boolean to check if the action is
			 * being run for the first time, eg: 
			 */
			
			if (!isRunning)
			{
				isRunning = true;
				objectToAffect.GetComponent<TextMeshProUGUI>().text = textToPrint;

				return defaultPauseTime;
			}
			else
			{
				isRunning = false;
				return 0f;
			}
		}

		public override void Skip ()
		{
			/*
			 * This function is called when the Action is skipped, as a
			 * result of the player invoking the "EndCutscene" input.
			 * 
			 * It should perform the instructions of the Action instantly -
			 * regardless of whether or not the Action itself has been run
			 * normally yet.  If this method is left blank, then skipping
			 * the Action will have no effect.  If this method is removed,
			 * or if the Run() method call is left below, then skipping the
			 * Action will cause it to run itself as normal.
			 */

			 Run ();
		}

        public override void AssignValues(List<ActionParameter> parameters)
        {
			objectToAffect = AssignFile(parameters, parameterID, constantID, objectToAffect);
		}


#if UNITY_EDITOR

        public override void ShowGUI (List<ActionParameter> parameters)
		{
			// Action-specific Inspector GUI code here
			parameterID = Action.ChooseParameterGUI("GameOject to affect: ", parameters, parameterID, ParameterType.GameObject);
			if( parameterID >= 0)
            {
				constantID = 0;
				objectToAffect = null;
            }
            else
            {
				objectToAffect = (GameObject)EditorGUILayout.ObjectField("TMPROUI to affect: ", objectToAffect, typeof(GameObject), true);
				textToPrint = (string)EditorGUILayout.TextField("Text to print: ", textToPrint);

				constantID = FieldToID(objectToAffect, constantID);
				objectToAffect = IDToField(objectToAffect, constantID, true);
			}
		}
		

		public override string SetLabel ()
		{
			// (Optional) Return a string used to describe the specific action's job.
			
			return string.Empty;
		}

		#endif
		
	}

}