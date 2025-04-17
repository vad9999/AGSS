using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AGSS.Entities;
using AGSS.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using OxyPlot;
using OxyPlot.Series;

namespace AGSS
{
    /// <summary>
    /// Логика взаимодействия для ChiefEngineerWindow.xaml
    /// </summary>
    public partial class ChiefEngineerWindow : Window
    {

        private ObservableCollection<Area> areaData = new ObservableCollection<Area>();
        private ObservableCollection<AreaCoordinate> areaCoordinates = new ObservableCollection<AreaCoordinate>();

        private ObservableCollection<Profile> profileData = new ObservableCollection<Profile>();
        private ObservableCollection<ProfileCoordinate> profileCoordinates = new ObservableCollection<ProfileCoordinate>();

        private ObservableCollection<Channel1> channel1Data = new ObservableCollection<Channel1>();
        private ObservableCollection<Channel2> channel2Data = new ObservableCollection<Channel2>();
        private ObservableCollection<Channel3> channel3Data = new ObservableCollection<Channel3>();

        private ObservableCollection<Flight> flightData = new ObservableCollection<Flight>();
        private ObservableCollection<Spectrometer> spectrometerData = new ObservableCollection<Spectrometer>();
        private ObservableCollection<Metadata> metadataData = new ObservableCollection<Metadata>();

        private List<DataPoint> dataPoints = new List<DataPoint>();

        public ChiefEngineerWindow()
        {
            InitializeComponent();
            LoadProjects();
            AddCombo.ItemsSource = new List<string> {"Проект", "Площадь", "Координаты площади", "Профиль", "Координаты профиля", "Канал 1", "Канал 2", "Канал 3", "Полет", "Спектрометер", "Метаданые" };
        }

        private void LoadProjects()
        {
            var names = ProjectRepository.GetProjectNamesAdmin();
            names.Add("Добавить проект");
            if (names != null)
            {
                ProjectCombo.ItemsSource = names;
            }
            else
                MessageBox.Show("Нет данных о проектах");
        }

        private void ProjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectCombo.SelectedItem != null && ProjectCombo.SelectedItem.ToString() != "Добавить проект")
            {
                var Projects = new ObservableCollection<Project>(ProjectRepository.GetDataOfProject(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));
                ProjectView.ItemsSource = Projects;

                foreach (var pr in Projects)
                {
                    pr.PropertyChanged += (s, e) => ProjectRepository.SaveChanges((Project)s);
                }
                if(ProjectRepository.GetSpecialistIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())) != null)
                {
                    var Specialists = new ObservableCollection<LeadSpecialist>(SpecialistRepository.GetDataOfSpecialist((int)ProjectRepository.GetSpecialistIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));
                    SpecialistView.ItemsSource = Specialists;

                    foreach (var pr in Specialists)
                    {
                        pr.PropertyChanged += (s, e) => SpecialistRepository.SaveChanges((LeadSpecialist)s);
                    }
                }

                var Engineers = new ObservableCollection<ChiefEnginner>(EngineerRepository.GetDataOfEngineer((int)ProjectRepository.GetEngineerIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));
                EngineerView.ItemsSource = Engineers;

                foreach (var pr in Engineers)
                {
                    pr.PropertyChanged += (s, e) => EngineerRepository.SaveChanges((ChiefEnginner)s);
                }

                var Analysts = new ObservableCollection<Analyst>(AnalystRepository.GetDataOfAnalyst((int)ProjectRepository.GetAnalystIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                AnalystView.ItemsSource = Analysts;

                foreach (var pr in Analysts)
                {
                    pr.PropertyChanged += (s, e) => AnalystRepository.SaveChanges((Analyst)s);
                }
                if(FlightRepository.GetOperatorIDByFlightID(FlightRepository.GetFlightIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))) != null)
                {
                    var Operators = new ObservableCollection<Operator>(OperatorRepository.GetDataOfOperator((int)FlightRepository.GetOperatorIDByFlightID(FlightRepository.GetFlightIDByProjectID(ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())))));

                    OperatorView.ItemsSource = Operators;

                    foreach (var pr in Operators)
                    {
                        pr.PropertyChanged += (s, e) => OperatorRepository.SaveChanges((Operator)s);
                    }
                }
            }

            if(ProjectCombo.SelectedItem.ToString() == "Добавить проект")
            {
                
            }
        }

        private void DataTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataTree.SelectedItem is TreeViewItem selectedItem)
            {
                if (ProjectCombo.SelectedItem != null && ProjectCombo.SelectedItem.ToString() != "Добавить проект")
                {
                    switch (selectedItem.Header.ToString())
                    {
                        case "Площадь":
                            AreaColumns();
                            AreaCoordinateColumns();
                            CoordinateView.Visibility = Visibility;

                            areaData = new ObservableCollection<Area>(AreaRepository.GetDataOfArea(
                                ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = areaData;

                            foreach (var pr in areaData)
                            {
                                pr.PropertyChanged += (s, e) => AreaRepository.SaveChanges((Area)s);
                            }

                            areaCoordinates = new ObservableCollection<AreaCoordinate>(AreaRepository.GetAreaCoordinates(
                                ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            CoordinateView.ItemsSource = areaCoordinates;

                            foreach (var pr in areaCoordinates)
                            {
                                pr.PropertyChanged += (s, e) => AreaRepository.SaveChangesCoord((AreaCoordinate)s);
                            }

                            if(areaCoordinates.Count > 0)
                            {
                                foreach (var c in areaCoordinates)
                                {
                                    dataPoints.Add(new DataPoint((double)c.X, (double)c.Y));
                                }

                                var plotModel = new PlotModel { Title = "График координат площади" };
                                var series = new LineSeries { Title = "Линия", MarkerType = MarkerType.Circle };

                                foreach (var point in dataPoints)
                                {
                                    series.Points.Add(point);
                                }

                                plotModel.Series.Add(series);
                                plotView.Model = plotModel;
                            }
                            break;
                        case "Профиль":
                            ProfileColumns();
                            ProfileCoordinateColumns();
                            CoordinateView.Visibility = Visibility;

                            profileData = new ObservableCollection<Profile>( ProfileRepository.GetProfiles(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            Data.ItemsSource = profileData;

                            foreach (var pr in profileData)
                            {
                                pr.PropertyChanged += (s, e) => ProfileRepository.SaveChanges((Profile)s);
                            }

                            profileCoordinates = new ObservableCollection<ProfileCoordinate>(ProfileRepository.GetProfileCoordinates(AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            CoordinateView.ItemsSource = profileCoordinates;

                            foreach (var pr in profileCoordinates)
                            {
                                pr.PropertyChanged += (s, e) => ProfileRepository.SaveChangesCoord((ProfileCoordinate)s);
                            }
                            break;
                        case "Канал 1":
                            ChannelsColumns();
                            ProfileCoordinateColumns();
                            CoordinateView.Visibility = Visibility;

                            channel1Data = new ObservableCollection<Channel1>( ChannelsRepository.GetChannel1s(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            Data.ItemsSource = channel1Data;

                            foreach (var pr in channel1Data)
                            {
                                pr.PropertyChanged += (s, e) => ChannelsRepository.SaveChanges1((Channel1)s);
                            }

                            profileCoordinates = new ObservableCollection<ProfileCoordinate>(ProfileRepository.GetProfileCoordinates(AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            CoordinateView.ItemsSource = profileCoordinates;

                            foreach (var pr in profileCoordinates)
                            {
                                pr.PropertyChanged += (s, e) => ProfileRepository.SaveChangesCoord((ProfileCoordinate)s);
                            }
                            break;
                        case "Канал 2":
                            ChannelsColumns();
                            ProfileCoordinateColumns();
                            CoordinateView.Visibility = Visibility;

                            channel2Data = new ObservableCollection<Channel2>(ChannelsRepository.GetChannel2s(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            Data.ItemsSource = channel2Data;

                            foreach (var pr in channel2Data)
                            {
                                pr.PropertyChanged += (s, e) => ChannelsRepository.SaveChanges2((Channel2)s);
                            }

                            profileCoordinates = new ObservableCollection<ProfileCoordinate>(ProfileRepository.GetProfileCoordinates(AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            CoordinateView.ItemsSource = profileCoordinates;

                            foreach (var pr in profileCoordinates)
                            {
                                pr.PropertyChanged += (s, e) => ProfileRepository.SaveChangesCoord((ProfileCoordinate)s);
                            }
                            break;
                        case "Канал 3":
                            ChannelsColumns();
                            ProfileCoordinateColumns();
                            CoordinateView.Visibility = Visibility;

                            channel3Data = new ObservableCollection<Channel3>(ChannelsRepository.GetChannel3s(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            Data.ItemsSource = channel3Data;

                            foreach (var pr in channel3Data)
                            {
                                pr.PropertyChanged += (s, e) => ChannelsRepository.SaveChanges3((Channel3)s);
                            }

                            profileCoordinates = new ObservableCollection<ProfileCoordinate>(ProfileRepository.GetProfileCoordinates(AreaRepository.GetAreaIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            CoordinateView.ItemsSource = profileCoordinates;

                            foreach (var pr in profileCoordinates)
                            {
                                pr.PropertyChanged += (s, e) => ProfileRepository.SaveChangesCoord((ProfileCoordinate)s);
                            }
                            break;
                        case "Полет":
                            FlightColumns();
                            CoordinateView.Visibility = Visibility.Hidden;
                            flightData = new ObservableCollection<Flight>(FlightRepository.GetDataOfFlight(
                                ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                            Data.ItemsSource = flightData;

                            foreach (var pr in flightData)
                            {
                                pr.PropertyChanged += (s, e) => FlightRepository.SaveChanges((Flight)s);
                            }
                            break;
                        case "Спектрометр":
                            SpectrometerColumns();
                            CoordinateView.Visibility = Visibility.Hidden;
                            spectrometerData = new ObservableCollection<Spectrometer>(SpectrometerRepository.GetDataOfSpectrometer(
                                FlightRepository.GetFlightIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString()))));

                            Data.ItemsSource = spectrometerData;

                            foreach (var pr in spectrometerData)
                            {
                                pr.PropertyChanged += (s, e) => SpectrometerRepository.SaveChanges((Spectrometer)s);
                            }
                            break;
                        case "Метаданные":
                            MetadataColumns();
                            CoordinateView.Visibility = Visibility.Hidden;
                            metadataData = new ObservableCollection<Metadata>(MetadataRepository.GetDataOfMetadata(
                                SpectrometerRepository.GetSpectrometerIDByFlightID(
                                FlightRepository.GetFlightIDByProjectID(
                                    ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())))));

                            Data.ItemsSource = metadataData;

                            foreach (var pr in metadataData)
                            {
                                pr.PropertyChanged += (s, e) => MetadataRepository.SaveChanges((Metadata)s);
                            }
                            break;
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
                    new GridViewColumn { Header = "Номер координат профиля", DisplayMemberBinding = new Binding("ProfileCoordinatesId") },
                    new GridViewColumn { Header = "X", CellTemplate = CreateTextBoxTemplate("X") },
                    new GridViewColumn { Header = "Y", CellTemplate = CreateTextBoxTemplate("Y") },
                    new GridViewColumn { Header = "Номер профиля", DisplayMemberBinding = new Binding("ProfileId") }
                }
            };
        }

        private void ChannelsColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Значение", CellTemplate = CreateTextBoxTemplate("MeasurementResult")},
                    new GridViewColumn {Header = "№ координат профиля", CellTemplate = CreateTextBoxTemplate("ProfileCoordinatesId")}
                }
            };
        }

        private void FlightColumns()
        {
            Data.View = new GridView
            {
                Columns =
                {
                    new GridViewColumn { Header = "Номер полёта", DisplayMemberBinding = new Binding("FlightId") },
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
                    new GridViewColumn {Header = "Номер спектрометра", DisplayMemberBinding = new Binding("SpectrometerId") },
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
                    new GridViewColumn { Header = "№ спектрометра", DisplayMemberBinding = new Binding("SpectrometerId")},
                    new GridViewColumn { Header = "Описание оборудования", CellTemplate = CreateTextBoxTemplate("EquipmentDescription") },
                    new GridViewColumn { Header = "Примечания", CellTemplate = CreateTextBoxTemplate("Notes") }
                }
            };
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if(ProjectCombo.SelectedItem != null)
            {
                int ProjectID = ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString());
                if (Data.SelectedItem != null)
                {
                    var select = Data.SelectedItem;
                    if(select is Channel1)
                    {
                        using (var context = new GravitySurveyOnDeleteNoAction())
                        {
                            context.Remove(select);
                            context.SaveChanges();
                            channel1Data.Remove((Channel1)select);
                        }
                    }

                    if(select is Channel2)
                    {
                        using (var context = new GravitySurveyOnDeleteNoAction())
                        {
                            context.Remove(select);
                            context.SaveChanges();
                            channel2Data.Remove((Channel2)select);
                        }
                    }

                    if(select is Channel3)
                    {
                        using(var  context = new GravitySurveyOnDeleteNoAction())
                        {
                            context.Remove(select);
                            context.SaveChanges();
                            channel3Data.Remove((Channel3)select);
                        }
                    }

                    if(select is Profile)
                    {
                        if(MessageBox.Show("Связанные данные так же будут удалены!", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                        {
                            using (var context = new GravitySurveyOnDeleteNoAction())
                            {
                                context.Profiles.Remove((Profile)select);
                                context.SaveChanges();
                                profileData.Remove((Profile)select);
                            }
                            AreaRepository.AreaProfileCountMinus(ProjectID);
                        }
                    }

                    if(select is Area)
                    {
                        if (MessageBox.Show("Связанные данные так же будут удалены!", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                        {
                            using (var context = new GravitySurveyOnDeleteNoAction())
                            {
                                context.Areas.Remove((Area)select);
                                context.SaveChanges();
                                areaData.Remove((Area)select);
                            }
                        }
                    }

                    if(select is Flight)
                    {
                        if (MessageBox.Show("Связанные данные так же будут удалены!", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                        {
                            using (var context = new GravitySurveyOnDeleteNoAction())
                            {
                                context.Areas.Remove((Area)select);
                                context.SaveChanges();
                                areaData.Remove((Area)select);
                            }
                        }
                    }

                    if (select is Spectrometer)
                    {
                        if (MessageBox.Show("Связанные данные так же будут удалены!", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                        {
                            using (var context = new GravitySurveyOnDeleteNoAction())
                            {
                                context.Spectrometers.Remove((Spectrometer)select);
                                context.SaveChanges();
                                spectrometerData.Remove((Spectrometer)select);
                            }
                        }
                    }

                    if (select is Metadata)
                    {
                        using (var context = new GravitySurveyOnDeleteNoAction())
                        {
                            context.Metadata.Remove((Metadata)select);
                            context.SaveChanges();
                            metadataData.Remove((Metadata)select);
                        }
                    }
                }
                else
                {
                    if (CoordinateView.SelectedItem != null)
                    {
                        var select = CoordinateView.SelectedItem;
                        if(select is AreaCoordinate)
                        {
                            using (var context = new GravitySurveyOnDeleteNoAction())
                            {
                                context.AreaCoordinates.Remove((AreaCoordinate)select);
                                context.SaveChanges();
                                areaCoordinates.Remove((AreaCoordinate)select);
                            }
                            AreaRepository.AreaBreakCountMinus(ProjectID);
                        }

                        if(select is ProfileCoordinate)
                        {
                            if (MessageBox.Show("Связанные данные так же будут удалены!", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                            {
                                using (var context = new GravitySurveyOnDeleteNoAction())
                                {
                                    context.ProfileCoordinates.Remove((ProfileCoordinate)select);
                                    context.SaveChanges();
                                    profileCoordinates.Remove((ProfileCoordinate)select);
                                }
                                ProfileCoordinate profile = (ProfileCoordinate)select;
                                int id = (int)profile.ProfileId;
                                ProfileRepository.ProfileBreakCountMinus(id);
                                TreeViewItem dataTree = (TreeViewItem)DataTree.SelectedItem;
                                switch (dataTree.Header.ToString())
                                {
                                    case "Канал 1":
                                        channel1Data = new ObservableCollection<Channel1>(ChannelsRepository.GetChannel1s(
                                        AreaRepository.GetAreaIDByProjectID(ProjectID)));
                                        Data.ItemsSource = channel1Data;
                                        break;
                                    case "Канал 2":
                                        channel2Data = new ObservableCollection<Channel2>(ChannelsRepository.GetChannel2s(
                                        AreaRepository.GetAreaIDByProjectID(ProjectID)));
                                        Data.ItemsSource = channel2Data;
                                        break;
                                    case "Канал 3":
                                        channel3Data = new ObservableCollection<Channel3>(ChannelsRepository.GetChannel3s(
                                        AreaRepository.GetAreaIDByProjectID(ProjectID)));
                                        Data.ItemsSource = channel3Data;
                                        break;
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите данные для удаления");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите проект!");
            }
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectCombo.SelectedItem != null && ProjectCombo.SelectedItem.ToString() != "Добавить проект")
            {
                if (AddCombo.SelectedItem != null)
                {
                    int ProjectID = ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString());
                    switch (AddCombo.SelectedItem.ToString())
                    {
                        case "Площадь":
                            if(oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                Area area = new Area { GeologicalInfo = oneBox.Text, Area1 = double.Parse(twoBox.Text.Trim().Replace('.', ',')), ProfileCount = 0, BreaksCount = 0, ProjectId = ProjectID };
                                AreaRepository.Add(area);
                                oneBox.Text = "";
                                twoBox.Text = "";
                                areaData.Add(area);
                            }

                                break;
                        case "Координаты площади":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                AreaCoordinate areacoord = new AreaCoordinate { X = double.Parse(oneBox.Text.Trim().Replace('.', ',')), Y = double.Parse(twoBox.Text.Trim().Replace('.', ',')), AreaId = AreaRepository.GetAreaIDByProjectID(ProjectID) };
                                AreaRepository.AddCoordinate(areacoord);
                                AreaRepository.AreaBreakCount(ProjectID);
                                oneBox.Text = "";
                                twoBox.Text = "";
                                areaCoordinates.Add(areacoord);

                                areaData = new ObservableCollection<Area>(AreaRepository.GetDataOfArea(
                                ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString())));

                                Data.ItemsSource = areaData;
                            }
                            break;
                        case "Профиль":
                            Profile profile = new Profile { AreaId = AreaRepository.GetAreaIDByProjectID(ProjectID), BreaksCount = 0 };
                            ProfileRepository.Add(profile);
                            AreaRepository.AreaProfileCount(ProjectID);
                            profileData.Add(profile);
                            break;
                        case "Координаты профиля":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty() &&
                                threeBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                if(ProfileRepository.CheckProfileId(int.Parse(threeBox.Text.Trim())))
                                {
                                    ProfileCoordinate coordinate = new ProfileCoordinate { ProfileId = int.Parse(threeBox.Text.Trim()), X = double.Parse(oneBox.Text.Trim().Replace('.', ',')), Y = double.Parse(twoBox.Text.Trim().Replace('.', ',')) };
                                    ProfileRepository.AddCoordinate(coordinate);
                                    ProfileRepository.ProfileBreakCount(int.Parse(threeBox.Text.Trim()));
                                    profileCoordinates.Add(coordinate);
                                    oneBox.Text = "";
                                    twoBox.Text = "";
                                    threeBox.Text = "";

                                    profileData = new ObservableCollection<Profile>(ProfileRepository.GetProfiles(
                                AreaRepository.GetAreaIDByProjectID(
                                    ProjectID)));
                                    Data.ItemsSource = profileData;
                                }
                                else
                                {
                                    MessageBox.Show("Профиль с таким номером не найден");
                                    threeBox.Text = "";
                                }
                            }
                                break;
                        case "Канал 1":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                if(ProfileRepository.CheckProfileCoordinatesId(int.Parse(twoBox.Text.Trim())))
                                {
                                    Channel1 channel1 = new Channel1 { MeasurementResult = double.Parse(oneBox.Text.Trim().Replace('.', ',')), ProfileCoordinatesId = int.Parse(twoBox.Text.Trim()) };
                                    ChannelsRepository.AddChannel1(channel1);
                                    channel1Data.Add(channel1);

                                    oneBox.Text = "";
                                    twoBox.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Координаты профиля с таким номером не найдены!");
                                    twoBox.Text = "";
                                }
                            }
                                break;
                        case "Канал 2":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                if (ProfileRepository.CheckProfileCoordinatesId(int.Parse(twoBox.Text.Trim())))
                                {
                                    Channel2 channel2 = new Channel2 { MeasurementResult = double.Parse(oneBox.Text.Trim().Replace('.', ',')), ProfileCoordinatesId = int.Parse(twoBox.Text.Trim()) };
                                    ChannelsRepository.AddChannel2(channel2);
                                    channel2Data.Add(channel2);

                                    oneBox.Text = "";
                                    twoBox.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Координаты профиля с таким номером не найдены!");
                                    twoBox.Text = "";
                                }
                            }
                            break;
                        case "Канал 3":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                if (ProfileRepository.CheckProfileCoordinatesId(int.Parse(twoBox.Text.Trim())))
                                {
                                    Channel3 channel3 = new Channel3 { MeasurementResult = double.Parse(oneBox.Text.Trim().Replace('.', ',')), ProfileCoordinatesId = int.Parse(twoBox.Text.Trim()) };
                                    ChannelsRepository.AddChannel3(channel3);
                                    channel3Data.Add(channel3);

                                    oneBox.Text = "";
                                    twoBox.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Координаты профиля с таким номером не найдены!");
                                    twoBox.Text = "";
                                }
                            }
                            break;
                        case "Полет":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty() &&
                                threeBox.Text.Trim().IsNullOrEmpty() &&
                                fourBox.Text.Trim().IsNullOrEmpty() &&
                                fiveBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                Flight flight = new Flight { StartDateTime = DateTime.Parse(oneBox.Text.Trim()), EndDateTime = DateTime.Parse(twoBox.Text.Trim()), AltitudeAboveSea = double.Parse(threeBox.Text.Trim().Replace('.',',')), AltitudeAboveGround = double.Parse(fourBox.Text.Trim().Replace('.', ',')), Speed = double.Parse(fiveBox.Text.Trim().Replace('.', ',')), ProjectId = ProjectID, OperatorId = null};
                                FlightRepository.Add(flight);
                                flightData.Add(flight);

                                oneBox.Text = "";
                                twoBox.Text = "";
                                threeBox.Text = "";
                                fourBox.Text = "";
                                fiveBox.Text = "";
                            }    
                                break;
                        case "Спектрометер":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty() &&
                                threeBox.Text.Trim().IsNullOrEmpty() &&
                                fourBox.Text.Trim().IsNullOrEmpty() &&
                                fiveBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                if(FlightRepository.CheckFlightId(int.Parse(fiveBox.Text.Trim())))
                                {
                                    Spectrometer spectrometer = new Spectrometer { MeasurementTime = double.Parse(oneBox.Text.Trim().Replace('.', ',')), PulseCount = int.Parse(twoBox.Text.Trim()), TotalCount = int.Parse(threeBox.Text.Trim()), EnergyWindowsCount = int.Parse(fourBox.Text.Trim()), FlightId = int.Parse(fiveBox.Text.Trim()) };
                                    SpectrometerRepository.Add(spectrometer);
                                    spectrometerData.Add(spectrometer);

                                    oneBox.Text = "";
                                    twoBox.Text = "";
                                    threeBox.Text = "";
                                    fourBox.Text = "";
                                    fiveBox.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Полет с таким номером не найден!");
                                    fiveBox.Text = "";
                                }
                            }
                                break;
                        case "Метаданые":
                            if (oneBox.Text.Trim().IsNullOrEmpty() &&
                                twoBox.Text.Trim().IsNullOrEmpty() &&
                                threeBox.Text.Trim().IsNullOrEmpty())
                            {
                                MessageBox.Show("Пожалуйста заполните все поля!");
                            }
                            else
                            {
                                if(SpectrometerRepository.CheckSpectrometerId(int.Parse(threeBox.Text.Trim())))
                                {
                                    Metadata metadata = new Metadata { EquipmentDescription = oneBox.Text.Trim(), Notes = twoBox.Text.Trim(), SpectrometerId = int.Parse(threeBox.Text.Trim()) };
                                    MetadataRepository.Add(metadata);
                                    metadataData.Add(metadata);

                                    oneBox.Text = "";
                                    twoBox.Text = "";
                                    threeBox.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Спектрометер с таким номером не найден!");
                                    threeBox.Text = "";
                                }
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите какие данные вы хотите добавить!");
                }
            }
            else
            {
                MessageBox.Show("Выберите проект!");
            }
        }

        private void AddCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectCombo.SelectedItem != null && ProjectCombo.SelectedItem.ToString() != "Добавить проект")
            {
                if (AddCombo.SelectedItem != null)
                {
                    int ProjectID = ProjectRepository.GetIDByProjectName(ProjectCombo.SelectedItem.ToString());
                    switch (AddCombo.SelectedItem.ToString())
                    {
                        case "Площадь":
                            if(AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                oneLabel.Visibility = Visibility.Visible;
                                oneLabel.Content = "Геологическая информация";
                                oneBox.Visibility = Visibility.Visible;

                                twoLabel.Visibility = Visibility.Visible;
                                twoLabel.Content = "Площадь";
                                twoBox.Visibility = Visibility.Visible;

                                threeLabel.Visibility = Visibility.Hidden;
                                fourLabel.Visibility = Visibility.Hidden;
                                fiveBox.Visibility = Visibility.Hidden;

                                threeBox.Visibility = Visibility.Hidden;
                                fourBox.Visibility = Visibility.Hidden;
                                fiveLabel.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                MessageBox.Show("Площадь уже добавлена!");
                                AddCombo.SelectedItem = null;
                            }
                                break;
                        case "Координаты площади":
                            if(AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте площадь!");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                oneLabel.Visibility = Visibility.Visible;
                                oneLabel.Content = "X";
                                oneBox.Visibility = Visibility.Visible;

                                twoLabel.Visibility = Visibility.Visible;
                                twoLabel.Content = "Y";
                                twoBox.Visibility = Visibility.Visible;

                                threeLabel.Visibility = Visibility.Hidden;
                                fourLabel.Visibility = Visibility.Hidden;
                                fiveLabel.Visibility = Visibility.Hidden;

                                threeBox.Visibility = Visibility.Hidden;
                                fourBox.Visibility = Visibility.Hidden;
                                fiveBox.Visibility = Visibility.Hidden;
                            }
                            break;
                        case "Профиль":
                            if (AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте площадь!");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                oneLabel.Visibility = Visibility.Hidden;
                                oneBox.Visibility = Visibility.Hidden;

                                twoLabel.Visibility = Visibility.Hidden;
                                threeLabel.Visibility = Visibility.Hidden;
                                fourLabel.Visibility = Visibility.Hidden;
                                fiveLabel.Visibility = Visibility.Hidden;

                                twoBox.Visibility = Visibility.Hidden;
                                threeBox.Visibility = Visibility.Hidden;
                                fourBox.Visibility = Visibility.Hidden;
                                fiveBox.Visibility = Visibility.Hidden;
                            }
                            break;
                        case "Координаты профиля":
                            if(AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте площадь!");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                if (ProfileRepository.GetProfiles(ProjectID).Count == 0)
                                {
                                    MessageBox.Show("Сначала добавьте профиль!");
                                    AddCombo.SelectedItem = null;
                                }
                                else
                                {
                                    oneLabel.Visibility = Visibility.Visible;
                                    oneLabel.Content = "X";
                                    oneBox.Visibility = Visibility.Visible;

                                    twoLabel.Visibility = Visibility.Visible;
                                    twoLabel.Content = "Y";
                                    twoBox.Visibility = Visibility.Visible;

                                    threeLabel.Visibility = Visibility.Visible;
                                    threeLabel.Content = "Номер профиля";
                                    threeBox.Visibility = Visibility.Visible;

                                    fourLabel.Visibility = Visibility.Hidden;
                                    fiveLabel.Visibility = Visibility.Hidden;

                                    fourBox.Visibility = Visibility.Hidden;
                                    fiveBox.Visibility = Visibility.Hidden;
                                }
                            }
                            break;
                        case "Канал 1":
                            if (AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте площадь!");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                if (ProfileRepository.GetProfiles(ProjectID).Count == 0)
                                {
                                    MessageBox.Show("Сначала добавьте профиль!");
                                    AddCombo.SelectedItem = null;
                                }
                                else
                                {
                                    if(ProfileRepository.GetProfileCoordinates(ProjectID).Count == 0)
                                    {
                                        MessageBox.Show("Сначала добавьте координаты профиля!");
                                        AddCombo.SelectedItem = null;
                                    }
                                    else
                                    {
                                        oneLabel.Visibility = Visibility.Visible;
                                        oneLabel.Content = "Результат измерений";
                                        oneBox.Visibility = Visibility.Visible;

                                        twoLabel.Visibility = Visibility.Visible;
                                        twoLabel.Content = "Номер координат профиля";
                                        twoBox.Visibility = Visibility.Visible;

                                        threeLabel.Visibility = Visibility.Hidden;
                                        fourLabel.Visibility = Visibility.Hidden;
                                        fiveLabel.Visibility = Visibility.Hidden;

                                        threeBox.Visibility = Visibility.Hidden;
                                        fourBox.Visibility = Visibility.Hidden;
                                        fiveBox.Visibility = Visibility.Hidden;
                                    }
                                }
                            }
                                break;
                        case "Канал 2":
                            if (AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте площадь!");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                if (ProfileRepository.GetProfiles(ProjectID).Count == 0)
                                {
                                    MessageBox.Show("Сначала добавьте профиль!");
                                    AddCombo.SelectedItem = null;
                                }
                                else
                                {
                                    if (ProfileRepository.GetProfileCoordinates(ProjectID).Count == 0)
                                    {
                                        MessageBox.Show("Сначала добавьте координаты профиля!");
                                        AddCombo.SelectedItem = null;
                                    }
                                    else
                                    {
                                        oneLabel.Visibility = Visibility.Visible;
                                        oneLabel.Content = "Результат измерений";
                                        oneBox.Visibility = Visibility.Visible;

                                        twoLabel.Visibility = Visibility.Visible;
                                        twoLabel.Content = "Номер координат профиля";
                                        twoBox.Visibility = Visibility.Visible;

                                        threeLabel.Visibility = Visibility.Hidden;
                                        fourLabel.Visibility = Visibility.Hidden;
                                        fiveLabel.Visibility = Visibility.Hidden;

                                        threeBox.Visibility = Visibility.Hidden;
                                        fourBox.Visibility = Visibility.Hidden;
                                        fiveBox.Visibility = Visibility.Hidden;
                                    }
                                }
                            }
                            break;
                        case "Канал 3":
                            if (AreaRepository.GetDataOfArea(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте площадь!");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                if (ProfileRepository.GetProfiles(ProjectID).Count == 0)
                                {
                                    MessageBox.Show("Сначала добавьте профиль!");
                                    AddCombo.SelectedItem = null;
                                }
                                else
                                {
                                    if (ProfileRepository.GetProfileCoordinates(ProjectID).Count == 0)
                                    {
                                        MessageBox.Show("Сначала добавьте координаты профиля!");
                                        AddCombo.SelectedItem = null;
                                    }
                                    else
                                    {
                                        oneLabel.Visibility = Visibility.Visible;
                                        oneLabel.Content = "Результат измерений";
                                        oneBox.Visibility = Visibility.Visible;

                                        twoLabel.Visibility = Visibility.Visible;
                                        twoLabel.Content = "Номер координат профиля";
                                        twoBox.Visibility = Visibility.Visible;

                                        threeLabel.Visibility = Visibility.Hidden;
                                        fourLabel.Visibility = Visibility.Hidden;
                                        fiveLabel.Visibility = Visibility.Hidden;

                                        threeBox.Visibility = Visibility.Hidden;
                                        fourBox.Visibility = Visibility.Hidden;
                                        fiveBox.Visibility = Visibility.Hidden;
                                    }
                                }
                            }
                            break;
                        case "Полет":
                            oneLabel.Visibility = Visibility.Visible;
                            oneLabel.Content = "Дата и время начала полета";
                            oneBox.Visibility = Visibility.Visible;

                            twoLabel.Visibility = Visibility.Visible;
                            twoLabel.Content = "Дата и время конца полета";
                            twoBox.Visibility = Visibility.Visible;

                            threeLabel.Visibility = Visibility.Visible;
                            threeLabel.Content = "Высота над уровнем моря";
                            threeBox.Visibility = Visibility.Visible;

                            fourLabel.Visibility = Visibility.Visible;
                            fourLabel.Content = "Высота над уровнем земли";
                            fourBox.Visibility = Visibility.Visible;

                            fiveLabel.Visibility = Visibility.Visible;
                            fiveLabel.Content = "Скорость";
                            fiveBox.Visibility = Visibility.Visible;
                            break;
                        case "Спектрометер":
                            if(FlightRepository.GetDataOfFlight(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте данные о полет");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                oneLabel.Visibility = Visibility.Visible;
                                oneLabel.Content = "Время измерений";
                                oneBox.Visibility = Visibility.Visible;

                                twoLabel.Visibility = Visibility.Visible;
                                twoLabel.Content = "Количество импульсов";
                                twoBox.Visibility = Visibility.Visible;

                                threeLabel.Visibility = Visibility.Visible;
                                threeLabel.Content = "Общий счет";
                                threeBox.Visibility = Visibility.Visible;

                                fourLabel.Visibility = Visibility.Visible;
                                fourLabel.Content = "Количество энергетических окон";
                                fourBox.Visibility = Visibility.Visible;

                                fiveLabel.Visibility = Visibility.Visible;
                                fiveLabel.Content = "Номер полета";
                                fiveBox.Visibility = Visibility.Visible;
                            }
                                break;
                        case "Метаданые":
                            if (FlightRepository.GetDataOfFlight(ProjectID).Count == 0)
                            {
                                MessageBox.Show("Сначала добавьте данные о полет");
                                AddCombo.SelectedItem = null;
                            }
                            else
                            {
                                if(SpectrometerRepository.GetDataOfSpectrometer(FlightRepository.GetFlightIDByProjectID(ProjectID)).Count == 0)
                                {
                                    MessageBox.Show("Сначала добавьте данные о спектрометре");
                                    AddCombo.SelectedItem = null;
                                }
                                else
                                {
                                    oneLabel.Visibility = Visibility.Visible;
                                    oneLabel.Content = "Описание оборудования";
                                    oneBox.Visibility = Visibility.Visible;

                                    twoLabel.Visibility = Visibility.Visible;
                                    twoLabel.Content = "Записи";
                                    twoBox.Visibility = Visibility.Visible;

                                    threeLabel.Visibility = Visibility.Visible;
                                    threeLabel.Content = "Номер спектрометра";
                                    threeBox.Visibility = Visibility.Visible;

                                    fourLabel.Visibility = Visibility.Hidden;
                                    fiveLabel.Visibility = Visibility.Hidden;

                                    fourBox.Visibility = Visibility.Hidden;
                                    fiveBox.Visibility = Visibility.Hidden;
                                }
                            }
                                break;
                    }
                }
                else
                {
                    oneLabel.Visibility = Visibility.Hidden;
                    twoLabel.Visibility = Visibility.Hidden;
                    threeLabel.Visibility = Visibility.Hidden;
                    fourLabel.Visibility = Visibility.Hidden;
                    fiveLabel.Visibility = Visibility.Hidden;
                    oneBox.Visibility = Visibility.Hidden;
                    twoBox.Visibility = Visibility.Hidden;
                    threeBox.Visibility = Visibility.Hidden;
                    fourBox.Visibility = Visibility.Hidden;
                    fiveBox.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Выберите проект!");
                AddCombo.SelectedItem = null;
            }
        }
    }
}
