using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuildWars2WorldEventTracker
{
    /// <summary>
    /// Interaction logic for GWCheckBox.xaml
    /// </summary>
    public partial class GWCheckBox : UserControl
    {
        public static readonly DependencyProperty IsCheckedProperty = 
            DependencyProperty.Register("IsChecked", typeof (bool), typeof (GWCheckBox)
            ,new FrameworkPropertyMetadata(OnIsCheckedChanged));

        public bool IsChecked
        {
            get { return (bool) GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty,value); }
        }

        private static void OnIsCheckedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var me = (GWCheckBox)sender;
            me.IsChecked = (bool) e.NewValue;
            me.Tick.Visibility = me.IsChecked ? Visibility.Visible : Visibility.Hidden;
        }

        public GWCheckBox()
        {
            InitializeComponent();
        }

        private void CheckBoxMouseEnter(object sender, MouseEventArgs e)
        {
            Glow.Visibility = Visibility.Visible;
        }

        private void CheckBoxMouseLeave(object sender, MouseEventArgs e)
        {
            Glow.Visibility = Visibility.Hidden;
        }

        private void CheckBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsChecked = !IsChecked;
        }
    }
}
