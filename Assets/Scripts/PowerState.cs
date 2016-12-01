using UnityEngine;
using System.Collections;

public abstract class PowerState {

	// A reference to the containing class.
	protected BlobPlayer player;

	public PowerState(BlobPlayer playerObj) {
		player = playerObj;
	}

	public abstract void Update();

//@author Nahan Young
//Note: the OnTriggerEnter is not the same as unity's, I mirrored it.
	public abstract void OnTriggerEnter(Collider other);

	public abstract void OnTriggerExit(Collider other);

}
