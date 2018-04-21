using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Disparador : MonoBehaviour 
{
	public Transform player;
	public float nextFire;
    public float nextFire2;
    public float fireRate;
    public List<GameObject> proyectiles;
    public bool ispressedlaser;
    public bool ispressedMisil;
    public AudioSource disparoLaser;
    public AudioSource disparoMisil;

    public void PresionoLaser()
    {
		//como la accion de disparar se hacia por medio de botones del hud... solo para cambiar el valor de un bool
		//Se devio crear un void exclusivamente para esto, igualmente para presionomisil, soltolaser, y soltomisil.
        ispressedlaser = true;
    }

    public void PresionoMisil()
    {
        ispressedMisil = true;
    }

    public void SoltoLaser()
    {
        ispressedlaser = false;
    }

    public void SoltoMisil()
    {
        ispressedMisil = false;
    }


    private void Update()
	//este update se encargara de disparar (dependiendo de los booleanos) determinado prefab de proyectil desde la posicion del jugador. quien es el poseedor de este script.
    {
        if (ispressedlaser == true)
        {
            FireLaser();
        }

        if (ispressedMisil == true)
        {
            FireMisil();
        }
    }

	//tanto como el firelaser como el firemisil... tienen un if adentro que se encarga de contar un tiempo de enfriamiento... 
	//para que no se dispare a la velocidad de cada frame... y cada tipo de bala tiene un tiempo de enfriamiento diferente
    public void FireLaser ()
	{
		if (GameState.Instance.estaVolando == true) 
		{
            if (Time.time > nextFire)
            {
                nextFire = Time.time + (fireRate*2);
                Instantiate(proyectiles[0], player.position, Quaternion.identity);
                disparoLaser.Play();
            } 
		}
	}

    public void FireMisil()
    {
		if (GameState.Instance.estaVolando == true)
        {
            if (Time.time > nextFire2)
            {
               nextFire2 = Time.time + (fireRate/3);
               Instantiate(proyectiles[1], player.position, Quaternion.identity);
                disparoMisil.Play();
            }
        }
    }
}
