[BTComposite(typeof(BTReturnFailure))]
public class BTReturnFailure : BTNode
{
    [Input] public BTResult inResult;

    public override BTResult Execute()
    {
        BTResult result = GetInputValue("inResult", BTResult.SUCCESS);
        if (result == BTResult.RUNNING)
        {
            return BTResult.RUNNING;
        }
        else return BTResult.FAILURE;
    }
}
