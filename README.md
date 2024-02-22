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
