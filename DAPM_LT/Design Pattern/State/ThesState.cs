using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_LT.State
{
    public class ThesState
    {
        public string CurrentState { get; private set; }

        public void SetState(string state)
        {
            CurrentState = state;
        }
    }
}