using UnityEngine;
using System.Collections;


public class FireGun : MonoBehaviour {


	public GameObject bulletObject;
	public float fireRate;

	float nextFire = 0.0f;
	GameObject jetType;

	/// <summary>
	/// Find the jetType to which this gun belongs.
	/// </summary>
	void Start()
	{
		// Is this the player jet or enemy jet?
		jetType = transform.parent.parent.gameObject;

	}

	/// <summary>
	/// Check whether conditions to fire the gun are satisfied. If they are, consider firing.
	/// </summary>
	void FixedUpdate () {

		// If the gun that's about to fire belongs to the player, and the fire button is pressed...
		if (jetType.tag == "Player" && Input.GetButton ("Jump")) 
			ConsiderFiring ();
		else { 
			// If this gun doesn't belong to the player, it must belong to an enemy
			// Shoot a raycast out from the gun to determine whether its aim is near the player
			if (StandardRaycast.GetTagFromRaycast (transform.position, transform.forward, 100000) == "EnemyAI") 
				ConsiderFiring ();


		}


	}

	/// <summary>
	/// Check whether we have clock permission to fire. If so, fire and reset clock permission.
	/// </summary>
	void ConsiderFiring()
	{
		// If we're no longer waiting for clock permission to fire...
		if (Time.time > nextFire) {

			// Create a bullet and give it forward velocity
			GameObject newBullet = Instantiate (bulletObject, transform.position, transform.rotation) as GameObject;
			newBullet.GetComponent<Rigidbody> ().velocity = transform.forward * 500;

			// Reset clock permission to fire
			nextFire = Time.time + fireRate;

		} 
	}




}
