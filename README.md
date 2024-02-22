# Proyecto RollABall + Animator

Como continuación del proyecto RollABall, se ha añadido un Animator para el jugador:

### 1. Se ha añadido un Enemy que persigue al jugador:

Para ello, se ha creado un script que permite al enemigo seguir al jugador, y si lo alcanza, el jugador pierde una vida.

```csharp
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder; // Reference to the navmesh agent
    private Transform target; // Reference to the player
	private PlayerController playerController; // Reference to the player controller
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
		playerController = target.GetComponent<PlayerController>();
    }
    void Update()
    {
		if (playerController.count >= 12) // Check if the player has collected all the pickups
	    {
		    pathfinder.isStopped = true; // Stop the enemy
		}
		else{
        	pathfinder.SetDestination(target.position);
		}
    }
}
```
![img.png](Images%2Fimg.png)

Cabe destacar, que el enemigo debe tener un NavMeshAgent para poder seguir al jugador (asignando a este el target "Humanoid" que corresponde al Player).

![img_1.png](Images%2Fimg_1.png)

Ademas, en la opción de Bake, se debe seleccionar el suelo para que el enemigo pueda moverse por el.

>[!WARNING]
> Debes tener instalado el paquete AI Navigation para acceder a estas opciones.

### 2. Se ha añadido un Animator para el jugador:

Para ello, en el script del jugador se ha añadido un Animator que permite controlar los parametros de su animación.

```csharp
    ...

    private Animator anim; // Reference to the animator
    void Start()
    {
        ...
        anim = GetComponent<Animator>();
        ...
    }
    ...
		anim.SetBool("isEating", true); // Set the isJumping parameter to true
    ...
   ```
    
![img_2.png](Images%2Fimg_2.png)

Para entrar en detalle a la animación, se ha creado un Animator Controller que permite controlar las animaciones del jugador.

Ahora bien, a la hora de poder hacer nuestra propia animación, nos encontramos con la siguiente ventana (accediendo a ella eligiendo nuestro objeto -> Window -> Animation -> Animation):

![img_3.png](Images%2Fimg_3.png)

Como se puede observar, en esta no sólo se puede controlar la animación, sino que también se puede controlar el movimiento del objeto, la rotación, la escala, etc. Además de cuan larga será la animación, si se repetirá, etc.

---

## APARTADO EXAMEN:

### 1. Animación al saltar:

Para ello, se ha creado una animación que permite al jugador saltar. Para ello, se ha creado otro clip que permite controlar la escala del jugador y estirarlo:

![img_4.png](Images%2Fimg_4.png)

![img_5.png](Images%2Fimg_5.png)

A la hora de modificar los valores de su escala X, podemos expandir la forma del Player.

Ahora, vamos a los estados:

![img_6.png](Images%2Fimg_6.png)

En nuestra pestaña animator, enlazamos por medio de transiciones nuestro estado expandir. Condicionando a estas por medio del parametro booleano "isJumping". De esta manera, con ayuda de nuestro Script Player, podemos manejar el cambio de estado:

![img_7.png](Images%2Fimg_7.png)

>[!NOTE]
> Las transiciones se pueden condicionar por medio de parametros booleanos como bien hemos visto. Pero sus valores deben cambiar (siendo de True a False) para poder hacer el cambio de estado.

![img_8.png](Images%2Fimg_8.png)

Adicionalmente, cambiamos el Loop Time a False, para que la animación suceda una sola vez.

Con respecto al Script, realizamos la siguiente modificación:

```csharp
        private void FixedUpdate()
    {
        Vector3 movement=new Vector3(movementX,0.0f,movementY); // Create a vector with the movement
        rb.AddForce(movement*speed); // Apply the force to the rigidbody
        
        // Jump if the space key is pressed
	    if (Input.GetKey(KeyCode.Space))
        {
            Jump();
            anim.SetBool("isJumping", true); // Set the isJumping parameter to true
            StartCoroutine(StopExpandingAfterSeconds(0.2f)); // Wait for 0.2 seconds
        }
    }
    
    ...
    
    	IEnumerator StopExpandingAfterSeconds(float seconds)
    {
    	yield return new WaitForSeconds(seconds);
    	anim.SetBool("isJumping", false);
    }
   ```

Como se puede observar, añadimos una corrutina que permite cambiar el valor de nuestro parametro "isJumping" a False después de 0.2 segundos.
Además de cambiar el valor de nuestro parametro "isJumping" a True cuando el jugador salta.

De esta manera conseguimos el siguiente resultado:
![img_9.png](Images%2Fimg_9.png)

### 2. Animación relativa a la cercanía del enemigo:

Posteriormente, para  crear una animación que cambie el color de nuestro player. Usamos el siguiente propiedas: Mesh Renderer -> Material_BaseColor.

![img_10.png](Images%2Fimg_10.png)

De esta manera, al igual que en nuestra animación anterior, manipulamos los frames y los valores de tiempo y RGB para notar el cambio.

Con respecto a nuestros estados, quedaría de la siguiente manera, junto a su parámetro booleano "isNear":

![img_11.png](Images%2Fimg_11.png)

A la hora de modificar el Script, se realizó la siguiente modificación:

````csharp
    private Transform target; // Reference to the player
	private PlayerController playerController; // Reference to the player controller
    private float distance=3.0f; // Distance between the enemy and the player
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
		playerController = target.GetComponent<PlayerController>();
    }
    void Update()
    {
		if (playerController.count >= 12) // Check if the player has collected all the pickups
	    {
		    pathfinder.isStopped = true; // Stop the enemy
		}
		else{
        	pathfinder.SetDestination(target.position);
		}
		
		//Check if the enemy is near the player
        if (Vector3.Distance(target.position, transform.position) <= distance)
        {
            playerController.anim.SetBool("isNear", true); // Set the isNear parameter to true
        }
		        else
        {
            playerController.anim.SetBool("isNear", false); // Set the isNear parameter to false
        }
    }
}
````

Considerando que el parámetro "anim" sea público en mi archivo PlayerController.cs, y respetando una distancia de 3.0f entre el enemigo y el jugador. Añadimos una condición en la cual, cuando el Vector distancia sea inferior o igual a la distancia establecida, el parámetro "isNear" cambie a True. De lo contrario, cambie a False.

Como en nuestro caso, la acción de salto fue realizada por acciones de fuerza por tener su componente RigidBody y no de animación, trabajan de manera independiente en nuestro juego. Haciendo posible que ambas interacciones actúen a la vez (cambio de color y salto).

![img_12.png](Images%2Fimg_12.png)

***IMPORTANTE:*** No obstante, es importante entender que los estados no trabajan de manera paralela. Aunque puedas hacer subestados, estos transcurren uno a uno. Esto quiere decir que, si el salto fuese un estado en nuestro Animator Controller, no pueden haber 2 en 1 como ejecución.

---

### GIF'S DE DEMOSTRACIÓN:

- Alerta de proximidad con el enemigo:

![gif_1.gif](GIFs%2Fgif_1.gif)

- Salto del jugador y expansión de su tamaño al saltar: 

![gif_2.gif](GIFs%2Fgif_2.gif)

- Animación al comer un bloque:

![gif_3.gif](GIFs%2Fgif_3.gif)

- El enemigo se detiene al recoger todos los bloques:

![gif_4.gif](GIFs%2Fgif_4.gif)

---
## DALE UNA ESTRELLA A ESTE REPOSITORIO SI TE HA SERVIDO DE AYUDA! :sparkles: