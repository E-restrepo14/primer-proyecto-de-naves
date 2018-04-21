using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VectorEnemy : MonoBehaviour 
{

	public float bulletSpeed;
	public Rigidbody rgbdy;



	void Start ()
	{
		// al momento de aparecer el enemigo. utilizando su rigidbody... se le da un impulso hacia su eje z... este es el mismo codigo para la bala, con las variables y todo
		//solo que solo funcionara contra un objetivo, y ese es el jugador.
		rgbdy.velocity = transform.forward* bulletSpeed;
	}

	void OnTriggerEnter(Collider other)	
	{


		if (other.gameObject.CompareTag ("Player")) 
		{
			GameManager.Instance.Explotar (other.gameObject);
			GameManager.Instance.Inmunizar() ;

			GameManager.Instance.playerLife--;
		
			Destroy (gameObject);
		}
	}
}