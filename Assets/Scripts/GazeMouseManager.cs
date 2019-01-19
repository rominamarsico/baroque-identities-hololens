using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeMouseManager : MonoBehaviour
{
	// Represents the hologram that is currently being gazed at.
	public GameObject FocusedObject { get; private set; }


	// Update is called once per frame
	void Update()
	{
		// Figure out which hologram is focused this frame.
		GameObject oldFocusObject = FocusedObject;

		// Do a raycast into the world based on the user's
		// head position and orientation.
		var gazeDirection = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;
		if (Physics.Raycast(gazeDirection, out hitInfo))
		{
			// If the raycast hit a hologram, use that as the focused object.
			FocusedObject = hitInfo.collider.gameObject;
		}
		else
		{
			// If the raycast did not hit a hologram, clear the focused object.
			FocusedObject = null;
		}

		if (FocusedObject != null) {
			if (Input.GetMouseButtonDown (0)) {
				FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
			}
		}

	}
}