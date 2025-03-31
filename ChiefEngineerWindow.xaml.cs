using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using AGSS.Entities;
using AGSS.Repositories;
using Microsoft.Data.SqlClient;

namespace AGSS
{
    /// <summary>
    /// Логика взаимодействия для ChiefEngineerWindow.xaml
    /// </summary>
    public partial class ChiefEngineerWindow : Window
    {
        public ChiefEngineerWindow()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                var names = ProjectRepository.GetProjectNamesAdmin(context);
                if (names != null)
                {
                    ProjectCombo.ItemsSource = names;
                }
                else
                    MessageBox.Show("Нет данных о проектах");
            }
        }

        private void ProjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectCombo.SelectedItem != null)
            {
                using (var context = new GravitySurveyOnDeleteNoAction())
                {
                    var Projects = new ObservableCollection<Project>(ProjectRepository.GetDataOfProject(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context));
                    ProjectView.ItemsSource = Projects;

                    foreach (var pr in Projects)
                    {
                        pr.PropertyChanged += (s, e) => ProjectRepository.SaveChanges((Project)s);
                    }
                }
            }
        }
    }
}
