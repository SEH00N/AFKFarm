using H00N.Farms;

namespace H00N.FSM.Farmers
{
    public class PlantAction : FarmingAction
    {
        public override void FarmingBehaviour()
        {
            targetField.PlantCrop(targetFarm.CropData);
            brainParam.ActionFinished = true;
        }
    }
}
