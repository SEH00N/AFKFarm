namespace H00N.FSM.Farmers
{
    public class HarvestAction : FarmingAction
    {
        public override void FarmingBehaviour()
        {
            targetField.Harvest();
            brainParam.ActionFinished = true;
        }
    }
}
