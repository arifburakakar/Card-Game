using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IUIHudPanel
{
    void Create(Transform rootTransform);
    void Open();

    UniTask Transition(bool toggle);
    void Close();
}
