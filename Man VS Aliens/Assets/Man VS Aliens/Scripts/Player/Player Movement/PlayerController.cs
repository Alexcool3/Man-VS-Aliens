using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float thrusterForce = 1000f;

    // Configurable Joint Property variables
    [Header("Spring Settings:")]
    //[SerializeField] private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField] private float jointSpring = 20f;
    [SerializeField] private float jointMaxForce = 40f;

    // Private reference to PlayerMotor script called motor
    private PlayerMotor motor;
    private ConfigurableJoint joint;   

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        // Calculate movement velocity as a 3D Vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov; // Vector (1,0,0) Only x component will change within the range [-1;1]
        Vector3 _movVertical = transform.forward * _zMov; // Vector (0,0,1) Only z component will change within the range [-1;1]
        // Final Movement vector. 
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed; // Normalize gets the length of the vector which is 1. 

        // Apply Movement
        motor.Move(_velocity);

        // Calculate rotation as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;
       
        // Apply rotation
        motor.Rotate(_rotation);

        // Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        // Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

        // Calculate the thrusterforce based on input
        Vector3 _thrusterForce = Vector3.zero;
       
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        } else
        {
            SetJointSettings(jointSpring);
        }
        // Apply the thrusterforce
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };
        
    }

}
