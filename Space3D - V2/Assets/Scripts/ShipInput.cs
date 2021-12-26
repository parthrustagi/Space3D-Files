using UnityEngine;

/// <summary>
/// Class specifically to deal with input.
/// </summary>
public class ShipInput : MonoBehaviour
{
    public bool mouseUse = true;
    public bool rollAdd = true;

    [Space]

    [Range(-1, 1)]
    public float pitchF;
    [Range(-1, 1)]
    public float yawF;
    [Range(-1, 1)]
    public float rollF;
    [Range(-1, 1)]
    public float strafeF;
    [Range(0, 1)]
    public float throttleF;

    private const float throttleSpeed = 0.5f;
    private Ship ship;

    private void Awake()
    {
        ship = GetComponent<Ship>();
    }

    private void Update()
    {
        if (mouseUse)
        {
            strafeF = Input.GetAxis("Horizontal");
            SetStickCommandsUsingMouse();
            UpdateMouseWheelThrottle();
            UpdateKeyboardThrottle(KeyCode.W, KeyCode.S);
        }
        else
        {            
            pitchF = Input.GetAxis("Vertical");
            yawF = Input.GetAxis("Horizontal");

            if (rollAdd)
            {
                rollF = -Input.GetAxis("Horizontal") * 0.5f;
            }

            strafeF = 0.0f;
            UpdateKeyboardThrottle(KeyCode.R, KeyCode.F);
        }
    }
    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        pitchF = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        yawF = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        pitchF = -Mathf.Clamp(pitchF, -1.0f, 1.0f);
        yawF = Mathf.Clamp(yawF, -1.0f, 1.0f);
    }

    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {
        float target = throttleF;

        if (Input.GetKey(increaseKey))
        {
            target = 1.0f;
        }
        
        else if (Input.GetKey(decreaseKey))
        {
            target = 0.0f;
        }

        throttleF = Mathf.MoveTowards(throttleF, target, Time.deltaTime * throttleSpeed);
    }

    private void UpdateMouseWheelThrottle()
    {
        throttleF += Input.GetAxis("Mouse ScrollWheel");
        throttleF = Mathf.Clamp(throttleF, 0.0f, 1.0f);
    }
}