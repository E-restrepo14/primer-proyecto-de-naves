using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;
	public GameObject player;
	public GameState gaState;
	public GameObject shield;

	public Vector3 limitesGamemanager;
	public GameObject explosionParticle;
	public GameObject miniexplosionParticle;

	public float waveWait;
	public float h = 0f;
	public float j= 0.5f;
	public float tiempoInmunizado= 3.0f;
	public List <GameObject> enemiesPrefabs;

	public int playerLife = 3;
	public int numWave=0;

	public Image life1Sprite;
	public Image life2Sprite;
	public Image life3Sprite;
    public Image enemylifebar;
	public float bossLife = 100f;
    public Image welldone;
    public Image gameover;


	void Awake()
	{
		//este script al ser un singleton... no podia hacer un llamado a otro singleton. porlo que se tuvo que
		//acceder al game state de esta forma
		gaState = GetComponent <GameState>();


        Instance = this;
		gaState.estaVolando = true;
		StartCoroutine (iniciarOleada1 ());
	}

	void Update ()
	{
		//en este update el juego verificara si el player puede ser dañado por enemigos y dependiendo de las vidas que pierda... ir desactivando los esprites de vidas del jugador en el hud
		if (gaState.estaVolando == true) 
		{
			if (Time.fixedTime > tiempoInmunizado) 
			{
				player.tag = ("Player");
				shield.SetActive (false);
			}

			if (playerLife < 3) 
			{
				life3Sprite.enabled = false;
			}

			if (playerLife < 2) 
			{
				life2Sprite.enabled = false;
			}

			if (playerLife < 1) 
			{
				life1Sprite.enabled = false;
			}

			if (playerLife < 0) 
			{
				//si la vida del jugador llega a menos que 0. se ordena la coroutina de perder y explota al jugador.
				Explotar(player);
				GameState.Instance.estaVolando = false;
				GameState.Instance.estaMuerto = true;
				Destroy (player);
				StartCoroutine (Perdio());
			}
		}
	}

	//este es un metodo muy utilizado durante el juego.. y por eso esta adentro de el singleton.
	public void Explotar(GameObject nave)
	{
		Instantiate (explosionParticle,nave.transform.position,nave.transform.rotation);
	}

	//esta mini explosion se utiliza solo cuando se le dispara a la alien reina.
	public void Miniexplotar(GameObject nave)
	{
		Instantiate (miniexplosionParticle,nave.transform.position,nave.transform.rotation);
	}

	//este ontriggerexit es para eliminar las balas que se alejan mucho... para que no se acumulen en la escena
    private void OnTriggerExit(Collider other)
    {
		//en caso de que por algun error... la nave reina se instancie al otro lado de los muros y termine saliendose del collider y destruyendose de la escena.
		//es poco probable de que suceda... pero en caso de que esto pase... el jugador esperara eternamente nuevas oleadas de enemigos que nunca llegaran y tendran que reiniciar la aplicacion manualmente
		//por eso se creo este if.
		if (other.gameObject.CompareTag ("FinalEnemy")) 
		{
			StartCoroutine (Gano());
		}
        Destroy(other.gameObject);
    }

	public void Inmunizar ()
	{
		//este void solo se encarga de bolver invulnerable al player... el update se encarga de regresarlo a su estado original cuando se supere el tiempo de invencibilidad.
		player.tag = ("Untagged");
		shield.SetActive(true);
		tiempoInmunizado = Time.time + 3.0f;
	}

    public void bajarVidaEnemigo(GameObject reina)
    {
        Vector3 porcentajedevida = new Vector3(1f,(bossLife / 100f),1f);
		//este vector3 es el que controlara la escala de la barra de vida (enemylifebar) de la reina que se mostrara en el canvas.

        enemylifebar.transform.GetChild(0).gameObject.transform.localScale = porcentajedevida;

        if (enemylifebar.transform.GetChild(0).gameObject.transform.localScale.y <= 0f)
        {
			//cuando la vida de la reina llegue a 0. se destruirá la reina y se llamara a la coroutine Gano().
            GameState.Instance.estaVolando = false;
            Explotar(reina);
            Destroy(reina);
			StartCoroutine (Gano());
        }
    }

	//las coroutinas de gano y perdio... solo se encargaran de mostrar la imagen del game over o el well done esperar 4 segundos y reiniciar el nivel.
    IEnumerator Perdio()
    {
		gameover.enabled = true;
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene ("main");
    }
    IEnumerator Gano()
    {
		welldone.enabled = true;
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene ("main");
    }



    IEnumerator iniciarOleada1 ()
	{
		//recordemos que el numwave iniciara valiendo 0.
		while (numWave<3)
		{
			for (int i = 0; i < 30; i++) 
				//se procede a instanciar a todos los enemigos de la oleada con cierto tiempo de espera entre instanciacion e instanciacion. 
			{
				transform.position = new Vector3 ((Random.Range (-4f,4f)),2.45f,9.98f);
				Instantiate (enemiesPrefabs [Random.Range (0, 2)], transform.position, transform.rotation);
				yield return new WaitForSeconds (0.5f);
			}
			numWave++;
			yield return new WaitForSeconds (waveWait);
			//luego se aumenta en uno el numero de oleadas y se ordena esperar a la siguiente oleada.
		}

		if (numWave >= 3) 
			//cuando ya se llegue a la oleada 3... solo se ordenara instanciar al enemigo final y mostrar en el hud... la barra de vidas de este
		{
			Instantiate (enemiesPrefabs [2], transform.position, transform.rotation);
            enemylifebar.enabled = true;
            enemylifebar.transform.GetChild(0).gameObject.SetActive(true);
        }
	}
}
