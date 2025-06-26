using NAudio.CoreAudioApi.Interfaces;
using NAudio.CoreAudioApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Data;
using UnityEngine.UI;

public class SystemPlaybackManager : MonoBehaviour
{
    public Toggle toggle;
    public bool useSystemPlayback = true;

    public void Awake()
    {
        toggle.SetIsOnWithoutNotify(useSystemPlayback);
        toggle.onValueChanged.AddListener(value =>
        {
            useSystemPlayback = value;
        });
    }

    public void SetUseSystemPlayback(bool value)
    {
        useSystemPlayback = value;
    }

    public void PlayPause()
    {
        keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
    }

    public void Play()
    {
        if (!IsSystemPlayingAudio()) PlayPause();
    }

    public void Pause()
    {
        if (IsSystemPlayingAudio()) PlayPause();
    }

    public void Next()
    {
        keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
    }

    public void Previous()
    {
        keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
    }

    static bool IsSystemPlayingAudio()
    {
        var unityProcessId = System.Diagnostics.Process.GetCurrentProcess().Id;

        using (var enumerator = new MMDeviceEnumerator())
        {
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            foreach (var device in devices)
            {
                using (device)
                {
                    var sessionCollection = device.AudioSessionManager.Sessions;
                    for (int i = 0; i < sessionCollection.Count; i++)
                    {
                        var session = sessionCollection[i];
                        if (session.State == AudioSessionState.AudioSessionStateActive && session.GetProcessID != unityProcessId)
                        {
                            if (session.AudioMeterInformation.MasterPeakValue > 0.0f)
                            {
                                Debug.Log("System is playing audio from another application.");
                                return true;
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("System is NOT playing audio from another application.");
        return false;
    }


    [DllImport("user32.dll")]
    public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

    public const int KEYEVENTF_EXTENTEDKEY = 1;
    public const int KEYEVENTF_KEYUP = 0;
    public const int VK_MEDIA_NEXT_TRACK = 0xB0;
    public const int VK_MEDIA_PREV_TRACK = 0xB1;
    public const int VK_MEDIA_STOP = 0xB2;
    public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
}