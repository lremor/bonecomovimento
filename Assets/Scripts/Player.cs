using UnityEditor.Animations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : MonoBehaviour {

    CharacterController controller;
    public Animator animation;
    public Transform cubotransform;
    public Collider cubocollider;
    public Transform head;
    public float distanciamira;
    public float velocidadecubo;
    bool subindo = false;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 6f;
    float strafeSpeed = 6f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;
    bool walking = false;

    void Start() {

        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update() {


        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        // force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if(controller.isGrounded) {
            vertical = Vector3.down;
        }

        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0) {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);

        
        walking = !(finalVelocity.x == 0 && finalVelocity.z == 0);
        animation.SetBool("walking",walking);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (cubocollider.Raycast(ray, out hit, distanciamira))
            {
                subindo = true;
                
                //Vector3 escala = cubotransform.localScale;
                //escala.x = 2;
                //cubotransform.localScale = escala;

                //Vector3 posicao = cubotransform.position;
                //posicao.y = 5;
                //cubotransform.position = posicao;
            }
        }

        if (subindo == true)
        {
            Vector3 position = cubotransform.position;

            if (position.y < 5)
            {
                position.y += Time.deltaTime * velocidadecubo;
                cubotransform.position = position;
            }
            else
            {
                subindo = false;
            }

        }
    }

}
