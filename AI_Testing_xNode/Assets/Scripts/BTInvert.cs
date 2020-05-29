[BTComposite(typeof(BTInvert))]
public class BTInvert : BTNode
{
    [Input] public BTResult inResult;

    public override BTResult Execute()
    {
        BTResult result = GetInputValue("inResult", BTResult.SUCCESS);
        if (result == BTResult.RUNNING)
        {
            return BTResult.RUNNING;
        }
        else return result == BTResult.SUCCESS ? BTResult.FAILURE : BTResult.SUCCESS;
    }
}
