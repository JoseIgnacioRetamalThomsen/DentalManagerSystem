using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DentalManagerSys.Print
{
    public class TreatmentPlanPrint : StackPanel
    {
        private string title;
        private string subTitle;
        private string doctorName;

        private string date;

        private string patienNameLabelString;
        private string patienAddressLabelString;
        private string TreatmentNumberLabelString;
        private string patientName;
        private string patientAddress;
        private string treatmentNumber;


        private string treatmentLabel;
        private string teathNumberLabel;
        private string amountLabel;
        private string totalLabel;

        private List<TreatmentOnPlan> treatments;

        private User user;
        private Customer customer;
        #region constructors
        public TreatmentPlanPrint(User _user)
        {
            this.Width = 800;

            user = _user;
            // this.Height = 5000;
            this.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;

            InitStrings();
            InitGrid();
            InitTreatments();
            SetTitle();
            SetDate();
            SetPatientData();
            SetTreatmentNum();
            SetTreatments();


        }

        public TreatmentPlanPrint(Customer c, List<TreatmentOnPlan> list, User u)
        {
            this.Width = 800;
            // this.Height = 5000;
            this.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            customer = c;
            user = u;
            InitStrings(u, c);
            InitGrid();
            InitTreatments(list);
            SetTitle();
            SetDate();
            SetPatientData();
            SetTreatmentNum();
            SetTreatments();
        }
        #endregion

        #region load dataa
        private void InitTreatments()
        {
            treatments = new List<TreatmentOnPlan>();
            treatments.Add(new TreatmentOnPlan(1, 1, 1, 25000, DateTime.Now, 3, "none", false, "Treattment one 2dasd "));
            treatments.Add(new TreatmentOnPlan(1, 1, 1, 25000, DateTime.Now, 3, "none", false, "Treattment one 2dasd "));
            treatments.Add(new TreatmentOnPlan(1, 1, 1, 25000, DateTime.Now, 3, "none", false, "Treattment one 2dasd "));
            treatments.Add(new TreatmentOnPlan(1, 1, 1, 25000, DateTime.Now, 3, "none", false, "Treattment one 2dasd "));
            treatments.Add(new TreatmentOnPlan(1, 1, 1, 25000, DateTime.Now, 3, "none", false, "Treattment one 2dasd "));

        }
        private void InitTreatments(List<TreatmentOnPlan> list)
        {
            treatments = new List<TreatmentOnPlan>(list);


        }


        private void InitStrings()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");



            //static
            patienNameLabelString = resourceLoader.GetString("/Strings/patienNameLabelString/Text");
            patienAddressLabelString = resourceLoader.GetString("/Strings/patienAddressLabelString/Text");
            TreatmentNumberLabelString = resourceLoader.GetString("/Strings/TreatmentNumberLabelString/Text");

            treatmentLabel = resourceLoader.GetString("/Strings/treatmentLabel/Text");
            teathNumberLabel = resourceLoader.GetString("/Strings/teathNumberLabel/Text");
            amountLabel = resourceLoader.GetString("/Strings/amountLabel/Text");
            totalLabel = resourceLoader.GetString("/Strings/totalLabel/Text");



            title = resourceLoader.GetString("/Strings/titlePrintPreview/Text");
            subTitle = resourceLoader.GetString("/Strings/subTitlePlanPreview/Text");

            //dinamic
            doctorName = "Dr. Manual Arraigado";
            date = "18/03/2019";

            patientName = "Marco Alfonso Gonazales";
            patientAddress = "13 Shop street , Vina Del Mar";
            treatmentNumber = "1";


        }
        private void InitStrings(User u, Customer c)
        {

            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Strings");



            //static
            patienNameLabelString = resourceLoader.GetString("/Strings/patienNameLabelString/Text");
            patienAddressLabelString = resourceLoader.GetString("/Strings/patienAddressLabelString/Text");
            TreatmentNumberLabelString = resourceLoader.GetString("/Strings/TreatmentNumberLabelString/Text");

            treatmentLabel = resourceLoader.GetString("/Strings/treatmentLabel/Text");
            teathNumberLabel = resourceLoader.GetString("/Strings/teathNumberLabel/Text");
            amountLabel = resourceLoader.GetString("/Strings/amountLabel/Text");
            totalLabel = resourceLoader.GetString("/Strings/totalLabel/Text");

            title = resourceLoader.GetString("/Strings/titlePrintPreview/Text");
            subTitle = resourceLoader.GetString("/Strings/subTitlePlanPreview/Text");


            //dynamic
            doctorName = "Dr. " + u.Name;
            date = DateTime.Now.ToString("dd/MM/yyyy");

            patientName = c.name + " " + c.surname;
            patientAddress = c.street + ", " + c.city + ", " + c.country + ". " + c.postcode;
            treatmentNumber = "1";


        }

        #endregion

        public string GetStringEmail()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} \n", title);
            sb.AppendFormat("{0} \n", subTitle);
            sb.AppendFormat("{0} \n\n", doctorName);

            sb.AppendFormat("Patient: {0} \n", patientName);
            sb.AppendFormat("Address: {0} \n", patientAddress);
            sb.AppendFormat("Threatment num: {0} \n\n\n", treatmentNumber);
            
            sb.AppendFormat("{0,-35} {1,-20} {2,-10} \n",
                                        treatmentLabel, teathNumberLabel, amountLabel);

            sb.Append("------------------------------------------------------------------------------\n");
            decimal total = 0;
            foreach (TreatmentOnPlan trm in treatments)
            {
                sb.AppendFormat("{0,-35} {1,-20} {2,-10} \n",
                                        trm.Name, trm.Tooth, trm.Price);
                total += trm.Price;
            }
            sb.Append("------------------------------------------------------------------------------\n");
            sb.AppendFormat("\n{0,50} {1,-10}  \n",
                                     totalLabel,total);

            return sb.ToString();
        }

        //row 5
        private void SetTreatmentNum()
        {
            StackPanel tnSP = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tn1 = new TextBlock()
            {
                Text = TreatmentNumberLabelString,
                FontSize = 24
            };
            TextBlock tn2 = new TextBlock()
            {
                Text = treatmentNumber,
                FontSize = 24,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Right
            };
            tnSP.Children.Add(tn1);
            tnSP.Children.Add(tn2);
            Grid.SetColumn(tnSP, 0);
            Grid.SetRow(tnSP, 5);
            this.Children.Add(tnSP);
        }
        //row6
        private void SetTreatments()
        {
            Grid grid = new Grid()
            {
                Margin = new Windows.UI.Xaml.Thickness(0, 20, 0, 0),
                //  BorderThickness = new Windows.UI.Xaml.Thickness(20),
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,

            };


            ColumnDefinition c1 = new ColumnDefinition
            {
                Width = new Windows.UI.Xaml.GridLength(400),

            };
            ColumnDefinition c2 = new ColumnDefinition
            {
                Width = new Windows.UI.Xaml.GridLength(100),

            };
            ColumnDefinition c3 = new ColumnDefinition
            {
                Width = new Windows.UI.Xaml.GridLength(300)
            };




            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            grid.ColumnDefinitions.Add(c3);


            RowDefinition r1 = new RowDefinition();

            RowDefinition r2 = new RowDefinition();

            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);

            //labels (first row)
            TextBlock label1 = new TextBlock()
            {
                Text = treatmentLabel,
                FontSize = 24,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch,
                Width = 400

            };
            Grid.SetRow(label1, 0);
            Grid.SetColumn(label1, 0);
            grid.Children.Add(label1);

            //labels (first row)
            TextBlock label2 = new TextBlock()
            {
                Text = teathNumberLabel,
                FontSize = 24,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                Width = 100

            };
            Grid.SetRow(label2, 0);
            Grid.SetColumn(label2, 1);
            grid.Children.Add(label2);

            //labels (first row)
            TextBlock label3 = new TextBlock()
            {
                Text = amountLabel,
                FontSize = 24,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                Width = 100

            };
            Grid.SetRow(label3, 0);
            Grid.SetColumn(label3, 2);
            grid.Children.Add(label3);




            /*
            RowDefinition r2 = new RowDefinition();
            RowDefinition r3 = new RowDefinition();


            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);
            grid.RowDefinitions.Add(r3);*/
            //add indivudual treatment
            int rowCount = 2;
            decimal total = 0;
            foreach (TreatmentOnPlan t in treatments)
            {
                grid.RowDefinitions.Add(new RowDefinition());

                //first treat
                TextBlock t1 = new TextBlock()
                {
                    Text = t.Name,
                    FontSize = 18,
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                    Width = 400

                };
                Grid.SetRow(t1, rowCount);
                Grid.SetColumn(t1, 0);
                grid.Children.Add(t1);

                //code
                TextBlock t2 = new TextBlock()
                {
                    Text = "" + t.Tooth,
                    FontSize = 18,
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                    Width = 100
                };

                Grid.SetRow(t2, rowCount);
                Grid.SetColumn(t2, 1);
                grid.Children.Add(t2);

                //price
                TextBlock t3 = new TextBlock()
                {
                    Text = "" + t.ShowPrice,
                    FontSize = 18,
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                    TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                    Width = 250
                };

                Grid.SetRow(t3, rowCount);
                Grid.SetColumn(t3, 2);
                grid.Children.Add(t3);

                rowCount++;
                total += t.Price;


            }

            //total

            grid.RowDefinitions.Add(new RowDefinition());
            TextBlock totalL = new TextBlock()
            {
                Text = totalLabel,
                FontSize = 18,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                Width = 100

            };



            Grid.SetRow(totalL, rowCount);
            Grid.SetColumn(totalL, 1);
            grid.Children.Add(totalL);

            TextBlock totalA = new TextBlock()
            {
                Text = total.ToString("C", CultureInfo.CurrentCulture),
                FontSize = 18,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
                Width = 100

            };



            Grid.SetRow(totalA, rowCount);
            Grid.SetColumn(totalA, 2);
            grid.Children.Add(totalA);



            Grid.SetRow(grid, 6);
            Grid.SetColumn(grid, 0);
            Grid.SetColumnSpan(grid, 3);
            Children.Add(grid);

        }



        //row1
        private void SetDate()
        {
            StackPanel dataSP = new StackPanel()
            {
                Height = 50
            };

            TextBlock dateTB = new TextBlock()
            {
                Text = "Date: " + date,
                FontSize = 28,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Left
            };
            dataSP.Children.Add(dateTB);
            Grid.SetColumn(dataSP, 0);
            Grid.SetRow(dataSP, 1);
            Grid.SetColumnSpan(dataSP, 2);
            this.Children.Add(dataSP);
        }

        //row3
        private void SetPatientData()
        {
            Grid patienDataGrid = new Grid();
            Grid.SetRow(patienDataGrid, 3);
            Grid.SetColumn(patienDataGrid, 0);
            Grid.SetColumnSpan(patienDataGrid, 2);
            this.Children.Add(patienDataGrid);
            ColumnDefinition c1 = new ColumnDefinition();

            ColumnDefinition c2 = new ColumnDefinition();

            patienDataGrid.ColumnDefinitions.Add(c1);
            patienDataGrid.ColumnDefinitions.Add(c2);


            RowDefinition r1 = new RowDefinition();
            RowDefinition r2 = new RowDefinition();
            RowDefinition r3 = new RowDefinition();


            patienDataGrid.RowDefinitions.Add(r1);
            patienDataGrid.RowDefinitions.Add(r2);
            patienDataGrid.RowDefinitions.Add(r3);

            //labels
            TextBlock nameTB = new TextBlock()
            {
                Text = patienNameLabelString,
                FontSize = 24
            };
            Grid.SetColumn(nameTB, 0);
            Grid.SetRow(nameTB, 0);
            patienDataGrid.Children.Add(nameTB);

            TextBlock addressTB = new TextBlock()
            {
                Text = patienAddressLabelString,
                FontSize = 24
            };
            Grid.SetColumn(addressTB, 0);
            Grid.SetRow(addressTB, 1);
            patienDataGrid.Children.Add(addressTB);

            // data
            TextBlock nameDTB = new TextBlock()
            {
                Text = patientName,
                FontSize = 24
            };
            Grid.SetColumn(nameDTB, 1);
            Grid.SetRow(nameDTB, 0);
            patienDataGrid.Children.Add(nameDTB);
            // data
            TextBlock addressDTB = new TextBlock()
            {
                Text = patientAddress,
                FontSize = 24
            };
            Grid.SetColumn(addressDTB, 1);
            Grid.SetRow(addressDTB, 1);
            patienDataGrid.Children.Add(addressDTB);
        }


        //row0
        private void SetTitle()
        {
            StackPanel titleSP = new StackPanel()
            {
                Height = 150
            };

            //set position on grid
            this.Children.Add(titleSP);
            titleSP.Children.Add(new TextBlock()
            {
                Text = title,
                FontSize = 36,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center

            });

            titleSP.Children.Add(new TextBlock()
            {
                Text = subTitle,
                FontSize = 36,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center
            });

            titleSP.Children.Add(new TextBlock()
            {
                Text = doctorName,
                FontSize = 24,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center
            });



        }

        Grid topGrid;
        private void InitGrid()
        {
            topGrid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();

            ColumnDefinition c2 = new ColumnDefinition();
            ColumnDefinition c3 = new ColumnDefinition();

            topGrid.ColumnDefinitions.Add(c1);
            topGrid.ColumnDefinitions.Add(c2);
            topGrid.ColumnDefinitions.Add(c3);

            RowDefinition r1 = new RowDefinition();
            RowDefinition r2 = new RowDefinition();
            RowDefinition r3 = new RowDefinition();
            RowDefinition r4 = new RowDefinition();
            RowDefinition r5 = new RowDefinition();
            RowDefinition r6 = new RowDefinition();


            topGrid.RowDefinitions.Add(r1);
            topGrid.RowDefinitions.Add(r2);
            topGrid.RowDefinitions.Add(r3);
            topGrid.RowDefinitions.Add(r4);
            topGrid.RowDefinitions.Add(r5);
            topGrid.RowDefinitions.Add(r6);


        }
    }


}
