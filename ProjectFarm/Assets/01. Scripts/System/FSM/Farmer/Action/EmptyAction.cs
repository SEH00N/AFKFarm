using UnityEngine;

namespace H00N.FSM.Farmers
{
    public class EmptyAction : FarmerAction
    {
        protected override bool ActionPossibility()
        {
            return false;
        }
    }
}
