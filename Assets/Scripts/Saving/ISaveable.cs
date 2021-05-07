using System.Collections;
using UnityEngine;

namespace RPG.Saving
{
    public interface ISaveable
    {
        public object CaptureState();
        public void RestoreState(object state);
    }
}