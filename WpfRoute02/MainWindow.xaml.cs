using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfRoute02
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool EditButton = false;
        bool AddButton = false;
        public MainWindow()
        {

            InitializeComponent();
            FillStreetBox();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void FillStreetBox()
        {
            SqlControl st = new SqlControl();
            string Query = "Select Street from Streets";
            st.ExecQuery(Query);

            foreach (DataRow dr in st.DT.Rows)
            {
                cboxStreet.Items.Add(dr["Street"].ToString());
            }

        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
         
        }
        private void Update_DataGrid()
        {
            btnLoad.IsEnabled = false;
            SqlControl st = new SqlControl();
            string StreetSelected = " ";
            string Query = " ";

            StreetSelected = cboxStreet.SelectedItem.ToString();

            if (StreetSelected != " ")
            {
                Query = "Select Id,Street, HouseNum, Unit, Code, TVGuide, Delivery, Updated, Path, Seq from Route02 where Street = " + '"' + cboxStreet.SelectedItem.ToString() + '"';
            }
            else
            {
                Query = "Select Id, Street, HouseNum, Unit, Code, TVGuide, Delivery, Updated, Path, Seq from Route02";
            }
            st.ExecQuery(Query);
            DataGrid1.ItemsSource = st.DT.DefaultView;

        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;

            // var txt = row_selected["Updated"].ToString();
            this.txtHouseNum.Text = row_selected["HouseNum"].ToString();
            this.txtUnit.Text = row_selected["Unit"].ToString();
            this.cboxCode.Text = row_selected["Code"].ToString();

            if ((bool)row_selected["TVGuide"])
            { this.chkboxTVGuide.IsChecked = true; }
            else
            { this.chkboxTVGuide.IsChecked = false; }

            this.txtDelivery.Text = row_selected["Delivery"].ToString();
            this.txtUpdated.Text = row_selected["Updated"].ToString();
            txtId.Text = row_selected["Id"].ToString();
        }

        private void RouteButton_Click(object sender, RoutedEventArgs e)
        {
            RouteList02 rt = new RouteList02();
            rt.ShowDialog();
        }

        private void ClearBoxes()
        {
            this.txtHouseNum.Text = "";
            this.txtUnit.Text = "";
            this.cboxCode.Text = "";
            this.chkboxTVGuide.IsChecked = false;
            this.txtDelivery.Text = "";
            this.txtUpdated.Text = "";
        }

        public void TurnsButtonsOff()
        {
            btnInsert.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnClose.IsEnabled = false;
            btnRoadList.IsEnabled = false;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //ValidateCustomer();
            SqlControl sss = new SqlControl();
            DateTime today = DateTime.Today;
            string Query = "";

            Query = "insert into Route02 (Street, HouseNum, Unit, Code, TVGuide, Delivery, Updated, Path, Seq) " +
            "values ('" + cboxStreet.Text + "','"
            + int.Parse(txtHouseNum.Text) + "','"
            + txtUnit.Text + "','"
            + cboxCode.Text + "','"
            + chkboxTVGuide.IsChecked + "','"
            + txtDelivery.Text + "','"
            + today.ToString("d") + "','"
            + 0 + "','"
            + 0 + "')";

            sss.ExecNonQuery(Query);
            Update_DataGrid();
            MessageBox.Show("Customer Added");
            TurnsButtonsOn();

        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //ValidateCustomer();
            SqlControl sss = new SqlControl();
            DateTime today = DateTime.Today;
            string Query = "";

            Query = "update Route02 set Street = '" + cboxStreet.Text
               + "', HouseNum = '" + int.Parse(txtHouseNum.Text)
               + "', Unit = '" + txtUnit.Text
               + "', Code = '" + cboxCode.Text
               + "', TVGuide = '" + chkboxTVGuide.IsChecked
               + "', Delivery = '" + txtDelivery.Text
               + "', Updated = '" + today.ToString("d")
               + "'  where Id = '" + int.Parse(txtId.Text) + "'";

            sss.ExecNonQuery(Query);
            Update_DataGrid();
            MessageBox.Show("Customer Added");
            TurnsButtonsOn();

        }

        public void ValidateCustomer()
        {
            if (this.cboxStreet.Text != "")
            {
                string Street = cboxStreet.Text;
            }
            else
            {
                MessageBox.Show("Please select a valid Street");
            }
            if (this.txtHouseNum.Text != "")
            {
                int HouseNum = int.Parse(txtHouseNum.Text);
            }
            else
            {
                MessageBox.Show("Please enter a valid House Number");
            }
            if (this.cboxCode.Text != "")
            {
                string Code = cboxCode.Text;
            }
            else
            {
                MessageBox.Show("Please select a valid Code");
            }

        }
        public void TurnsButtonsOn()
        {
            btnInsert.IsEnabled = true;
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnClose.IsEnabled = true;
            btnRoadList.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Cancel Button Clicked");
        }

        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ValidateCustomer();
            SqlControl sss = new SqlControl();
            string messageBoxText = "";
            string Query = "";
            if (AddButton)
            {
                Query = "insert into Route02 (Street, HouseNum, Unit, Code, TVGuide, Delivery, Path, Seq) " +
                "values ('" + cboxStreet.Text + "','"
                + int.Parse(txtHouseNum.Text) + "','"
                + txtUnit.Text + "','"
                + cboxCode.Text + "','"
                + chkboxTVGuide.IsChecked + "','"
                + txtDelivery.Text + "','"
                + 0 + "','"
                + 0 + "')";

                messageBoxText = "Customer Added";
            }
            else if (EditButton)
            {
                Query = "update Route02 set Street = '" + cboxStreet.Text
                    + "', HouseNum = '" + int.Parse(txtHouseNum.Text)
                    + "', Unit = '" + txtUnit.Text
                    + "', Code = '" + cboxCode.Text
                    + "', TVGuide = '" + chkboxTVGuide.IsChecked
                    + "', Delivery = '" + txtDelivery.Text
                    + "' where Id = '" + txtId.Text + "'";

                messageBoxText = "Customer Edited ";
            }
            sss.ExecNonQuery(Query);
            MessageBox.Show("Save Button Clicked");
            TurnsButtonsOn();
            //Update_DataGrid();
            MessageBox.Show(messageBoxText);


        }

        private void cboxStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_DataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TestWindow1 ttt = new TestWindow1();
            ttt.ShowDialog();
        }
    }

}

