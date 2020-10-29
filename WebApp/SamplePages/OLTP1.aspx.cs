using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class OLTP1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Command(Object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            TracksBy.Text = e.CommandName;
            switch (e.CommandName)
            {
                case ("Artist"):
                    if (string.IsNullOrEmpty(ArtistName.Text))
                        MessageUserControl.ShowInfo("Entry Error", "Select an artist name or part of.");
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
                        MessageUserControl.ShowInfo("Entry Error", "Enter an album title or part of.");
                    else
                        SearchArg.Text = AlbumTitle.Text;
                    break;
            }
            //TracksSelectionList.DataBind();
        }
    }
}