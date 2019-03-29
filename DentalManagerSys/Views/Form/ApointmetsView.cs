using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DentalManagerSys.Views.Form
{
    public class ApointmetsView : StackPanel
    {
        int hours = 12;
        int slotPerHour = 4;
        int column = 7;
        int starhour = 8;
        List<int> slot = new List<int>() { 0, 15, 30, 45 };
        List<String> days = new List<string>() { "Monday","Tueday","Wenesday","Thursday","Friday","Saturday"};
        Grid ApoGrid = new Grid();
        int columnwidth = 150;
        public ApointmetsView()
        {
            GuenerateAppGrid();
            this.Children.Add(ApoGrid);
        }

        private void GuenerateAppGrid()
        {
            //create row
            //for 12 hours, 08:00 to 20:00, 4 slot of 15 mint per hour 
            for (int i = 0; i <= hours * slotPerHour; i++)
            {
                ApoGrid.RowDefinitions.Add(new RowDefinition() {});
            }

            //create columns
            for (int i = 0; i < column; i++)
            {
                ApoGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=new Windows.UI.Xaml.GridLength(columnwidth) });
            }

            //add time to first colum
            for (int i = 1; i <= hours * slotPerHour; i++)
            {
                Border border = new Border()
                {
                    Height = 20,
                    Width = columnwidth,
                    BorderThickness = new Windows.UI.Xaml.Thickness(1,1,1,0),
                    BorderBrush = new SolidColorBrush(Colors.Black)
                    //Background = new SolidColorBrush(Colors.Black)
                };

               // border.SetValue(Grid.RowProperty, i);
               // border.SetValue(Grid.ColumnProperty, 0);
                Grid.SetColumn(border, 0);
                Grid.SetRow(border, i);

                if (i % 4 == 0) starhour++;
                TextBlock label = new TextBlock()
                {
                    Text = ""+ starhour +":" + slot[i%4],
                    VerticalAlignment=Windows.UI.Xaml.VerticalAlignment.Center,
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
            for(int i =1;i<column;i++)
            {
                TextBlock label = new TextBlock()
                {
                    Text = "" +days[i-1],
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center

                };
                Grid.SetColumn(label, i);
                Grid.SetRow(label, 0);
                ApoGrid.Children.Add(label);

            }

            //add border to all other
            for(int row =1;row < hours * slotPerHour;row++)
            {
                for(int col=1;col<column;col++)
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

                    ApoGrid.Children.Add(border);

                }
            }

        }
    }
}
