using UnityEngine;

namespace H00N.FSM.Farmer
{
    public abstract class FarmerAction : FSMAction
    {
        protected BrainParam brainParam = null;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);

            brainParam = brain.GetFSMParam<BrainParam>();
        }

        public override void EnterState()
        {
            base.EnterState();

            if(ActionPossibility() == false)
                brainParam.ActionFinished = true;
        }

        protected abstract bool ActionPossibility();
    }
}
