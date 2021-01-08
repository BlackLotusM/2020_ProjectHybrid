using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class micPickup : NetworkBehaviour
{
    public float rmsVal;
    [SyncVar]
    public float dbVal;
    public float pitchVal;
    private const int QSamples = 1024;
    private const float Threshold = 0.02f;
    public float reff;

    public GameObject MiniGameHost;
    public GameObject MiniGameJoin;

    public TextMeshProUGUI btnHost;

    [SerializeField]
    private Canvas canvas;
    public TextMeshProUGUI dbText;

    public Slider showdb;
    public Slider value;

    public bool t;
    public bool t2;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    public void mee(bool BOOL)
    {
        if (!isLocalPlayer) return;
        MiniGameJoin.SetActive(BOOL);
    }

    public void me(bool BOOL)
    {
        if (!isLocalPlayer) return;
        MiniGameHost.SetActive(BOOL);
    }

    private void Start()
    {
        if (!isLocalPlayer) return;
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 22050);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audio.Play();
        StartCoroutine(Updateall());
        
        StartCoroutine(AnalyzeSound());
        canvas.gameObject.SetActive(true);
    }

    IEnumerator Updateall()
    {
        mee(t);
        me(t2);
        showdb.value = dbVal;
        reff = value.value;
        dbText.text = Convert.ToString(dbVal);
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Updateall());
        CmdUpdateServer(this.gameObject, dbVal);

    }

    [Command]
    public void CmdUpdateServer(GameObject mc, float value)
    {
        mc.GetComponent<micPickup>().dbVal = value;
    }

    IEnumerator AnalyzeSound()
    {
        GetComponent<AudioSource>().GetOutputData(_samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        rmsVal = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        dbVal = 20 * Mathf.Log10(rmsVal / reff); // calculate dB
        if (dbVal < -160) dbVal = -160; // clamp it to -160dB min
                                        // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { 
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchVal = freqN * (_fSample / 2) / QSamples;
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(AnalyzeSound());
    }
}
