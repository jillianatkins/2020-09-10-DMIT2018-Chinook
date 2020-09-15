<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="P01_DDLofArtists.aspx.cs" Inherits="WebApp.SamplePages.P01_DDLofArtists" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <asp:Label ID="Label1" runat="server" Text="Select Artist"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="DDLofArtists" runat="server"
            DataSourceID="ODS_DDLofArtists"
            DataTextField="ArtistName"
            DataValueField="ArtistId"
            Width="350px"
            AppendDataBoundItems="true">
            <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
        <asp:LinkButton
            ID="DisplaySelectedInfo"
            runat="server" OnClick="DisplaySelectedInfo_Click">
            Display Selected Info
        </asp:LinkButton>
    </div>
    <br /><br />
    <div class="row">
        <asp:Label ID="MessageLabel" runat="server" ></asp:Label>
    </div>
    <asp:ObjectDataSource ID="ODS_DDLofArtists" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="Artist_List"
        TypeName="ChinookSystem.BLL.ArtistController">
    </asp:ObjectDataSource>
</asp:Content>
