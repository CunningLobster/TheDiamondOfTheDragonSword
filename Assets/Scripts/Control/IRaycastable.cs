﻿namespace RPG.Control
{
    public interface IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController);
    }
}