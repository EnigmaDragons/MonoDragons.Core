﻿using System;

namespace MonoDragons.Core.Engine
{
    public interface IScene
    {
        void Update(TimeSpan delta);
        void Draw();
    }
}
