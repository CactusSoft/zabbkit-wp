using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Interactivity
{
    /// <summary>
    /// <see cref="Behavior">Behavior</see> that updates <see cref="PasswordBox">PasswordBox</see> Password <see cref="Binding">binding</see> source.
    /// </summary>
    public class PasswordBoxUpdateBindingBehavior : Behavior<RadPasswordBox>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PasswordChanged += OnTextChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PasswordChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (AssociatedObject == null)
            {
                return;
            }

            var binging = AssociatedObject.GetBindingExpression(RadPasswordBox.PasswordProperty);
            
            if (binging != null)
            {
                binging.UpdateSource();
            }
        }
    }
}
