using System;

namespace MonoDragons.Core.Animations
{
    public static class AnimationExtensions
    {
        public static IAnimation Start(this IAnimation animation)
        {
            animation.Start(() => { });
            return animation;
        }
        
        public static IAnimation Started(this IAnimation animation)
        {
            animation.Start();
            return animation;
        }
        
        public static IAnimation Started(this IAnimation animation, Action onFinished)
        {
            animation.Start(onFinished);
            return animation;
        }
    }
}