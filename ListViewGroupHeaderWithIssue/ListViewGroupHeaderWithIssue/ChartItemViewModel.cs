using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;


namespace FormsAnywhere.Client.ViewModels
{
    public class ChartItemViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Icon { get; set; }

        public string FullName
        {
            get { return LastName + "," + FirstName; }
        }
    }

    public class MenuGroup : ObservableCollection<ChartItemViewModel>, INotifyPropertyChanged
    {
        private bool _expanded;

        public string title { get; set; }

        public string ShortName { get; set; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded = value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");

                }
            }
        }




        public string StateIcon
        {
            get
            {
                if (this.title == "View Charts")
                    return Expanded ? "collapse-arrow.png" : "expand-arrow.png";
                else return null;
            } 
        }


        public int MenuCount { get; set; }



        public MenuGroup(string Title, string shortName, bool expanded = true)
        {

            title = Title;

            ShortName = shortName;

            Expanded = expanded;

        }



        public static ObservableCollection<MenuGroup> All { private set; get; }



        static MenuGroup()
        {

            loadData();
        }

        public static void loadData()
        {
            ObservableCollection<MenuGroup> Groups = new ObservableCollection<MenuGroup>{

                new MenuGroup("Create Chart","C"){

                }
                ,
                new MenuGroup("Settings","S"){

                }
            };

            //ChartItemViewModel itemViewModel = new ChartItemViewModel();

            MenuGroup group = new MenuGroup("View Charts", "V");

            //var cases = CaseService.GetCasesByProfile(App.activeProfile.ID);


                ChartItemViewModel itemViewModel = new ChartItemViewModel
                {
                    FirstName = "Jon",
                    LastName = "Smith",
                    Icon = "ChartIcon.png"

                };
            group.Add(itemViewModel);
            ChartItemViewModel itemViewModel1 = new ChartItemViewModel
            {
                FirstName = "Bob",
                LastName = "Smith",
                Icon = "ChartIcon.png"

            };
            group.Add(itemViewModel1);



            ;
            Groups.Add(group);
            All = Groups;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)

        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


    }
}