using System.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Controls
{
    public class SafeHyperlinkButton : HyperlinkButton
    {
        public SafeHyperlinkButton()
        {
            TargetName = "_blank";
        }

        protected override void OnClick()
        {
            try
            {
                base.OnClick();
            }
            catch(System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}