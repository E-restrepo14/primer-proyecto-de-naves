using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoReina : MonoBehaviour 
{
	public float j = 0.1f;
	public float h = 0.07f;
	public float j2 = 1.1f;
	public float i = 1.0f;

	private void Update()
	{
        if (GameState.Instance.estaVolando == true)
        {
			//la nave reina solo se movera cuando el gamestate este en estado volando
            StartCoroutine("Moverse");
            if (gameObject.transform.position.z <= 3.0f)
            {
				// si la nave llega a avanzar hasta cierto punto... h que se encarga de cambiar la posicion en z del gameobject... valdra 0.
                h = 0f;
            }
        }
	}


	IEnumerator Moverse()
	{
		transform.Translate (j2, 0, h);
		yield return new WaitForSeconds (0.0f);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Wall")) 
		{
			j = (-j);
			//j2 es el valor de j multiplicandose por un random range... lo que cada vez variará su velocidad de desplazamiento.
			j2 = j * Random.Range (0.5f,2.0f);
		}
		if (other.gameObject.CompareTag ("Player")) 
		{
			// si el jugador choca con la nave reina... esta le quitara dos vidas y a diferencia de otras no se destruirá por el choque.
			GameManager.Instance.Explotar (other.gameObject);
			GameManager.Instance.Inmunizar();
			GameManager.Instance.playerLife -= 2;
		}
	}
}

