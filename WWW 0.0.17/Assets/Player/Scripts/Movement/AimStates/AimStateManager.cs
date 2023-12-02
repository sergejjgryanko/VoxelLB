using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] float mouseSense = 1;   
    [SerializeField] Transform camFollowPos;
    float xAxis, yAxis;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 75;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10;

    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;

    public GameObject pistolInHand;  // ссылка на объект пистолета в руках
    public GameObject pistolInCabura; // ссылка на объект пистолета в Cabura
    public Animator shootingAnimator; // ссылка на аниматор слоя Shooting

    bool pistolInHandActive = true; // флаг, показывающий, активен ли пистолет в руках
    
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            TogglePistol(); // вызываем функцию, которая переключает состояние пистолета
        }

        currentState.UpdateState(this);
    }

    void TogglePistol()
    {
        pistolInHandActive = !pistolInHandActive; // инвертируем флаг

        // Переключаем активность объектов в зависимости от состояния флага
        pistolInHand.SetActive(pistolInHandActive);
        pistolInCabura.SetActive(!pistolInHandActive);

        // Активируем или дезактивируем слой аниматора в зависимости от состояния флага
        if (pistolInHandActive)
        {
            // Если пистолет в руках, включаем слой Shooting
            shootingAnimator.SetLayerWeight(shootingAnimator.GetLayerIndex("Shooting"), 1f);
        }
        else
        {
            // Если пистолет в Cabura, выключаем слой Shooting
            shootingAnimator.SetLayerWeight(shootingAnimator.GetLayerIndex("Shooting"), 0f);
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
