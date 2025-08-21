using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RotationCenterHeight : MonoBehaviour
{
    public Vector3 screenRotationCenter = new Vector3(0, 0.088f, 0);
    public Vector3 roomRotationCenter = new Vector3(0, 0.088f, 0);

    public Transform screenCenter;
    public Transform roomCenter;

    [Header("UI")]
    public TMP_InputField screenInputFieldX;
    public TMP_InputField screenInputFieldY;
    public TMP_InputField screenInputFieldZ;
    [Space]
    public TMP_InputField roomInputFieldX;
    public TMP_InputField roomInputFieldY;
    public TMP_InputField roomInputFieldZ;

    // Start is called before the first frame update
    void Start()
    {
        SetScreenRotationCenter(screenRotationCenter);
        SetRoomRotationCenter(roomRotationCenter);

        screenInputFieldX.SetTextWithoutNotify(screenRotationCenter.x.ToString());
        screenInputFieldX.onEndEdit.AddListener(value =>
        {
            SetScreenRotationCenter(new Vector3(float.Parse(value), screenRotationCenter.y, screenRotationCenter.z));
        });
        screenInputFieldY.SetTextWithoutNotify(screenRotationCenter.y.ToString());
        screenInputFieldY.onEndEdit.AddListener(value =>
        {
            SetScreenRotationCenter(new Vector3(screenRotationCenter.x, float.Parse(value), screenRotationCenter.z));
        });
        screenInputFieldZ.SetTextWithoutNotify(screenRotationCenter.z.ToString());
        screenInputFieldZ.onEndEdit.AddListener(value =>
        {
            SetScreenRotationCenter(new Vector3(screenRotationCenter.x, screenRotationCenter.y, float.Parse(value)));
        });

        roomInputFieldX.SetTextWithoutNotify(roomRotationCenter.x.ToString());
        roomInputFieldX.onEndEdit.AddListener(value =>
        {
            SetRoomRotationCenter(new Vector3(float.Parse(value), roomRotationCenter.y, roomRotationCenter.z));
        });
        roomInputFieldY.SetTextWithoutNotify(roomRotationCenter.y.ToString());
        roomInputFieldY.onEndEdit.AddListener(value =>
        {
            SetRoomRotationCenter(new Vector3(roomRotationCenter.x, float.Parse(value), roomRotationCenter.z));
        });
        roomInputFieldZ.SetTextWithoutNotify(roomRotationCenter.z.ToString());
        roomInputFieldZ.onEndEdit.AddListener(value =>
        {
            SetRoomRotationCenter(new Vector3(roomRotationCenter.x, roomRotationCenter.y, float.Parse(value)));
        });
    }

    private void SetScreenRotationCenter(Vector3 center)
    {
        if (screenCenter != null)
        {
            screenCenter.localPosition = center;
            this.screenRotationCenter = center;
        }
    }

    private void SetRoomRotationCenter(Vector3 center)
    {
        if (roomCenter != null)
        {
            roomCenter.localPosition = center;
            this.roomRotationCenter = center;
        }
    }
}
