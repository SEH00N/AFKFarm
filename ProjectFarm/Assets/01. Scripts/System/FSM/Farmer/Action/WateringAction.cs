namespace H00N.FSM.Farmers
{
    public class WateringAction : FarmingAction
    {
        public override void FarmingBehaviour()
        {
            targetField.Watering();
            brainParam.ActionFinished = true;
        }
    }
}
