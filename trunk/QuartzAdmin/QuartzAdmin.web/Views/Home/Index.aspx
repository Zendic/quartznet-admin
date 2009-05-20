<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<string>>" %>


<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome to Quartz Admin</h2>
    <p>
    
        <ul>
        <%
        foreach (var group in Model)
          {%>
            <li><%=Html.ActionLink(group, "Details", "Group", new{id=group}, null) %></li>
        
        <%} %>        
        </ul>
        
    </p>
</asp:Content>
