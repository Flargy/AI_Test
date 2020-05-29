using XNode;

[BTComposite(typeof(BTUntilSuccess))]
public class BTUntilSuccess : BTNode
{
    [Input] public BTResult inResult;

    BehaviourTreeManager behaviourTreeManager;

    public override void OnStart()
    {
        behaviourTreeManager = BehaviourTreeManager.GetInstance();
    }

    public override BTResult Execute()
    {
        NodePort inPort = GetPort("inResult");
        if (inPort != null && inPort.GetConnections().Count != 0)
        {
            NodePort connection = inPort.GetConnection(0);

            if (connection != null)
            {
                BTResult result = (BTResult)connection.GetOutputValue();
                if (result == BTResult.SUCCESS)
                {
                    BehaviourTreeRuntimeData.RemoveRunningNode(context, this);
                }
                else if (result == BTResult.RUNNING)
                {
                    return BTResult.RUNNING;
                }
                else
                {
                    BehaviourTreeRuntimeData.AddRunningNode(context, this);
                    return BTResult.RUNNING;
                }
            }
        }
        return BTResult.SUCCESS;
    }
}
