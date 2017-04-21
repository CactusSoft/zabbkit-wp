using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views.FavoritesHub
{
    public partial class GraphsView : UserControl
    {
        public GraphsView()
        {
            InitializeComponent();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, GraphsRadDataBoundListBox);
        }
    }
}
