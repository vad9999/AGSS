using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace AGSS
{
    /// <summary>
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        string connectionString;
        int ID;

        public CustomerWindow(string con, int id)
        {
            InitializeComponent();
            connectionString = con;
            ID = id;
            if (LoadProjects() != null)
            {
                ProjectCombo.ItemsSource = LoadProjects();
            }
        }

        private List<string> LoadProjects()
        {
            List<string> names = new List<string>();
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand($"select ProjectName from Project where CustomerID = {ID};", connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(reader["ProjectName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return names;
        }

        private void DataTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataTree.SelectedItem is TreeViewItem selectedItem)
            {
                switch(selectedItem.Header.ToString())
                {
                    case "Площадь":
                        if(GetDataOfArea() != null)
                        {
                            Data.ItemsSource = GetDataOfArea().DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("Данные о площадях не найдены");
                        }
                        break;
                    case "Профиль":
                        break;
                    case "Канал 1":
                        break;
                    case "Канал 2":
                        break;
                    case "Канал 3":
                        break;
                    case "Полёт":
                        break;
                    case "Спектромер":
                        break;
                    case "Метаданные":
                        break;


                }
            }
        }

        private int? GetProjectIDByName()
        {
            int? id = null;
            if (ProjectCombo.SelectedItem != null)
            {
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();

                    SqlCommand command = new SqlCommand($"select ProjectID from Project where ProjectName = '{ProjectCombo.SelectedItem}' and CustomerID = {ID};", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите проект!");
            }
            return id;
        }

        private int? GetAreaIDbyProjectID()
        {

        }

        private DataTable GetDataOfArea()
        {
            DataTable data = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                int? ProjectID = GetProjectIDByName();
                if(ProjectID != null)
                {
                    SqlCommand command = new SqlCommand($"select GeologicalInfo, Area, ProfileCount from Area where ProjectID = {ProjectID};", connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(data);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return data;
        }

        private DataTable GetDataofProfile()
        {
            DataTable data = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                int? ProjectID = GetProjectIDByName();
                if (ProjectID != null)
                {
                    SqlCommand command = new SqlCommand($"SELECT p.ProfileID, p.StartX, p.StartY, p.EndX, p.EndY, \r\n       STRING_AGG(CONCAT(pc.X, ',', pc.Y), '; ') AS Coordinates\r\nFROM Profile p\r\nJOIN ProfileCoordinates pc ON p.ProfileID = pc.ProfileID\r\nWHERE p.AreaID = 1\r\nGROUP BY p.ProfileID, p.StartX, p.StartY, p.EndX, p.EndY;", connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return data;
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
