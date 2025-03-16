using UnityEngine;

public class UIPART : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        if(IsActive == false)
        {
            Debug.LogWarning("Not Active this UI");
            return;
        }
        gameObject.SetActive(false);
    }

    public virtual void Toggle()
    {
        if (IsActive)
            Close();
        else Open();
    }
}
