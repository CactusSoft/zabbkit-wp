using System.Windows.Media;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public static class Extensions
    {
        public static Brush ToBrush(this TriggerPriority triggerPriority)
        {
            Color color;

            switch (triggerPriority)
            {
                case TriggerPriority.Average:
                    color = Color.FromArgb(0xFF, 255, 168, 114);
                    break;
                case TriggerPriority.Disaster:
                    color = Color.FromArgb(0xFF, 253, 34, 38);
                    break;
                case TriggerPriority.High:
                    color = Color.FromArgb(0xFF, 255, 133, 134);
                    break;
                case TriggerPriority.Warning:
                    color = Color.FromArgb(0xFF, 255, 248, 141);
                    break;
                case TriggerPriority.Information:
                    color = Color.FromArgb(0xFF, 205, 244, 255);
                    break;
                case TriggerPriority.NotClassified:
                    color = Color.FromArgb(0xFF, 128, 128, 128);
                    break;
                default:
                    color = Colors.Transparent;
                    break;
            }

            return new SolidColorBrush(color);
        }
    }
}
