using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using System.ComponentModel;
using ChinookSystem.BLL;
using ChinookSystem.DAL;
using ChinookSystem.VIEWMODELS;
using ChinookSystem.ENTITIES;
using Microsoft.Ajax.Utilities;
#endregion

namespace WebApp.SamplePages
{
    public partial class P08_OLTP_Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArtistName.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            AlbumTitle.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            TextBoxUserName.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            NewPlayListName.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            //TODO: #7 Check if a user is logged in
            //if (Request.IsAuthenticated)
            //{
            //    MessageUserControl.ShowInfo("Login Successful");
            //}
            //else
            //{
            //    Response.Redirect("~/Account/Login.aspx");
            //}
        }

        #region UserNameCheck
        protected void CheckForValidUserName(object sender, EventArgs e)
        {
            var userNameIsValid = UserNameCheck();
            if (userNameIsValid == false)
            {
                MessageUserControl.ShowInfo("", "ERROR: User Name is NOT VALID");
                MyPlayList.DataSource = null;
                MyPlayList.DataBind();
            }
            else
            {
                ExistingPlayListDDL.DataBind();
            }
        }

        private bool UserNameCheck()
        {
            PlayListController sysmgr = new PlayListController();
            var userNameIsValid = sysmgr.UserNameIsValid(TextBoxUserName.Text);
            return userNameIsValid;
        }
        #endregion

        #region TrackList Item Command and Building of the GridView
        protected void Tracks_Button_Command(Object sender, System.Web.UI.WebControls.CommandEventArgs e)
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

        protected void TracksSelectionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToMyPlayList")
            {
                UserPlayListTrack item = GetTrackFromTracksListToAddToPlayList(e.Item);
                List<UserPlayListTrack> playListItems = GetPlayListItemsFromGridView();
                bool trackAlreadyInPlaylist = false;
                foreach (UserPlayListTrack i in playListItems)
                {
                    if (item.TrackID == i.TrackID)
                        trackAlreadyInPlaylist = true;
                }
                if (trackAlreadyInPlaylist)
                    MessageUserControl.ShowInfo("", "ERROR: Cannot have duplicate tracks in playlist");
                else
                {
                    playListItems.Insert(0, item);
                    MyPlayList.DataSource = playListItems;
                    MyPlayList.DataBind();
                }
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
                UnitPrice = item.FindLabel("UnitPriceLabel").Text.ToDecimal()
            };
            return track;
        }
        List<UserPlayListTrack> GetPlayListItemsFromGridView()
        {
            var list = new List<UserPlayListTrack>();
            foreach (GridViewRow row in MyPlayList.Rows)
            {
                var item = new UserPlayListTrack
                {
                    TrackID = row.FindLabel("TrackId").Text.ToInt(),
                    TrackNumber = row.FindLabel("TrackNumber").Text.ToInt(),
                    TrackName = row.FindLabel("TrackName").Text,
                    Milliseconds = row.FindLabel("Milliseconds").Text.ToInt(),
                    UnitPrice = row.FindLabel("UnitPrice").Text.ToDecimal()
                };
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region PlaylistDropDown
        protected void PlaylistDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //how do we do error handling using MessageUserControl if the
            //   code executing is NOT part of an ODS
            //you could use Try/Catch (BUT WE WON'T)
            //if you examine the source code of MessageUserControl, you will
            //  find embedded within the code the Try/Catch
            //the syntax:
            //  MessageUserControl.TryRun( () => {coding block});
            //  MessageUserControl.TryRun( () => {coding block},"success title","successmessage");
            MessageUserControl.TryRun(() => {
                PlayListController sysmgr = new PlayListController();
                List<UserPlayListTrack> info = sysmgr.ListExistingPlayList
                    (ExistingPlayListDDL.SelectedValue);
                MyPlayList.DataSource = info;
                MyPlayList.DataBind();
                ButtonSavePlayList.Text = "Save (Playlist # " + ExistingPlayListDDL.SelectedValue + ")";
            }, "", "SUCCESS: PlayList Retrieved");
            NewPlayListName.Visible = false;

        }
        #endregion

        #region PlayList Item Command New and Save buttons
        protected void PlayList_Buttons_Command(Object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            var userNameIsValid = UserNameCheck();
            if (userNameIsValid == false)
            {
                MessageUserControl.ShowInfo("", "ERROR: (PlayList_Buttons) User Name is NOT VALID");
            }
            else
            {
                switch (e.CommandName)
                {
                    case ("New"):
                        ButtonSavePlayList.Text = "Save (New Playlist)";
                        MyPlayList.DataSource = null;
                        MyPlayList.DataBind();
                        NewPlayListName.Visible = true;
                        break;
                    case ("Save"):
                        List<UserPlayListTrack> playListItems = GetPlayListItemsFromGridView();
                        if (ButtonSavePlayList.Text == "Save (New Playlist)")
                        {
                            if (string.IsNullOrEmpty(NewPlayListName.Text))
                                MessageUserControl.ShowInfo("", "ERROR: Give a new PlayList name.");
                            else
                            {
                                MessageUserControl.TryRun(() =>
                                {
                                    PlayListController sysmgr = new PlayListController();
                                    int id = sysmgr.AddNewPLaylist(NewPlayListName.Text, TextBoxUserName.Text);
                                    ExistingPlayListDDL.DataBind();
                                    ExistingPlayListDDL.SelectedValue = id.ToString();
                                    sysmgr.SavePlayList(ExistingPlayListDDL.SelectedValue.ToInt(), playListItems);
                                }, "", "SUCCESS: New PlayList Saved");
                            }
                        }
                        else
                        {
                            MessageUserControl.TryRun(() =>
                            {
                                PlayListController sysmgr = new PlayListController();
                                sysmgr.SavePlayList(ExistingPlayListDDL.SelectedValue.ToInt(), playListItems);
                            }, "", "SUCCESS: Old PlayList Saved");
                        }
                        break;
                }
            }
        }
        #endregion

        #region PlayList Row Commands (Delete, MoveUp, MoveDown)
        protected void MyPlayList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            List<UserPlayListTrack> playListItems = GetPlayListItemsFromGridView();
            UserPlayListTrack playListItem = playListItems[rowIndex];
            if (e.CommandName == "DeleteFromMyPlayList")
            {
                MessageUserControl.ShowInfo("", "MESSAGE: DeleteFromMyPlayList, index: " +
                    rowIndex.ToString());
                playListItems.Remove(playListItem);
                resetPlayListTrackNumbers(playListItems);
                //MyPlayList.DataSourceID = "";
                MyPlayList.DataSource = playListItems;
                MyPlayList.DataBind();
                e.Handled = true;

            }
            else if (e.CommandName == "MoveUpOnMyPlayList")
            {
                MessageUserControl.ShowInfo("", "MESSAGE: MoveUpOnMyPlayList, index: " +
                    rowIndex.ToString());
                if (rowIndex != 0)
                {
                    playListItems.Remove(playListItem);
                    playListItems.Insert(rowIndex - 1, playListItem);
                    resetPlayListTrackNumbers(playListItems);
                    //MyPlayList.DataSourceID = "";
                    MyPlayList.DataSource = playListItems;
                    MyPlayList.DataBind();
                }
                e.Handled = true;
            }
            else if (e.CommandName == "MoveDownOnMyPlayList")
            {
                MessageUserControl.ShowInfo("", "MESSAGE: MoveDownOnMyPlayList, index: " +
                    rowIndex.ToString());
                if (rowIndex != playListItems.Count - 1)
                {
                    playListItems.Remove(playListItem);
                    playListItems.Insert(rowIndex + 1, playListItem);
                    resetPlayListTrackNumbers(playListItems);
                    //MyPlayList.DataSourceID = "";
                    MyPlayList.DataSource = playListItems;
                    MyPlayList.DataBind();
                }
            }
        }

        void resetPlayListTrackNumbers(List<UserPlayListTrack> playListItems)
        {
            var trackNumber = 1;
            foreach(UserPlayListTrack track in playListItems)
            {
                track.TrackNumber = trackNumber;
                trackNumber++;
            }
        }
        #endregion

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
    #region Web Extensions
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
    #endregion
}