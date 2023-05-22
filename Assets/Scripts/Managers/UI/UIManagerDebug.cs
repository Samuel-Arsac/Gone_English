using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerDebug : MonoBehaviour
{
  public static UIManagerDebug instance;
  #region Debug Variables
  
  [Space(3f)]
  [Header("Pan Settings")]
  [Header("Debug")]
  [SerializeField] private GameObject cameraPanDebugZone;
  [SerializeField] private GameObject zoomDebugZone;
  [SerializeField] private TextMeshProUGUI panSpeedText;
  [SerializeField] private TextMeshProUGUI resetTimeText;
  [SerializeField] private Button resetButton;

  public TMP_InputField inputField_TimeReset;

  [Space(5f)]
  [Header("Edges Limits")]
  [SerializeField] private TextMeshProUGUI rightEdgeText;
  [SerializeField] private TextMeshProUGUI leftEdgeText;
  [SerializeField] private TextMeshProUGUI topEdgeText;
  [SerializeField] private TextMeshProUGUI bottomEdgeText;

  [SerializeField] private Slider rightEdgeSlider;
  [SerializeField] private Slider leftEdgeSlider;
  [SerializeField] private Slider topEdgeSlider;
  [SerializeField] private Slider bottomEdgeSlider;

  [Space(5f)]
  [Header("Zoom")]
  [SerializeField] private List<string> blendNames;
  [SerializeField] private TMP_Dropdown dropdown_blend;
  [HideInInspector] public string newValue;
  [SerializeField] private TextMeshProUGUI zoomTimeText;
  public TMP_InputField inputField_ZoomTime;

  private bool isPanSettingsShowed;
  private bool isZoomSettingsShowed;


  #endregion

  #region Singleton
  void Awake ()
  {
    if (instance == null)
      instance = this;
    else
      Destroy (gameObject);
  }

  #endregion

  #region  DisplayDebug

  public void DisplayDebugPan()
  {
    if(isPanSettingsShowed)
    {
      isPanSettingsShowed = false;
      cameraPanDebugZone.SetActive(false);
    }
    else
    {
      isPanSettingsShowed = true;
      cameraPanDebugZone.SetActive(true);
    }
  }

  public void DisplayDebugZoom()
  {
    if(isZoomSettingsShowed)
    {
      isZoomSettingsShowed = false;
      zoomDebugZone.SetActive(false);
    }
    else
    {
      isZoomSettingsShowed = true;
      zoomDebugZone.SetActive(true);
    }
  }

  public void UpdateValueText()
  {

   panSpeedText.text = CameraManager.instance.panSpeedValue.ToString();

  }

  public void UpdateResetTimeText(float value)
  {
    resetTimeText.text = value.ToString()  + " s";
  }

  public void RefreshRightTextValue(float value)
  {
    rightEdgeSlider.value = value;
    rightEdgeText.text = value.ToString();
  }

  public void RefreshLeftTextValue(float value)
  {
    leftEdgeSlider.value = value;
    leftEdgeText.text = value.ToString();
  }

  public void RefreshTopTextValue(float value)
  {
    topEdgeSlider.value = value;
    topEdgeText.text = value.ToString();
  }

  public void RefreshBottomTextValue(float value)
  {
    bottomEdgeSlider.value = value;
    bottomEdgeText.text = value.ToString();
  }

  public void RefreshDropdownValues()
  {
    blendNames = new List<string>(CameraManager.instance.blendTypes);
    dropdown_blend.AddOptions(blendNames);
  }

  public void ChangeValueBlend(int index)
  {
    newValue = dropdown_blend.options[index].text;
    CameraManager.instance.ChangeBlendType();
  }

  public void RefreshZoomTimeText(float timeValue)
  {
    zoomTimeText.text = timeValue.ToString() + " s";
  }

  #endregion

}
