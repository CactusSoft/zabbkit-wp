using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CactusSoft.Stierlitz.Application.Interactivity
{
    public class ItemsControlBehavior : Behavior<ItemsControl>
    {
        private ScrollViewer _scrollViewer;
        private double _initialHeight;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OntemsControlLoaded;
            ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (_scrollViewer == null)
            {
                return;
            }

            AssociatedObject.UpdateLayout();
            if (AssociatedObject.ActualHeight > _initialHeight)
            {
                _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
        }

        private void OntemsControlLoaded(object sender, RoutedEventArgs e)
        {
            _initialHeight = AssociatedObject.ActualHeight;
            _scrollViewer = AssociatedObject.Parent as ScrollViewer;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
            AssociatedObject.Loaded -= OntemsControlLoaded;
        }

    }
}
