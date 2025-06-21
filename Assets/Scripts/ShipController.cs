using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Joystick References")]
    public FixedJoystick movementJoystick;
    public FixedJoystick rotationJoystick;

    [Header("Thrust Settings")]
    public float mainThrust = 10f;
    public float boostMultiplier = 1.5f;

    [Header("Rotation Settings")]
    public float pitchSpeed = 30f;
    public float yawSpeed = 30f;
    public float rollSpeed = 30f;

    [Header("Engine Sound")]
    public AudioSource engineAudio; // Drag an AudioSource here
    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;

    private float pitchInput;
    private float yawInput;
    private float rollInput;
    private float thrustInput;

    void Start()
    {
        if (engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.playOnAwake = false;
            engineAudio.Play();
        }
    }

    void Update()
    {
        pitchInput = movementJoystick.Vertical;
        yawInput = movementJoystick.Horizontal;
        rollInput = rotationJoystick.Horizontal;

        thrustInput = Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f;

        // Forward movement
        transform.position += transform.forward * mainThrust * thrustInput * Time.deltaTime;

        // Rotation
        float pitch = pitchInput * pitchSpeed * Time.deltaTime;
        float yaw = yawInput * yawSpeed * Time.deltaTime;
        float roll = rollInput * rollSpeed * Time.deltaTime;
        transform.Rotate(pitch, yaw, -roll, Space.Self);

        // Engine Sound Pitch Adjust
        if (engineAudio != null)
        {
            engineAudio.pitch = Mathf.Lerp(minPitch, maxPitch, (thrustInput - 1f) / (boostMultiplier - 1f));
        }
    }
}
