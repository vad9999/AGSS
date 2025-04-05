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
using System.Windows.Markup;
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
                    if(ProjectRepository.GetSpecialistIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context) != null)
                    {
                        var Specialists = new ObservableCollection<LeadSpecialist>(SpecialistRepository.GetDataOfSpecialist((int)ProjectRepository.GetSpecialistIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context), context));
                        SpecialistView.ItemsSource = Specialists;

                        foreach (var pr in Specialists)
                        {
                            pr.PropertyChanged += (s, e) => SpecialistRepository.SaveChanges((LeadSpecialist)s);
                        }
                    }

                    var Engineers = new ObservableCollection<ChiefEnginner>(EngineerRepository.GetDataOfEngineer((int)ProjectRepository.GetEngineerIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context), context));
                    EngineerView.ItemsSource = Engineers;

                    foreach (var pr in Engineers)
                    {
                        pr.PropertyChanged += (s, e) => EngineerRepository.SaveChanges((ChiefEnginner)s);
                    }

                    var Analysts = new ObservableCollection<Analyst>(AnalystRepository.GetDataOfAnalyst((int)ProjectRepository.GetAnalystIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context), context));

                    AnalystView.ItemsSource = Analysts;

                    foreach (var pr in Analysts)
                    {
                        pr.PropertyChanged += (s, e) => AnalystRepository.SaveChanges((Analyst)s);
                    }
                    if(FlightRepository.GetOperatorIDByFlightID(FlightRepository.GetFlightIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context), context) != null)
                    {
                        var Operators = new ObservableCollection<Operator>(OperatorRepository.GetDataOfOperator((int)FlightRepository.GetOperatorIDByFlightID(FlightRepository.GetFlightIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context), context), context), context));

                        OperatorView.ItemsSource = Operators;

                        foreach (var pr in Operators)
                        {
                            pr.PropertyChanged += (s, e) => OperatorRepository.SaveChanges((Operator)s);
                        }
                    }
                }
            }
        }

        private void DataTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataTree.SelectedItem is TreeViewItem selectedItem)
            {
                if (ProjectCombo.SelectedItem != null)
                {
                    using (var context = new GravitySurveyOnDeleteNoAction())
                    {
                        switch (selectedItem.Header.ToString())
                        {
                            case "Площадь":
                                AreaColumns();
                                AreaCoordinateColumns();
                                CoordinateView.Visibility = Visibility;

                                var areaData = new ObservableCollection<Area>(AreaRepository.GetDataOfArea(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                    context
                                ));

                                Data.ItemsSource = areaData;

                                foreach (var pr in areaData)
                                {
                                    pr.PropertyChanged += (s, e) => AreaRepository.SaveChanges((Area)s);
                                }

                                var coordinates = new ObservableCollection<AreaCoordinate>(AreaRepository.GetAreaCoordinates(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                    context
                                ));

                                CoordinateView.ItemsSource = coordinates;

                                foreach (var pr in coordinates)
                                {
                                    pr.PropertyChanged += (s, e) => AreaRepository.SaveChangesCoord((AreaCoordinate)s);
                                }
                                break;
                            case "Профиль":
                                ProfileColumns();
                                ProfileCoordinateColumns();
                                CoordinateView.Visibility = Visibility;

                                var profileData = new ObservableCollection<Profile>( ProfileRepository.GetProfiles(
                                    AreaRepository.GetAreaIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                    context),
                                context));

                                Data.ItemsSource = profileData;

                                foreach (var pr in profileData)
                                {
                                    pr.PropertyChanged += (s, e) => ProfileRepository.SaveChanges((Profile)s);
                                }

                                var coordinate = new ObservableCollection<ProfileCoordinate>(ProfileRepository.GetProfileCoordinates(AreaRepository.GetAreaIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                    context), context));

                                CoordinateView.ItemsSource = coordinate;

                                foreach (var pr in coordinate)
                                {
                                    pr.PropertyChanged += (s, e) => ProfileRepository.SaveChangesCoord((ProfileCoordinate)s);
                                }
                                break;
                            case "Канал 1":
                                ChannelsColumns();

                                var channel1Data = new ObservableCollection<object>( ChannelsRepository.GetDataOfChannel1(
                                    AreaRepository.GetAreaIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                        context),
                                    context));

                                Data.ItemsSource = channel1Data;
                                break;
                            case "Канал 2":

                                var channel2Data = new ObservableCollection<object>(ChannelsRepository.GetDataOfChannel2(
                                    AreaRepository.GetAreaIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                        context),
                                    context));

                                Data.ItemsSource = channel2Data;
                                ChannelsColumns();
                                break;
                            case "Канал 3":

                                var channel3Data = new ObservableCollection<object>(ChannelsRepository.GetDataOfChannel3(
                                    AreaRepository.GetAreaIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                        context),
                                    context));

                                Data.ItemsSource = channel3Data;
                                ChannelsColumns();
                                break;
                            case "Полет":
                                FlightColumns();

                                var flightData = new ObservableCollection<object>(FlightRepository.GetDataOfFlight(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                    context));

                                Data.ItemsSource = flightData;
                                break;
                            case "Спектрометр":
                                SpectrometerColumns();

                                var specData = new ObservableCollection<object>(SpectrometerRepository.GetDataOfSpectrometer(
                                    FlightRepository.GetFlightIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                        context),
                                    context));

                                Data.ItemsSource = specData;
                                break;
                            case "Метаданные":
                                MetadataColumns();

                                var metaData = new ObservableCollection<object>(MetadataRepository.GetDataOfMetadata(
                                    SpectrometerRepository.GetSpectrometerIDByFlightID(
                                    FlightRepository.GetFlightIDByProjectID(
                                        ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString(), context),
                                        context),
                                    context),
                                    context));

                                Data.ItemsSource = metaData;
                                break;
                        }
                    }
                }
            }
        }

        private DataTemplate CreateTextBoxTemplate(string propertyName)
        {
            DataTemplate template = new DataTemplate();

            FrameworkElementFactory textBoxFactory = new FrameworkElementFactory(typeof(TextBox));
            textBoxFactory.SetBinding(TextBox.TextProperty, new Binding(propertyName)
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            template.VisualTree = textBoxFactory;
            return template;
        }

        private void AreaColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Номер площади", DisplayMemberBinding = new Binding ("AreaId")},
                    new GridViewColumn { Header = "Геологическая информация", CellTemplate = CreateTextBoxTemplate("GeologicalInfo") },
                    new GridViewColumn { Header = "Площадь", CellTemplate = CreateTextBoxTemplate("Area1") },
                    new GridViewColumn { Header = "Количество профилей", CellTemplate = CreateTextBoxTemplate("ProfileCount") }
                }
            };
        }

        private void AreaCoordinateColumns()
        {
            CoordinateView.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "X", CellTemplate = CreateTextBoxTemplate("X") },
                    new GridViewColumn { Header = "Y", CellTemplate = CreateTextBoxTemplate("Y") }
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
                    new GridViewColumn { Header = "Количество изломов", CellTemplate = CreateTextBoxTemplate("BreaksCount")}
                }
            };
        }

        private void ProfileCoordinateColumns()
        {
            CoordinateView.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Номер Профиля", DisplayMemberBinding = new Binding("ProfileId") },
                    new GridViewColumn { Header = "X", CellTemplate = CreateTextBoxTemplate("X") },
                    new GridViewColumn { Header = "Y", CellTemplate = CreateTextBoxTemplate("Y") }
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
                    new GridViewColumn { Header = "X", CellTemplate = CreateTextBoxTemplate("X") },
                    new GridViewColumn { Header = "Y", CellTemplate = CreateTextBoxTemplate("Y") },
                    new GridViewColumn { Header = "Значение", CellTemplate = CreateTextBoxTemplate("MeasurementResult")}
                }
            };
        }

        private void FlightColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Дата начала полета", CellTemplate = CreateTextBoxTemplate("StartDateTime") },
                    new GridViewColumn { Header = "Дата конца полета", CellTemplate = CreateTextBoxTemplate("EndDateTime") },
                    new GridViewColumn { Header = "Высота над уровнем моря", CellTemplate = CreateTextBoxTemplate("AltitudeAboveSea") },
                    new GridViewColumn { Header = "Высота над уровнем земли", CellTemplate = CreateTextBoxTemplate("AltitudeAboveGround")},
                    new GridViewColumn { Header = "Средняя скорость", CellTemplate = CreateTextBoxTemplate("Speed")}
                }
            };
        }

        private void SpectrometerColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Время измерения", CellTemplate = CreateTextBoxTemplate("MeasurementTime") },
                    new GridViewColumn { Header = "Количество импульсов", CellTemplate = CreateTextBoxTemplate("PulseCount") },
                    new GridViewColumn { Header = "Общий счет", CellTemplate = CreateTextBoxTemplate("TotalCount") },
                    new GridViewColumn { Header = "Количество энергетических окон", CellTemplate = CreateTextBoxTemplate("EnergyWindowsCount")}
                }
            };
        }

        private void MetadataColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Описание оборудования", CellTemplate = CreateTextBoxTemplate("EquipmentDescription") },
                    new GridViewColumn { Header = "Примечания", CellTemplate = CreateTextBoxTemplate("Notes") }
                }
            };
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
