using UnityEngine;
using System.Collections;

//@author Nathan Young
public class WaterPower : MonoBehaviour {

	public GameObject waterParticleSystem;
	private ParticleSystem particleSystem;

	//Also see particle system setting: start lifetime
	public const float TIME_ACTIVE = 1f;

	//http://answers.unity3d.com/questions/225213/c-countdown-timer.html
	private float timeLeft; //time left for power on
	private bool canUseWater = true;
	private bool waterIsActive;


	//TODO: Create a particle system in front of the blob instead of always using the same one?
	//This will allow multiple water particle systems to exist

	// Use this for initialization
	void Start () {
		waterParticleSystem = GameObject.Find ("WaterParticleSystem");
		particleSystem = waterParticleSystem.GetComponent<ParticleSystem> ();

		waterParticleSystem.SetActive (true);
		particleSystem.Play ();

	}
	
	// Update is called once per frame
	void Update () {
	
		//if button pushed and blob in a state where it can use water
		if (Input.GetButton("WaterPower") && canUseWater) {

			timeLeft = TIME_ACTIVE;

			//if the water particle system is not already on, turn it on
			if (!particleSystem.isPlaying) {
				particleSystem.Play();
				print ("ok");
			}
		} else{
			timeLeft -= Time.deltaTime;
			//if player hasn't used water in a while it stops being active
			if(timeLeft<0 && particleSystem.isPlaying){
				particleSystem.Stop (); //  gameObject.GetComponent<ParticleSystem>().enableEmission = false; maybe use this, but its deprecated
										// above source is from http://answers.unity3d.com/questions/37875/turning-the-particle-system-on-and-off.html
			}


		}

	}
}
