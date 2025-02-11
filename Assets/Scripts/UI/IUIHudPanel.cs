using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IUIHudPanel
{
    void Create(Transform rootTransform);
    void Open();

    UniTask Transition(bool toggle);
    void Close();
}
