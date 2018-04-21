using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour 
{
	public static GameState Instance;

	void Awake ()
	{
		Instance = this;
	}

	//ya que el game manager necesitaba verificar cual de estas tres variables estaba activada varias veces... podria complicarse mucho para otros programadores leer y entender el codigo.
	// por lo que se crearon en este singleton y asi cualquiera puede acceder y modificar estas variables ahora.

	public bool estaEnPausa;
	public bool estaVolando;
	public bool estaMuerto;

}
