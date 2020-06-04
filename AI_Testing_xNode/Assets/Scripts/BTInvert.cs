[BTComposite(typeof(BTInvert))]
public class BTInvert : BTNode
{
    [Input] public BTResult inResult; // The indata for the node in Xnode


    /// <summary>
    /// Inverts the result of <see cref="BTResult"/> passed into <see cref="inResult"/>
    /// </summary>
    /// <returns></returns>
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
