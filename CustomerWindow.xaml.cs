using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using AGSS.Entities;
using AGSS.Repositories;
using Microsoft.Data.SqlClient;

namespace AGSS
{
    /// <summary>
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        int ID;

        public CustomerWindow(int id)
        {
            InitializeComponent();
            ID = id;
            LoadProjects();
        }
        
        private void LoadProjects()
        {
            var names = ProjectRepository.GetProjectNames(ID);
            if (names != null)
            {
                ProjectCombo.ItemsSource = names;
            }
            else
                MessageBox.Show("Нет данных о проектах");
        }
       
        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataTree.SelectedItem is TreeViewItem selectedItem)
            {
                if (ProjectCombo.SelectedItem != null)
                {
                    switch (selectedItem.Header.ToString())
                    {
                        case "Площадь":
                            AreaColumns();

                            var areaData = AreaRepository.GetDataOfArea(
                                ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()));

                            Data.ItemsSource = areaData;
                            break;
                        case "Профиль":
                            ProfileColumns();

                            var profileData = ProfileRepository.GetDataOfProfile(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = profileData;
                            break;
                        case "Канал 1":
                            ChannelsColumns();

                            var channel1Data = ChannelsRepository.GetDataOfChannel1(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = channel1Data;
                            break;
                        case "Канал 2":

                            var channel2Data = ChannelsRepository.GetDataOfChannel2(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = channel2Data;
                            ChannelsColumns();
                            break;
                        case "Канал 3":

                            var channel3Data = ChannelsRepository.GetDataOfChannel3(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = channel3Data;
                            ChannelsColumns();
                            break;
                        case "Полет":
                            FlightColumns();

                            var flightData = FlightRepository.GetDataOfFlight(
                                ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()));

                            Data.ItemsSource = flightData;
                            break;
                        case "Спектрометр":
                            SpectrometerColumns();

                            var specData = SpectrometerRepository.GetDataOfSpectrometer(
                                FlightRepository.GetFlightIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = specData;
                            break;
                        case "Метаданные":
                            MetadataColumns();

                            var metaData = MetadataRepository.GetDataOfMetadata(
                                SpectrometerRepository.GetSpectrometerIDByFlightID(
                                FlightRepository.GetFlightIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            Data.ItemsSource = metaData;
                            break;
                    }
                }
            }
        }

        

        

        private void AreaColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Геологическая информация", DisplayMemberBinding = new Binding("GeologicalInfo") },
                    new GridViewColumn { Header = "Область", DisplayMemberBinding = new Binding("Area1") },
                    new GridViewColumn { Header = "Количество профилей", DisplayMemberBinding = new Binding("ProfileCount") }
                }
            };
        }

        private void ProfileColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Номер Профиля", DisplayMemberBinding = new Binding("ProfileId") },
                    new GridViewColumn { Header = "X", DisplayMemberBinding = new Binding("X") },
                    new GridViewColumn { Header = "Y", DisplayMemberBinding = new Binding("Y") }
                }
            };
        }

        private void ChannelsColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Номер Профиля", DisplayMemberBinding = new Binding("ProfileId") },
                    new GridViewColumn { Header = "X", DisplayMemberBinding = new Binding("X") },
                    new GridViewColumn { Header = "Y", DisplayMemberBinding = new Binding("Y") },
                    new GridViewColumn { Header = "Значение", DisplayMemberBinding = new Binding("MeasurementResult")}
                }
            };
        }

        private void FlightColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Дата начала полета", DisplayMemberBinding = new Binding("StartDateTime") },
                    new GridViewColumn { Header = "Дата конца полета", DisplayMemberBinding = new Binding("EndDateTime") },
                    new GridViewColumn { Header = "Высота над уровнем моря", DisplayMemberBinding = new Binding("AltitudeAboveSea") },
                    new GridViewColumn { Header = "Высота над уровнем земли", DisplayMemberBinding = new Binding("AltitudeAboveGround")},
                    new GridViewColumn { Header = "Средняя скорость", DisplayMemberBinding = new Binding("Speed")}
                }
            };
        }

        private void SpectrometerColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Дата начала полета", DisplayMemberBinding = new Binding("MeasurementTime") },
                    new GridViewColumn { Header = "Дата конца полета", DisplayMemberBinding = new Binding("PulseCount") },
                    new GridViewColumn { Header = "Высота над уровнем моря", DisplayMemberBinding = new Binding("TotalCount") },
                    new GridViewColumn { Header = "Высота над уровнем земли", DisplayMemberBinding = new Binding("EnergyWindowsCount")}
                }
            };
        }

        private void MetadataColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Дата начала полета", DisplayMemberBinding = new Binding("EquipmentDescription") },
                    new GridViewColumn { Header = "Дата конца полета", DisplayMemberBinding = new Binding("Notes") }
                }
            };
        }
    }
}
