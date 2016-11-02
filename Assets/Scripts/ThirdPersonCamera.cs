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

    // My rotation value.  We have to keep our
    // own, since localEulerAngles must be 0-360.
    // Our variable is kept in range -45 to 45.
    private float rotationX = 0;
    private float rotationY = 0;
    private GameObject player;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update() {


        float xRot = Input.GetAxis("Mouse Y");
        float yRot = Input.GetAxis("Mouse X");

        Vector3 movement = new Vector3(yRot, xRot, 0);
        //movement.Normalize();
        transform.Translate(movement);

        transform.LookAt(player.transform);
    }

}
