using System.Windows;
using System.Windows.Controls;
using CactusSoft.Stierlitz.Application.ViewModels;

namespace CactusSoft.Stierlitz.Application.UserControls
{
    public partial class EventUserControl : UserControl
    {

        public static readonly DependencyProperty EventProperty =
            DependencyProperty.Register("Event", typeof (EventViewModel), typeof (EventUserControl), new PropertyMetadata(default(EventViewModel)));

        public EventUserControl()
        {
            InitializeComponent();
        }

        public EventViewModel Event
        {
            get
            {
                return (EventViewModel)GetValue(EventProperty);
            }
            set
            {
                SetValue(EventProperty, value);
            }
        }

    }
}
