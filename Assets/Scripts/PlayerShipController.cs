
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    //spaceship variables
    [Tooltip("Sideway speed is divided by 3")]
    public float _speed = 30f,defaultSpeed=5f,activeForwardSpeed,forwardAcceleration = 2.5f, activeStraftSpeed, straftAcceleration = 2f;
    private Vector2 pointer;
    private Rigidbody _rb;
    private List<Vector3> spawnpoints = new List<Vector3>();
    private Rigidbody rb;
    private GameManager _gm;
    private Transform gfx;
    private float rotZ;

    //touch Input
    //private Touch touch;
    private Vector2 lookInput;
    [Tooltip("camera sensitivity")]
    [SerializeField]
    private float sensitivity;
    private Vector2 MoveInput;
    private FloatingJoystick joystick;

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    private UIManager _uim;

    private void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        leftFingerId = -1;
        rightFingerId = -1;
        halfScreenWidth = Screen.width / 2;
        _rb = GetComponent<Rigidbody>();
        _uim = GameObject.Find("Canvas").GetComponent<UIManager>();
        rb = transform.GetComponent<Rigidbody>();
        joystick = _gm.GetJoystick();
        gfx = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale==1)
        {
            //print("Joystick: "+"("+JoystickMovement.Horizontal+","+ JoystickMovement.Vertical+")");
            if(Application.platform==RuntimePlatform.Android)
            {
                GetTouchInput();
            }
            Move();
            Rotate();
        }

        sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }


    private void GetTouchInput()
    {
        for(int i=0; i<Input.touchCount;i++)
        {
            Touch t = Input.GetTouch(i);
            switch(t.phase)
            {
                case TouchPhase.Began:
                    if(t.position.x<halfScreenWidth&&leftFingerId==-1)
                    {
                        leftFingerId = t.fingerId;
                    }
                    else if(t.position.x>halfScreenWidth&&rightFingerId==-1)
                    {
                        rightFingerId = t.fingerId;
                    }break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                    }

                    break;
                case TouchPhase.Moved:
                    if(t.fingerId==rightFingerId)
                    {
                        lookInput.x += t.deltaPosition.y * sensitivity * Time.deltaTime;
                        lookInput.y += t.deltaPosition.x * sensitivity * Time.deltaTime;
                    }
                    if(t.fingerId==leftFingerId)
                    {
                        MoveInput.x = joystick.Horizontal * _speed/3 * Time.deltaTime; 
                        MoveInput.y = joystick.Vertical * _speed * Time.deltaTime;
                    }break;
            }
        }
    }

    private void Move()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if(leftFingerId!=-1)
            {
                activeStraftSpeed = Mathf.Lerp(activeStraftSpeed, MoveInput.x * _speed / 3, straftAcceleration * Time.deltaTime);
                transform.position += transform.right * MoveInput.x;
                transform.position += transform.forward * MoveInput.y;
                float rotationZ = -(activeStraftSpeed / 3 * _speed / 3);
                rotZ = rotationZ;
                rotationZ = Mathf.Clamp(rotationZ, -50, 50);
                gfx.localRotation = Quaternion.Euler(gfx.localRotation.x, gfx.localRotation.y, rotationZ);
            }
            else
            {
                float rotationZ = Mathf.Lerp(rotZ,0, 1);
                rotationZ = Mathf.Clamp(rotationZ, -50, 50);
                gfx.localRotation = Quaternion.Euler(gfx.localRotation.x, gfx.localRotation.y, rotationZ);
            }
        }
        else
        {
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * _speed, forwardAcceleration * Time.deltaTime);
            activeStraftSpeed= Mathf.Lerp(activeStraftSpeed, Input.GetAxisRaw("Horizontal") * _speed/3, straftAcceleration * Time.deltaTime);
            transform.position += transform.right * activeStraftSpeed * Time.deltaTime;
            transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            float rotationZ = -(activeStraftSpeed / 3 * _speed / 3);
            rotationZ = Mathf.Clamp(rotationZ, -50, 50);
            gfx.localRotation = Quaternion.Euler(gfx.localRotation.x, gfx.localRotation.y,rotationZ);
        }
    }

    private void Rotate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if(rightFingerId!=-1)
            {
                transform.localRotation = Quaternion.Euler(-lookInput.x, lookInput.y, transform.rotation.z);
            }
        }
        else
        {
            pointer.x += Input.GetAxis("Mouse X")*sensitivity;
            pointer.y += Input.GetAxis("Mouse Y")*sensitivity;
            transform.localRotation = Quaternion.Euler(-pointer.y, pointer.x, 0.0f);
        }

    }
    public Vector3 GetEnemySpawnPoint(float Offset)
    {
        Vector3 SpawnPoint = (Random.onUnitSphere * Offset) + transform.position;
        return SpawnPoint;
    }
}
