using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class P01_DDLofArtists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void DisplaySelectedInfo_Click(object sender, EventArgs e)
        {
            {
                var selectedindex = DDLofArtists.SelectedIndex;
                var selectedvalue = DDLofArtists.SelectedValue;
                var selecteditem =  DDLofArtists.SelectedItem;
                MessageLabel.Text = $"SelectedIndex:{selectedindex} " +
                                    $" SelectedValue:{selectedvalue} " +
                                    $" SelectedItem:{selecteditem}";
            }
        }
    }
}