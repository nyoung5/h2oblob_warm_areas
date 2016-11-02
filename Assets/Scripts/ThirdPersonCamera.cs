using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
    //
    //    private float speed = 75.0f;
    //

    public enum RotationAxes {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }
    // Setting for how the mouse should look
    public RotationAxes axes = RotationAxes.MouseXandY; //This will be given to the editor X for player and Y for camera. Question: is this a good idea and will it be a permanent change?

    // General settings - sensitivity
    [SerializeField]
    private float sensitivityHor = 9.0f;
    [SerializeField]
    private float sensitivityVert = 9.0f;
    // General settings - y rotation constraints
    [SerializeField]
    private float maxVert = 45f;
    [SerializeField]
    private float minVert = -45f;

    private GameObject player;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    //TODO: CAP DISTANCE CAMERA CAN BE FROM PLAYER
    //TODO: CAP VERTICAL CAMERA MOVEMENT
    void Update() {
        if (axes == RotationAxes.MouseY || axes == RotationAxes.MouseXandY) {
            float xRot = Input.GetAxis("Mouse Y");
            Vector3 movement = new Vector3(0, -xRot, 0);
            transform.Translate(movement);
            transform.LookAt(player.transform);
        }
        if (axes == RotationAxes.MouseX || axes == RotationAxes.MouseXandY) {
            float yRot = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;
            float currentXRot = transform.localEulerAngles.x;
            transform.localEulerAngles = new Vector3(currentXRot, yRot, 0);
        }





        
    }

}
