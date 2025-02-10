using UnityEngine;

public class MetaManager : BaseManager
{
    public override void CreateManagerNecessary()
    {
        base.CreateManagerNecessary();
        var metaController = Object.Instantiate(Resources.Load<UIMetaHudPanel>("UI Meta Controller"));
        metaController.Create(RootObject.transform);
        UIControllerSystem.Instance.MetaController = metaController;
    }
}