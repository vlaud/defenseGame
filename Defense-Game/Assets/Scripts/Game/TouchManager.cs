using UnityEngine;

public class TouchManager : MonoBehaviour
{
    float m_fOldToucDis = 0f;       // ��ġ ���� �Ÿ��� �����մϴ�.
    float m_fFieldOfView = 60f;     // ī�޶��� FieldOfView�� �⺻���� 60���� ���մϴ�.
    int ClickCount = 0;
    [SerializeField] private Camera _Camera;

    void Update()
    {
        CheckTouch();
        QuitGame();
    }
    void CheckTouch()
    {
        float m_fToucDis, fDis;

        // ��ġ�� �ΰ��̰�, �� ��ġ�� �ϳ��� �̵��Ѵٸ� ī�޶��� fieldOfView�� �����մϴ�.
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;

            // ���� �� ��ġ�� �Ÿ��� ���� �� ��ġ�� �Ÿ��� ���̸� FleldOfView�� �����մϴ�.
            m_fFieldOfView -= fDis;

            // �ִ�� 100, �ּҴ� 20���� ���̻� ���� Ȥ�� ���Ұ� ���� �ʵ��� �մϴ�.
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 20.0f, 100.0f);

            // Ȯ�� / ��Ұ� ���ڱ� �����ʵ��� �����մϴ�.
            _Camera.fieldOfView = Mathf.Lerp(_Camera.fieldOfView, m_fFieldOfView, Time.deltaTime * 5);

            m_fOldToucDis = m_fToucDis;
        }
    }
    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);
        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }
    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
