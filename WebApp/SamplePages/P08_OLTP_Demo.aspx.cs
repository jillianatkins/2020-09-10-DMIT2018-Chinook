using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using System.ComponentModel;
using ChinookSystem.DAL;
using ChinookSystem.VIEWMODELS;
using ChinookSystem.ENTITIES;
#endregion

namespace WebApp.SamplePages
{
    public partial class P08_OLTP_Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
            MessageUserControl.ShowInfo("");
        }

        protected void Button_Command(Object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            TracksBy.Text = e.CommandName;
            switch (e.CommandName)
            {
                case ("Artist"):
                    if (string.IsNullOrEmpty(ArtistName.Text))
                        MessageUserControl.ShowInfo("", "ERROR: Select an artist name or part of.");
                    else
                        SearchArg.Text = ArtistName.Text;
                    break;
                case ("MediaType"):
                    SearchArg.Text = MediaTypeDDL.SelectedValue;
                    break;
                case ("Genre"):
                    SearchArg.Text = GenreDDL.SelectedValue;
                    break;
                case ("Album"):
                    if (string.IsNullOrEmpty(AlbumTitle.Text))
                        MessageUserControl.ShowInfo("", "ERROR: Enter an album title or part of.");
                    else
                        SearchArg.Text = AlbumTitle.Text;
                    break;
            }
            TracksSelectionList.DataBind();
        }

        protected void MyPlayList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFromMyPlayList")
            {
                MessageUserControl.ShowInfo("", "MESSAGE: DeleteFromMyPlayList");
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                MessageUserControl.ShowInfo("", "MESSAGE: " + rowIndex.ToString());

                //Reference the GridView Row.
                GridViewRow row = MyPlayList.Rows[rowIndex];

                //Fetch value of Name.
                //string name = (row.FindControl("txtName") as TextBox).Text;

                var playListItems = GetPlayListItemsFromGridView();
                playListItems.Remove(playListItems[rowIndex]);
                MyPlayList.DataSource = playListItems;
                MyPlayList.DataBind();
                e.Handled = true;

            }
            else if (e.CommandName == "MoveUpOnMyPlayList")
            {
                MessageUserControl.ShowInfo("", "MESSAGE: MoveUpOnMyPlayList");
            }
            else if (e.CommandName == "MoveDownOnMyPlayList")
            {
                MessageUserControl.ShowInfo("", "MESSAGE: MoveDownOnMyPlayList");
            }
        }





        protected void TracksSelectionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToMyPlayList")
            {
                //LinkButton button = e.Item.FindControl("AddToCart") as LinkButton;
                //button.Enabled = false;
                //button.CssClass += " disabled";

                UserPlayListTrack item = GetTrackFromTracksListToAddToPlayList(e.Item);
                var playListItems = GetPlayListItemsFromGridView();
                playListItems.Insert(0, item);
                MyPlayList.DataSource = playListItems;
                MyPlayList.DataBind();
                e.Handled = true;
            }
        }

        UserPlayListTrack GetTrackFromTracksListToAddToPlayList(ListViewItem item)
        {
            var track = new UserPlayListTrack
            {
                TrackID = item.FindLabel("TrackIDLabel").Text.ToInt(),
                TrackNumber = 1,
                TrackName = item.FindLabel("NameLabel").Text,
                Milliseconds = item.FindLabel("MillisecondsLabel").Text.ToInt(),
                //Bytes = item.FindLabel("BytesLabel").Text.ToInt(),
                UnitPrice = item.FindLabel("UnitPriceLabel").Text.ToDecimal()
            };
            return track;
        }
        IList<UserPlayListTrack> GetPlayListItemsFromGridView()
        {
            var list = new List<UserPlayListTrack>();
            foreach (GridViewRow row in MyPlayList.Rows)
            {
                var item = new UserPlayListTrack
                {
                    TrackID = row.FindLabel("TrackId").Text.ToInt(),
                    TrackNumber = 1,
                    TrackName = row.FindLabel("TrackName").Text,
                    Milliseconds = row.FindLabel("Milliseconds").Text.ToInt(),
                    UnitPrice = row.FindLabel("UnitPrice").Text.ToDecimal()
                };
                list.Add(item);
            }
            return list;
        }

        #region Error Handling
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void InsertCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("", "SUCCESS: Album has been added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void UpdateCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("", "SUCCESS: Album has been updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void DeleteCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("", "SUCCESS: Album has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        #endregion
    }
    public static class WebControlExtensions
    {
        public static Label FindLabel(this Control self, string id)
            => self.FindControl(id) as Label;
        public static TextBox FindTextBox(this Control self, string id)
            => self.FindControl(id) as TextBox;
        public static HiddenField FindHiddenField(this Control self, string id)
            => self.FindControl(id) as HiddenField;
        public static CheckBox FindCheckBox(this Control self, string id)
            => self.FindControl(id) as CheckBox;
        public static int ToInt(this string self) => int.Parse(self);
        public static decimal ToDecimal(this string self) => decimal.Parse(self);
    }
}