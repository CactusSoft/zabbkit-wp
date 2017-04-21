using System.Globalization;
using System.Windows.Markup;

namespace CactusSoft.Stierlitz.Application.Views.MainHub
{
    public partial class TimelineView
	{
        public TimelineView()
        {
            InitializeComponent();
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);
        }
	}
}