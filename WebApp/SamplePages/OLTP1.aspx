<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OLTP1.aspx.cs" Inherits="WebApp.SamplePages.OLTP1" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .grid-1 {
            display:grid;
            grid-template-columns: auto auto;
            grid-template-rows: auto;
            grid-gap:1em;
            justify-content: start;

        }
        .grid-2 {
            display:grid;
            grid-template-rows:repeat(4, minmax(auto, auto));
            justify-content: start;

        }
        .grid-2 > div {
            display:grid;
            grid-template-columns:repeat(3, auto);
            justify-content: start;

        }
        .item-e {
            grid-column: 2 / 3;
            grid-row: 1 / 2;
            align-self: start;
        }
    </style>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div class="grid-1">
        <div class="grid-2">
            <div>   
                <asp:Label ID="Label1" runat="server" Text="Artist" ></asp:Label>
                <asp:TextBox ID="ArtistName" runat="server" 
                     placeholder="artist name" Width="150"> 
                </asp:TextBox>
                <asp:Button ID="ArtistFetch" runat="server" Text="Fetch" 
                    OnCommand="Button_Command" CommandName="Artist"/>
            </div>
            <div>
                 <asp:Label ID="Label2" runat="server" Text="Media"></asp:Label>
                <asp:DropDownList ID="MediaTypeDDL" runat="server" Width="150">
                </asp:DropDownList>
                <asp:Button ID="MediaTypeFetch" runat="server" Text="Fetch" 
                     OnCommand="Button_Command"    CommandName="MediaType"  />
            </div>
            <div>
                 <asp:Label ID="Label3" runat="server" Text="Genre"></asp:Label>
                <asp:DropDownList ID="GenreDDL" runat="server" Width="150">
                </asp:DropDownList>
                <asp:Button ID="GenreFetch" runat="server" Text="Fetch" 
                    OnCommand="Button_Command" CommandName="Genre" />
            </div>
            <div>
                 <asp:Label ID="Label4" runat="server" Text="Album"></asp:Label>
                <asp:TextBox ID="AlbumTitle" runat="server" ToolTip="Enter an partial album title"
                     placeholder="album title" Width="150">
                </asp:TextBox>
                <asp:Button ID="AlbumFetch" runat="server" Text="Fetch" 
                    OnCommand="Button_Command" CommandName="Album" />
            </div>
        </div>
        <div class="item-e">
        <asp:Panel ID="QueryPanel" runat="server" Visible="true">
            <asp:Label ID="TracksBy" runat="server" ></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="SearchArg" runat="server" ></asp:Label>
        </asp:Panel>
        <span>aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa</span>
        </div>
    </div>
    
</asp:Content>
