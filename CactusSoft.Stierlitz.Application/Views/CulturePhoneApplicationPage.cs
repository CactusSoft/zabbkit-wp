using System.Globalization;
using System.Windows.Markup;
using Microsoft.Phone.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public class CulturePhoneApplicationPage : PhoneApplicationPage
    {
        protected CulturePhoneApplicationPage()
        {
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);
        }
    }
}
