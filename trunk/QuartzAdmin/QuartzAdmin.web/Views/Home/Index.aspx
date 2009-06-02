<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<QuartzAdmin.web.Models.InstanceModel>>" %>


<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome to Quartz Admin</h2>
    <p>
    
        <ul>
        <%
        foreach (var instance in Model)
          {%>
            <li><%=Html.ActionLink(instance.InstanceName, "Index", "Job", new { id = instance.InstanceName }, null)%> <%=Html.ActionLink("Edit", "Details", "Instance", new{id=instance.InstanceName}, null) %></li>
        
        <%} %>        
        </ul>
        
    </p>
</asp:Content>
