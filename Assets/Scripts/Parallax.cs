using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public Transform[] cityBlock;
	[SerializeField]
	private float velocidadCiudad;

    [SerializeField]
    private float cloudsSpeed;
    [SerializeField]
    private float wallsSpeed;
    public Material cloudsMaterial;
    public Material wallsMaterial;


    void Update () 
	{
		if (true)//GameManager.Instance.gameState == GameState.Flying) 
		{
			
			for (int i = 0; i < cityBlock.Length; i++) 
			{
				//despues de guardar todos los bloques de ciudad en un array de transforms... para cada uno de esos elementos se le aplicara un movimiento hacia adelante
				cityBlock [i].Translate (-Vector3.forward * velocidadCiudad * Time.deltaTime,Space.World);
			
				if (cityBlock [i].position.z < -17.999999f) 
				{
					//este if podria ejecutarse cuando la posicion de la ciudad valga 18.0, 18.1, 18,2 y hasta 18,3... y debido a esto se notaba aveces un espacio entre los bordes de los bloques de ciudades... 
					//o se notaba como se sobreponian aveces... mientras estas bajaban, tratando de simular que la nave avanzaba.
					//para arreglar esto... utilice un float f y le di un valor exacto... para que las ciudades mantengan siempre la distancia una de otra intacta ya esten en 18.0, 18.1, 18,2.
					Vector3 newPosicion = cityBlock [i].position;
					float f = newPosicion.z ;
					newPosicion.z = f + 36 ;
					cityBlock [i].position = newPosicion;
					cityBlock [i].Rotate(0,90f,0);
				}
			}
		}



		//estos dos vectores se encargan de mover el sprite de la textura de los materiales cloudsmaterial y wallsmaterial.

		Vector2 newOffset = cloudsMaterial.mainTextureOffset;
		newOffset.y += cloudsSpeed * Time.deltaTime;
		cloudsMaterial.mainTextureOffset = newOffset;

        Vector2 wallOffset = wallsMaterial.mainTextureOffset;
        wallOffset.y += wallsSpeed * Time.deltaTime;
        wallsMaterial.mainTextureOffset = wallOffset;
    }


}
