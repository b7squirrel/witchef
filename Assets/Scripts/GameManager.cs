using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("FrameRate")]
    private int target;

    [Header("TimeStop")]
    public float stopTime;
    public float slowTime;
    private bool isStopping;

    [Header("Camera Shake")]
    private float shakeTimeRemaining, shakePower, shakeFadeTime;
    public Camera currentCamera;
    private bool isShakingCamera;
    public int numberOfShake;   // ����Ƚ��
    public float shakingAmount;  // ��������

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        //Quit
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        //Hide mouse cursor
        if (Input.GetKeyDown(KeyCode.O))
        {
            HideMouseCursor();
        }

        //Reload scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }

        //Camera Shake Test
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(CameraShake(numberOfShake, shakingAmount));
        }
    }

    public void StartCameraShake(int _numberOfShake, float _shakingAmount)
    {
        StartCoroutine(CameraShake(_numberOfShake, _shakingAmount));
    }

    IEnumerator CameraShake(int _numberOfShake, float _shakingAmount)
    {
        float _currentShakingAmount = _shakingAmount;

        for(int i=0; i < _numberOfShake - 1; i++)
        {
            if(i==0 || i== _numberOfShake - 2)
            {
                _currentShakingAmount = _shakingAmount * .5f;
            }
            else
            {
                _currentShakingAmount = _shakingAmount;
            }
            
            if(i % 2 == 0) // index�� Ȧ���϶��� _currentShakingAmount�� ������ ¦���� ���� ����� �ؼ� �¿�� ��鵵��
            {
                _currentShakingAmount = Mathf.Abs(_currentShakingAmount);
            }
            else
            {
                _currentShakingAmount = -Mathf.Abs(_currentShakingAmount);

            }

            currentCamera.transform.position += new Vector3(_currentShakingAmount, _currentShakingAmount * .1f, 0f);
            yield return new WaitForSecondsRealtime(.04f);
        }
    }
    //  ���ο� ���
    public void TimeStop(float _stopTime)
    {
        if (!isStopping)
        {
            isStopping = true;
            Time.timeScale = 0f;
            StartCoroutine(StartTimeStop(_stopTime));
        }
    }

    IEnumerator StartTimeStop(float m_StopTime)
    {
        yield return new WaitForSecondsRealtime(m_StopTime);
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(slowTime);
        Time.timeScale = 1f;
        isStopping = false;
    }

    // �����ӷ���Ʈ
    private void FixedUpdate()
    {
        if (target != Application.targetFrameRate)
        {
            Application.targetFrameRate = target;
        }
    }
    public float Choose(float[] probs)
    {
        float total = 0f;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    // ���콺 Ŀ�� �����
    void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // �� ���ε�
    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
