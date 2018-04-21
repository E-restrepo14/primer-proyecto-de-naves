using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigoLateral : MonoBehaviour
{
    public float j = 0.1f;
	public float i = 1.0f;

	// este script es para que el enemigo empiece a moverse automaticamente hasta que sea destruido
    private void Update()
    {
        StartCoroutine("Moverse");
		//el proceso de movimiento lo hice dentro de una coroutina para que verifique en todo momento el valor de j... que decidira 
		//si mover al personaje hacia la izquierda o derecha
    }

	IEnumerator Moverse()
	{
		transform.Translate (j, 0, 0.07f);
		yield return new WaitForSeconds (0.0f);
	}

	//cuando los enemigos choquen contra una pared... entonces necesitaran moverse hacia el otro lado... para eso se cambia el valor 
	//de j a su opuesto con el simbolo negativo... siendo que si ya es negativo. por ley de simbolos ahora será positivo

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Wall")) 
		{
			j = (-j);
		}

		// y si llega a golpear al jugador, le dara la orden al game manager de realizar las siguientes acciones. y se destruirá el gameobject.
		if (other.gameObject.CompareTag ("Player")) 
		{
			GameManager.Instance.Explotar (other.gameObject);
			GameManager.Instance.Inmunizar();

			GameManager.Instance.playerLife--;

			Destroy (gameObject);
		}
	}
}
