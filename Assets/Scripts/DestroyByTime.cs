using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyByTime : MonoBehaviour 
{
	public float lifetime = 5;
	void Start () 
	{
		Destroy(gameObject,lifetime);
	}
	// este script se creó para eliminar las particulas que quedan en la escena despues de utilizarse, para evitar cargar la escena.

}
