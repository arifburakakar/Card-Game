using Vector3 = UnityEngine.Vector3;

public partial class Game
{
    private Item selectedItem;
    private Vector3[] slotPoints;

    private void CreateSlots()
    {
        slotPoints = new Vector3[gameplayManager.GameplayConfig.DefaultHandCount];
        Vector3 centerPoint = new Vector3(boardProxy.HandPoint.transform.position.x,boardProxy.HandPoint.transform.position.y + gameplayManager.GameplayConfig.HandYPosition);
        float deltaWidth = gameplayManager.GameplayConfig.HandWidth / slotPoints.Length;
        Vector3 leftSide = centerPoint + Vector3.left * deltaWidth * (slotPoints.Length / 2);

        for (int i = 0; i < slotPoints.Length; i++)
        {
            Vector3 slotPosition = leftSide + Vector3.right * deltaWidth * i;
            slotPoints[i] = slotPosition;
        }
    }
    
    private void UpdateHandCardPositions()
    {
        
    }

    private void SelectCard()
    {
        
    }
}