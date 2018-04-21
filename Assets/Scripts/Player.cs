using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	Camera mCam;
	[SerializeField]
	private float movingSpeed;
	Rigidbody rgbdy;
    public float speed = 0.01f;
	public Vector3 limits;


	void Awake()
	{
		rgbdy = GetComponent<Rigidbody> ();
		mCam = Camera.main;
	}

	void OnTriggerEnter (Collider other)
	{
		//en caso de que la pantalla del celular sea mas grande que el escenario del juego... decidi colocar unos muros para que no se vea el 
		//espacio negro sobrante ne la pantalla. pero el jugador puede atravesarlos igualmente, para solucionar este problema cree este if.
		if (other.gameObject.CompareTag ("Wall")) 
		{
			rgbdy.velocity = Vector3.zero;
		}
	}

	void FixedUpdate()
	{
		//el jugador en escena en todo momento hara el llamado a los procesos de verificar posicion y aplicar el movimiento a su propio rigidbody.
		ApplyMovement();
		VerifyPos();
	}


	private void VerifyPos()
	{
		if (GameState.Instance.estaVolando == true) 
		{

			Vector3 p = rgbdy.position;
			float distanceFromCamera = (mCam.transform.position - p).y;


			limits = mCam.ViewportToWorldPoint (new Vector3 (1f, 1f, distanceFromCamera));
			limits.x -= transform.localScale.x/2;
			limits.z -= transform.localScale.z/2;
			GameManager.Instance.limitesGamemanager = limits;

			//en caso de que la posicion del jugador se salga de los limites puestos por el viewport... estos ifs se encargaran de detener al jugador.
			if (p.x < -limits.x) 
			{
				p.x = -limits.x;
				rgbdy.velocity = Vector3.zero;

			}
			if (p.x > limits.x)
			{
				p.x = limits.x;
				rgbdy.velocity = Vector3.zero;

			}
			if (p.z < -limits.z) 
			{
				p.z = -limits.z;
				rgbdy.velocity = Vector3.zero;

			}
			if (p.z > limits.z) 
			{
				p.z = limits.z;
				rgbdy.velocity = Vector3.zero;

			}
			rgbdy.position = p;
		}
	}

	//y por ultimo este dependiendo de desde donde se maneje, siendo desde un celular o desde el editor de unity... tiene unas instrucciones distintas. para cada caso.
	private void ApplyMovement()
	{
		if (GameState.Instance.estaVolando == true) 
		{
#if UNITY_EDITOR
			float hSpeed = Input.GetAxis("Horizontal") * movingSpeed;
			float fSpeed = Input.GetAxis("Vertical") * movingSpeed;
			rgbdy.AddForce(new Vector3(hSpeed, 0, fSpeed));

#elif UNITY_ANDROID || UNITY_IOS
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
#endif

        }
    }
}
