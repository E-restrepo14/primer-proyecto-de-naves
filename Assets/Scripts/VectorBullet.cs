using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VectorBullet : MonoBehaviour 
{


	public float bulletSpeed;
	public Rigidbody rgbdy;
	public GameObject chispazo;
					// la particula chispazo solo se instanciara cuando el laser golpee la reina. el resto de enemigos al expotar... no dejaran ver el efecto por la misma explosion.
	public float daño = 1f;   // este solo se aplicara a la vida de la reina
	public AudioSource impactoLaser;


	void Start ()
	{
		// al momento de aparecer la bala... utilizando su rigidbody... se le da un impulso hacia su eje z...
		rgbdy.velocity = transform.forward* bulletSpeed;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Enemy")) 
		{
			GameManager.Instance.Explotar (other.gameObject);
			if (gameObject.CompareTag ("Misil")) 
			{
				//la bala del misil al poder ser disparada mas rapido... no debe de tener la ventaja de atravesar y matar mas enemigos en su camino... por lo que se destruirá al impactar algun enemigo.
				Destroy (gameObject);
			}
			Destroy (other.gameObject);
		}

		//las instrucciones para la bala al impactar contra la reina son parecidas pero un tanto mas compicadas.
		if (other.gameObject.CompareTag ("FinalEnemy")) 
		{
			
			if (gameObject.CompareTag ("Laser")) 
			{
				impactoLaser.Play();
				Instantiate (chispazo,other.gameObject.transform.position,other.gameObject.transform.rotation);
			}
			if (gameObject.CompareTag ("Misil")) 
			{
				//si el proyectil es el misil... tendra que hacer menos daño porque este se dispara mas rapido que el laser.
				daño = 2.0f;					
				GameManager.Instance.Miniexplotar (gameObject);
				Destroy (gameObject);
			}
			GameManager.Instance.bossLife -= daño;
            GameManager.Instance.bajarVidaEnemigo( other.gameObject);
			
		}
	}
}