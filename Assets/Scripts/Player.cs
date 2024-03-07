using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    /*
    Los límites definidos con bound nos hacen falta debido a que el jugador se puede salir de la pantalla
    debido a que su rigidbody es quinemático, por lo que no se ve afectado por la gravedad ni puede colisionar
    con objetos estáticos.
    */
    [SerializeField] private float bound = 4.5f; // x axis bound 
    [SerializeField] private GameObject sprite;
    [SerializeField] private BoxCollider2D hitbox;
    private Vector3 initialSize;
    private Vector2 initialHitboxSize;
    private bool isBig = false;
    private float MAX_TIME_POWERUP = 10f;
    private float timePowerUp = 0f;

    private Vector2 startPos; // Posición inicial del jugador


    // Start is called before the first frame update
    void Start()
    {
        initialSize = sprite.transform.localScale;
        initialHitboxSize = hitbox.size;
        startPos = transform.position; // Guardamos la posición inicial del jugador
    }

    // Update is called once per frame
    void Update()
    {
       PlayerMovement();

       if (timePowerUp > 0) 
       {
            timePowerUp -= Time.deltaTime;
       } else
       {
            isBig = false;
            sprite.transform.localScale = initialSize;
            hitbox.size = initialHitboxSize;
       }
    }

    void PlayerMovement()
    {
         float moveInput = Input.GetAxisRaw("Horizontal");
        // Controlaríamo el movimiento de la siguiente forma de no ser el rigidbody quinemático
        // transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0f, 0f);

        Vector2 playerPosition = transform.position;
        // Mathf.Clamp nos permite limitar un valor entre un mínimo y un máximo
        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * speed * Time.deltaTime, -bound, bound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {
        transform.position = startPos; // Posición inicial del jugador
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            GameManager.Instance.AddLife(); // Añadimos una vida
        } else if (collision.CompareTag("powerUpLoseLife")) 
        {
            Destroy(collision.gameObject);
            GameManager.Instance.LoseLifePowerUp();
        } else if (collision.CompareTag("powerUpBig")) 
        {
            Destroy(collision.gameObject);

            if (!isBig) {
                isBig = true;
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x * 2, sprite.transform.localScale.y, sprite.transform.localScale.z);
                hitbox.size = new Vector2(hitbox.size.x * 2, hitbox.size.y);
            }

            timePowerUp = MAX_TIME_POWERUP;
        }
    }
}
