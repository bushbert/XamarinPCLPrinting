using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FormsAnywhere.Client.ViewModels;

namespace FormsAnywhere.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpandableMenu : ContentPage

    {

        private ObservableCollection<MenuGroup> _allGroups;

        private ObservableCollection<MenuGroup> _expandedGroups;



        public ExpandableMenu()
        {
            
            InitializeComponent();

            _allGroups = MenuGroup.All;
            this.Title = "Menu";

            UpdateListContent();

            //BackgroundColor = Color.Black;



           
            


        }

        public ImageSource CaseIcon
        {
            get
            {
                return ImageSource.FromResource("Charticon.png");
            }
        }

        private void HeaderTapped(object sender, EventArgs args)
        {
            var button = (Button) sender;
            var menuGroup = (MenuGroup) (button).CommandParameter;
            int selectedIndex = _expandedGroups.IndexOf(menuGroup);


            


            _allGroups[selectedIndex].Expanded = !_allGroups[selectedIndex].Expanded;

            UpdateListContent();

            switch (menuGroup.title)
            {
                case "Create Chart":

                   // CaseService.CreateCase();

                    break;
            }

        }

        [ContentProperty(nameof(Source))]
        public class ImageResourceExtension : IMarkupExtension
        {
            public string Source { get; set; }

            public object ProvideValue(IServiceProvider serviceProvider)
            {
                if (Source == null)
                {
                    return null;
                }

                // Do your translation lookup here, using whatever method you require
                var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

                return imageSource;
            }
        }

        private void UpdateListContent()

        {
           
            
            _expandedGroups = new ObservableCollection<MenuGroup>();

            foreach (MenuGroup group in _allGroups)

            {

                //Create new FoodGroups so we do not alter original list

                MenuGroup newGroup = new MenuGroup(group.title, "", group.Expanded);

                //Add the count of food items for Lits Header Titles to use

                newGroup.MenuCount = group.Count;

                if (group.Expanded)

                {

                    foreach (ChartItemViewModel item in group)

                    {

                        newGroup.Add(item);

                    }

                }

                _expandedGroups.Add(newGroup);

            }

            GroupedView.ItemsSource = _expandedGroups;

        }

        private void Cell_OnTapped(object sender, EventArgs e)
        {
            /*
            var item = ();

            switch (menuGroup.title)
            {
                case "Create Chart":

                    break;
            }
            */
        }
    }
}