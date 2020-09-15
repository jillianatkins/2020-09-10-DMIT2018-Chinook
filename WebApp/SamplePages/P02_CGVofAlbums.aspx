<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="P02_CGVofAlbums.aspx.cs" Inherits="WebApp.SamplePages.P02_CGVofAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Custom Grid View of Albums</h1>
    <div class="row">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ODS_CGV" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="AlbumId" HeaderText="AlbumId" SortExpression="AlbumId"></asp:BoundField>
                <asp:BoundField DataField="AlbumTitle" HeaderText="AlbumTitle" SortExpression="AlbumTitle"></asp:BoundField>
                <asp:BoundField DataField="ArtistId" HeaderText="ArtistId" SortExpression="ArtistId"></asp:BoundField>
                <asp:BoundField DataField="AlbumReleaseYear" HeaderText="AlbumReleaseYear" SortExpression="AlbumReleaseYear"></asp:BoundField>
                <asp:BoundField DataField="AlbumReleaseLabel" HeaderText="AlbumReleaseLabel" SortExpression="AlbumReleaseLabel"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ODS_CGV" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Albums_List" TypeName="ChinookSystem.BLL.AlbumController">
    </asp:ObjectDataSource>
</asp:Content>
