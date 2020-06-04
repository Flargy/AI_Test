/// <summary>
/// Forces the return value of the behavior tree path to <see cref="BTResult.SUCCESS"/>
/// </summary>
[BTComposite(typeof(BTReturnSuccess))]
public class BTReturnSuccess : BTNode
{
    [Input] public BTResult inResult;

    public override BTResult Execute()
    {
        BTResult result = GetInputValue("inResult", BTResult.SUCCESS);
        if (result == BTResult.RUNNING)
        {
            return BTResult.RUNNING;
        } 
        else return BTResult.SUCCESS;
    }
}
