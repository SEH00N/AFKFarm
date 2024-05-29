using UnityEngine;

namespace H00N.FSM.Farmer
{
    public class EmptyAction : FarmerAction
    {
        protected override bool ActionPossibility()
        {
            return false;
        }
    }
}
