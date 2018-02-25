using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f; //RCS- Reaction Control System.
    [SerializeField] float mainThrust = 100f;
    Rigidbody myRigidBody;
    AudioSource thrusterAudio;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        thrusterAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddRelativeForce(Vector3.up * mainThrust);

            //check if the audio is already playing.
            if (!thrusterAudio.isPlaying)
            {
                thrusterAudio.Play();
            }
        }
        else
        {
            thrusterAudio.Pause();
        }
    }

    private void Rotate()
    {
        myRigidBody.freezeRotation = true; //take manual control of the rotation.

        float rotationThisFrame = rcsThrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        myRigidBody.freezeRotation = false; //give back the control.
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    print("Rocket Ready To Launch Captain");//TODO: remove
                    break;
                }
            case "Fuel":
                {
                    print("Fuel");//TODO remove
                    break;
                }
            default:
                print("Dead");
                break;//TODO kill the player
        }

    }
}