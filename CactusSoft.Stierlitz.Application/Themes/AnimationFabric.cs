using System;
using System.Windows;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Themes
{
    public class AnimationFabric
    {
        public RadAnimation ItemAddedAnimation
        {
            get
            {
                return new RadMoveAnimation
                {
                    StartPoint = new Point(500, 0),
                    EndPoint = new Point(0, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    Easing = new CubicEase { EasingMode = EasingMode.EaseOut },
                };
            }
        }
    }
}
