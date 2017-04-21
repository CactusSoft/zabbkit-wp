using System.ComponentModel;

namespace CactusSoft.Stierlitz.Common
{
    public enum ScreenName
    {   
        None = 0,
        [Description("Login page")]
        LoginPage,

        [Description("Main panorama")]
        MainPanorama, //? - panorama
        //panorama items
        [Description("Main panorama item: Favorites")]
        FavoritesView,
        [Description("Main panorama item: Overview")]
        OverviewView,
        [Description("Main panorama item: Events")]
        EventsView,
        [Description("Main panorama item: Triggers")]
        TriggersView,

        [Description("Favorites pivot")]
        FavoritesPage, //? - pivot
        //pivot items
        [Description("Favorites pivot item: Graphs")]
        FavoriteGraphs,
        [Description("Favorites pivot item: Triggers")]
        FavoriteTriggers,

        [Description("Period page")]
        PeriodPage,
        [Description("Custom period page")]
        CustomPeriodPage,

        [Description("About page")]
        AboutPage,
        [Description("Events page")]
        EventsPage,
        [Description("Graph page")]
        GraphPage,
        [Description("Graphs page")]
        GraphsPage,
        [Description("Hosts page")]
        DataPage,
        [Description("Data page")]
        HostsPage,
        [Description("Triggers page")]
        HostTriggersPage,
        [Description("Servers page")]
        ServerPickerPage,
        [Description("Server page")]
        ServerPage,
        [Description("All host groups page")]
        AllHostGroupsPage,
        [Description("All events page")]
        AllEventsPage,
        [Description("All triggers page")]
        AllTriggersPage,
    }
}
