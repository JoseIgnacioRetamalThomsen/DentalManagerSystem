using DentalManagerSys.Views.Appointments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DentalManagerSys.Views.Form
{
    public class ApointmetsView : StackPanel
    {
        public event EventHandler<EmptySlotTapped> OnEmptySlotTapped;

        int hours = 16;
        int slotPerHour = 4;
        int column = 7;
        int starhour = 8;
       public static List<int> slot = new List<int>() { 0, 15, 30, 45 };
        List<String> days = new List<string>() { "Monday", "Tueday", "Wenesday", "Thursday", "Friday", "Saturday" };
        Grid ApoGrid = new Grid();
        int columnwidth = 150;
        public ApointmetsView()
        {
            ApoGrid.HorizontalAlignment = HorizontalAlignment.Center;
            GuenerateAppGrid();
            this.Children.Add(ApoGrid);
            
        }

        private void GuenerateAppGrid()
        {
            //create row
            //for 12 hours, 08:00 to 20:00, 4 slot of 15 mint per hour 
            for (int i = 0; i <= hours * slotPerHour+1; i++)
            {
                ApoGrid.RowDefinitions.Add(new RowDefinition() { });
            }

            //create columns
            for (int i = 0; i < column; i++)
            {
                ApoGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new Windows.UI.Xaml.GridLength(columnwidth)  });
            }
            starhour--;
            //add time to first colum
            for (int i = 1; i <= hours * slotPerHour+1; i++)
            {
                Border border = new Border()
                {
                    Height = 20,
                    Width = columnwidth,
                    BorderThickness = new Windows.UI.Xaml.Thickness(1, 1, 1, 0),
                    BorderBrush = new SolidColorBrush(Colors.Black)
                    //Background = new SolidColorBrush(Colors.Black)
                };

                // border.SetValue(Grid.RowProperty, i);
                // border.SetValue(Grid.ColumnProperty, 0);
                Grid.SetColumn(border, 0);
                Grid.SetRow(border, i);

                if ((i-1) % 4 == 0) starhour++;
                TextBlock label = new TextBlock()
                {
                    Text = "" + starhour + ":" + slot[(i-1) % 4],
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center

                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, i);
                //label.SetValue(Grid.RowProperty, i);
                // label.SetValue(Grid.ColumnProperty, 0);
                ApoGrid.Children.Add(label);
                ApoGrid.Children.Add(border);




            }
            // add day headers
            for (int i = 1; i < column; i++)
            {
                TextBlock label = new TextBlock()
                {
                    Text = "" + days[i - 1],
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center

                };
                Grid.SetColumn(label, i);
                Grid.SetRow(label, 0);
                ApoGrid.Children.Add(label);

            }

            //add border to all other
            for (int row = 1; row < hours * slotPerHour; row++)
            {
                for (int col = 1; col < column; col++)
                {
                    Border border = new Border()
                    {
                        Height = 20,
                        Width = columnwidth,
                        BorderThickness = new Windows.UI.Xaml.Thickness(1, 1, 1, 0),
                        BorderBrush = new SolidColorBrush(Colors.Black)
                        //Background = new SolidColorBrush(Colors.Black)
                    };

                    // border.SetValue(Grid.RowProperty, i);
                    // border.SetValue(Grid.ColumnProperty, 0);
                    Grid.SetColumn(border, col);
                    Grid.SetRow(border, row);

                    TextBlock label = new TextBlock();
                    label.Text = "Free";
                    label.TextAlignment = TextAlignment.Center;

                    Grid.SetColumn(label, col);
                    Grid.SetRow(label, row);

                    ApoGrid.Children.Add(border);
                    ApoGrid.Children.Add(label);
                    label.Tapped += EmptySlot_Tapped;

                }
            }

        }

        private void EmptySlot_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
           // Debug.WriteLine("tapped");
            FrameworkElement b = (FrameworkElement)sender;

            //try
            //{
            //     b = (Border)sender;
            //}catch
            //{
            //    b = (TextBlock)sender;
            //}
            //Debug.WriteLine("d" + b.GetValue(Grid.ColumnProperty) + b.GetValue(Grid.RowProperty));
            EmptySlotTapped evnt = new EmptySlotTapped();
            evnt.X = (int)b.GetValue(Grid.ColumnProperty);
            evnt.Y = (int)b.GetValue(Grid.RowProperty)-1;
            OnEmpty(evnt);
        }

        public void AddApointment(List<int> timeSlot, string name, int day)
        {

            Border border = new Border()
            {
                //Height = 20,
                Width = columnwidth,
                // BorderThickness = new Windows.UI.Xaml.Thickness(1, 1, 1, 0),
                //  BorderBrush = new SolidColorBrush(Colors.Black)
                Background = new SolidColorBrush(Colors.LightGreen)
            };

            // border.SetValue(Grid.RowProperty, i);
            // border.SetValue(Grid.ColumnProperty, 0);
            Grid.SetColumn(border, day);
            Grid.SetRow(border, timeSlot[0]);

            Grid.SetRowSpan(border, 2);

            TextBlock label = new TextBlock()
            {
                Text = name,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center

            };
            Grid.SetColumn(label, day);
            Grid.SetRow(label, timeSlot[0]);
            //label.SetValue(Grid.RowProperty, i);
            // label.SetValue(Grid.ColumnProperty, 0);

            ApoGrid.Children.Add(border);
            ApoGrid.Children.Add(label);
            border.Tapped += Border_Tapped;
            label.Tapped += Border_Tapped;

        }

        private void Border_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement b = (FrameworkElement)sender;

            //try
            //{
            //     b = (Border)sender;
            //}catch
            //{
            //    b = (TextBlock)sender;
            //}
           // Debug.WriteLine("d" + b.GetValue(Grid.ColumnProperty)+ b.GetValue(Grid.RowProperty));
           
        }

        protected virtual void OnEmpty(EmptySlotTapped e)
        {
            OnEmptySlotTapped?.Invoke(this, e);
        }
    }
}
