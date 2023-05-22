using System.Collections;
using System;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("Pan")]
    [SerializeField] private bool allowToMove = true;
    [SerializeField] private float rightEdgeValue = .90f;
    [SerializeField] private float leftEdgeValue = 0.10f;
    [SerializeField] private float topEdgeValue = 0.90f;
    [SerializeField] private float bottomEdgeValue = 0.10f;
    [SerializeField] private float panSpeed = 15f;
    private float timeMultiplier = 1;
    [HideInInspector] public float panSpeedValue;
    private float resetTimer = 0f;
    [MinValue(1f)][MaxValue(4f)] private float zoomTime = 1f;
    [MinValue(1f)][MaxValue(5f)][SerializeField] private float resetTime = 1f;
    private bool isCoroutineRunning = false;

    [Space(5f)]
    [Header("Cinemachine")]
    [SerializeField] private CinemachineInputProvider inputProvider;
    public CinemachineVirtualCamera virtualCameraPan;
    public CinemachineVirtualCamera virtualCameraZoom;
    public CinemachineBrain cinemachineBrain;
    public string[] blendTypes;
    public float defaultLensValue;

    [Space(5f)]
    [Header("Camera")]
    private Transform cameraTransform;
    private Vector3 originalPosition;
    [SerializeField] private Collider2D boundary;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        cameraTransform = virtualCameraPan.VirtualCameraGameObject.transform;
    }

    private void Start()
    {
        blendTypes = Enum.GetNames(typeof(CinemachineBlendDefinition.Style));
        zoomTime = cinemachineBrain.m_DefaultBlend.m_Time;

        Mathf.Clamp(timeMultiplier, 1, 10);
        Mathf.Clamp(panSpeedValue, 1, 10);

        panSpeedValue = timeMultiplier;

        originalPosition = cameraTransform.position;
    }
    private void Update()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);

        Vector2 realXValue = Camera.main.WorldToScreenPoint(new Vector2(x, 0f));
        Vector2 realYValue = Camera.main.WorldToScreenPoint(new Vector2(0f, y));

        //Debug.Log(realXValue);
        //Debug.Log(realYValue);

        if ((x != 0 || y != 0) && allowToMove)
        {
            PanScreen(realXValue, realYValue);
        }
    }

    #region Pan
    ///Déplacement caméra avec position de la souris
    public Vector2 PanDirectionUp(float x, float y)
    {
        

        Vector2 direction = Vector2.zero;
        if (y >= Screen.height * rightEdgeValue)
        {
            direction.y += 1;
        }
        else if (y <= Screen.height * leftEdgeValue)
        {
            direction.y -= 1;
        }

        if (x >= Screen.width * topEdgeValue)
        {
            direction.x += 1;
        }
        else if (x<= Screen.width * bottomEdgeValue)
        {
            direction.x -= 1;
        }
        return direction;
    }

    public void PanScreen(Vector2 xValue, Vector2 yValue)
    {
        if (!isCoroutineRunning)
        {
            Vector2 direction = PanDirectionUp(xValue.x, yValue.y);
            Vector3 targetPosition = (Vector3)direction * panSpeed + cameraTransform.position;
            targetPosition.x = Mathf.Clamp(targetPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, boundary.bounds.min.y, boundary.bounds.max.y);
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * timeMultiplier);

        }

    }
    #endregion


    #region Debug

    #region PanSpeed
    public void IncreaseSpeedValue1()
    {
        timeMultiplier += 1;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }
    public void IncreaseSpeedValue3()
    {
        timeMultiplier += 3;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }
    public void IncreaseSpeedValue5()
    {
        timeMultiplier += 5;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }
    public void IncreaseSpeedValue15()
    {
        timeMultiplier += 15;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }

    public void DecreaseSpeedValue1()
    {
        timeMultiplier -= 1;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }
    public void DecreaseSpeedValue3()
    {
        timeMultiplier -= 3;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }
    public void DecreaseSpeedValue5()
    {
        timeMultiplier -= 5;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }
    public void DecreaseSpeedValue15()
    {
        timeMultiplier -= 15;
        panSpeedValue = timeMultiplier;
        UIManagerDebug.instance.UpdateValueText();
    }

    #endregion


    #region ResetCamera

    public void ResetCamera()
    {
        StartCoroutine(ResetCameraPos());

    }

    public void SetResetTimeValue()
    {
        resetTime = float.Parse(UIManagerDebug.instance.inputField_TimeReset.text);
        UIManagerDebug.instance.UpdateResetTimeText(resetTime);
    }

    public void SetZoomTimeValue()
    {
        zoomTime = float.Parse(UIManagerDebug.instance.inputField_ZoomTime.text);
        cinemachineBrain.m_DefaultBlend.m_Time = zoomTime;
        UIManagerDebug.instance.RefreshZoomTimeText(zoomTime);
    }

    private void StopReset()
    {
        StopCoroutine(ResetCameraPos());
    }
    IEnumerator ResetCameraPos()
    {
        isCoroutineRunning = true;
        resetTimer += Time.deltaTime;
        while (cameraTransform.position != originalPosition)
        {
            yield return null;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, originalPosition, resetTimer / resetTime);
        }
        isCoroutineRunning = false;
        StopReset();
    }


    public void ChangeBlendType()
    {
        cinemachineBrain.m_DefaultBlend.m_Style =
        (CinemachineBlendDefinition.Style)System.Enum.Parse(typeof(CinemachineBlendDefinition.Style), UIManagerDebug.instance.newValue);
    }

    #endregion

    #endregion

}
