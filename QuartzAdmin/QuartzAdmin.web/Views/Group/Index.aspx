<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<string>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
<ul>
    <%
        foreach (var group in Model)
      {%>
        <li><%=Html.ActionLink(group, "Details", new{id=group}) %></li>
    
    <%} %>
</ul>
</asp:Content>
