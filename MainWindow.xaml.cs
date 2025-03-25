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
using Microsoft.Data.SqlClient;

namespace AGSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        static string servername = "localhost\\SQLEXPRESS";
        static string dbName = "AGSS";
        public string servername_ = "ZALMAN\\SQLEXPRESS";
        public string connectionString = $"Server={servername};Database={dbName};Integrated Security=True;TrustServerCertificate=True;";
        public MainWindow()
        {
            InitializeComponent();
            LoginCombo.ItemsSource = new List<string> { "Заказчик", "Главный инженер", "Ведущий специалист", "Оператор", "Аналитик" };
        }

        private void LogInBTN_Click(object sender, RoutedEventArgs e)
        {
            if(LoginBox.Text.Trim() != "" && PasBox.Password.Trim() != "")
            {
                if (LoginCombo.SelectedItem != null)
                {
                    string login = LoginBox.Text.Trim();
                    string password = PasBox.Password.Trim();

                    switch(LoginCombo.SelectedItem)
                    {
                        case "Заказчик":
                            try
                            {
                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();

                                SqlCommand command = new SqlCommand($"SELECT COUNT(1) FROM Customer WHERE Login = '{login}' AND Password = '{password}';", connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if(reader.Read())
                                {
                                    int count = reader.GetInt32(0);
                                    reader.Close();

                                    SqlCommand command1 = new SqlCommand($"SELECT CustomerID FROM Customer WHERE Login = '{login}' AND Password = '{password}';", connection);
                                    SqlDataReader reader1 = command1.ExecuteReader();

                                    if (reader1.Read())
                                    {
                                        int id = reader1.GetInt32(0);

                                        if (count == 1)
                                        {
                                            CustomerWindow customerWindow = new CustomerWindow(connectionString, id);
                                            customerWindow.Show();
                                            this.Close();
                                            connection.Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз. 😢");
                                            connection.Close();
                                        }
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                        case "Главный инженер":
                            try
                            {
                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();

                                SqlCommand command = new SqlCommand($"SELECT COUNT(1) FROM ChiefEngineer WHERE Login = '{login}' AND Password = '{password}';", connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.Read())
                                {
                                    int count = reader.GetInt32(0);

                                    if (count == 1)
                                    {
                                        ChiefEngineerWindow engineerWindow = new ChiefEngineerWindow();
                                        engineerWindow.Show();
                                        this.Close();
                                        connection.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз. 😢");
                                        connection.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                        case "Ведущий специалист":
                            try
                            {
                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();

                                SqlCommand command = new SqlCommand($"SELECT COUNT(1), LeadSpecialistID FROM LeadSpecialist WHERE Login = '{login}' AND Password = '{password}';", connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.Read())
                                {
                                    int count = reader.GetInt32(0);
                                    int id = reader.GetInt32(1);

                                    if (count == 1)
                                    {
                                        LeadSpecialistWindow specialistWindow = new LeadSpecialistWindow();
                                        specialistWindow.Show();
                                        this.Close();
                                        connection.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз. 😢");
                                        connection.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                        case "Оператор":
                            try
                            {
                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();

                                SqlCommand command = new SqlCommand($"SELECT COUNT(1), OperatorID FROM Operator WHERE Login = {login} AND Password = {password}", connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.Read())
                                {
                                    int count = reader.GetInt32(0);
                                    int id = reader.GetInt32(1);

                                    if (count == 1)
                                    {
                                        OperatorWindow operatorWindow = new OperatorWindow();
                                        operatorWindow.Show();
                                        this.Close();
                                        connection.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз. 😢");
                                        connection.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                        case "Аналитик":
                            try
                            {
                                SqlConnection connection = new SqlConnection(connectionString);
                                connection.Open();

                                SqlCommand command = new SqlCommand($"SELECT COUNT(1) FROM Analyst WHERE Login = {login} AND Password = {password}", connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.Read())
                                {
                                    int count = reader.GetInt32(0);

                                    if (count == 1)
                                    {
                                        AnalystWindow analystWindow = new AnalystWindow();
                                        analystWindow.Show();
                                        this.Close();
                                        connection.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз. 😢");
                                        connection.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите роль!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
    }
}