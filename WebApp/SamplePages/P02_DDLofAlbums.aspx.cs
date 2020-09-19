using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class P02_DDLofAlbums : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "0")
            {
                MessageLabel.Text = "Please Select an Album";
            }
            else
            {
                var selectedindex = DropDownList1.SelectedIndex;
                var selectedvalue = DropDownList1.SelectedValue;
                var selecteditem = DropDownList1.SelectedItem;
                MessageLabel.Text = $"SelectedIndex:{selectedindex} " +
                                    $" SelectedValue:{selectedvalue} " +
                                    $" SelectedItem:{selecteditem}";
            }
            
        }
    }
}