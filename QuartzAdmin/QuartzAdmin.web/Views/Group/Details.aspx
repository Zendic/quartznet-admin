<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<QuartzAdmin.web.Models.GroupViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dashboard for <%=Model.GroupName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dashboard for <%=Model.GroupName %></h2>
    <h3>Jobs</h3>
        <%
        foreach (var job in Model.Jobs)
      {%>
        <li><%=Html.ActionLink(job.Name, "Details", "Job", new{groupName=Model.GroupName, itemName=job.Name}, null) %></li>
    
    <%} %>
    
    <h3>Triggers</h3>
        <%
        foreach (var trigger in Model.Triggers)
      {%>
        <li><%=Html.ActionLink(trigger.Name, "Details", "Trigger", new { groupName = Model.GroupName, itemName = trigger.Name }, null)%></li>
    
    <%} %>
    
    <h3>Current Status</h3>
    <%
        ViewDataDictionary vdd = new ViewDataDictionary();
        vdd.Add("groupName", Model.GroupName);
        Html.RenderPartial("CurrentStatus", vdd);
     %>
    

</asp:Content>
