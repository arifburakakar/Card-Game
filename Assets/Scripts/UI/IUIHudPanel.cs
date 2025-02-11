using UnityEngine;
using UnityEngine.SceneManagement;

public interface IUIHudPanel
{
    void Create(Transform rootTransform);
    void Open();

    float Transition(bool toggle);
    void Close();
}
