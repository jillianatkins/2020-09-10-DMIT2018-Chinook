<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="P03_CGVofAlbums.aspx.cs" Inherits="WebApp.SamplePages.P03_CGVofAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Custom Grid View of Albums</h1>
    <br /><br />
    <%--CssClass="table table-striped" GridLines="Horizontal" BorderStyle="None"--%>
    <div class="row">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="AlbumODS" AllowPaging="True" PageSize="5" BorderStyle="None" CssClass="table table-striped" GridLines="Horizontal" PagerSettings-FirstPageText="&lt;&lt;" PagerSettings-Mode="NumericFirstLast" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                <asp:BoundField DataField="AlbumId" HeaderText="AlbumId" SortExpression="AlbumId"></asp:BoundField>
                <asp:BoundField DataField="AlbumTitle" HeaderText="AlbumTitle" SortExpression="AlbumTitle"></asp:BoundField>
                <%--Convert asp:BoundField to asp:Template in the wizard,
                add a dropdownlist inside itemtemplate, attach ODS to DDL, and include 
                selectedvalue=--%>
                
                <asp:TemplateField HeaderText="ArtistId" SortExpression="ArtistId">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("ArtistId") %>' ID="TextBox1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("ArtistId") %>' ID="Label1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="AlbumReleaseYear" HeaderText="AlbumReleaseYear" SortExpression="AlbumReleaseYear"></asp:BoundField>
                <asp:BoundField DataField="AlbumReleaseLabel" HeaderText="AlbumReleaseLabel" SortExpression="AlbumReleaseLabel"></asp:BoundField>
                <asp:BoundField DataField="ReleaseLabelAndYear" HeaderText="Label (Year)" ReadOnly="True" SortExpression="ReleaseLabelAndYear"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ArtistODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="AlbumODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Albums_List" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
    </div>
    <br /><br />
    <div class="row">
        <asp:Label ID="MessageLabel" runat="server" ></asp:Label>
    </div>
</asp:Content>
