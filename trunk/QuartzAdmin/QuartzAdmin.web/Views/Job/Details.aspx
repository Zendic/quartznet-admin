<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Quartz.JobDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Name:
            <%= Html.Encode(Model.Name) %>
        </p>
        <p>
            Group:
            <%= Html.Encode(Model.Group) %>
        </p>
        <p>
            FullName:
            <%= Html.Encode(Model.FullName) %>
        </p>
        <p>
            Description:
            <%= Html.Encode(Model.Description) %>
        </p>
        <p>
            RequestsRecovery:
            <%= Html.Encode(Model.RequestsRecovery) %>
        </p>
        <p>
            Volatile:
            <%= Html.Encode(Model.Volatile) %>
        </p>
        <p>
            Durable:
            <%= Html.Encode(Model.Durable) %>
        </p>
        <p>
            Stateful:
            <%= Html.Encode(Model.Stateful) %>
        </p>
        <p>
            Job Data Map
            <ul>
            <%foreach (System.Collections.DictionaryEntry d in Model.JobDataMap)
              {%>
              <li><%=Html.Encode(d.Key) %> - <%=Html.Encode(d.Value) %></li>
            <%} %>
            </ul>
        </p>
    </fieldset>
    <p>
        <%=Html.ActionLink("Back to List", "Details", "Group", new { id = ViewData["groupName"] }, null)%>
        
        <%  var rv = new RouteValueDictionary();
            rv["groupName"] = Model.Group;
            rv["itemName"] = Model.Name;
            rv["lastRunDate"] = DateTime.Now;
 %>
 
    <a href="javascript:void(0)" id="showRunJobNow">Run Job Now</a>
    </p>
    <div id="status_msg"></div>
    <div id="dialog1">
    <div class="hd">Job Data Map</div> 
        <div class="bd">     
        
            <form id="runnow_form" action="<%=Url.Action("RunNow", "JobExecution", new {instanceName=ViewData["instanceName"], groupName=Model.Group, itemName=Model.Name}) %>" method="post">
                    <%
                        foreach (System.Collections.DictionaryEntry d in Model.JobDataMap)
                      {%>
                      <label for="jdm_<%=Html.Encode(d.Key)%>"><%=Html.Encode(d.Key) %></label>
                      <input type="text" id="jdm_<%=Html.Encode(d.Key)%>" name="jdm_<%=Html.Encode(d.Key)%>" value="<%=Html.Encode(d.Value) %>"/>
                      <br />
                    <%} %>
            </form>
        </div>
    </div>
<script type="text/javascript">
 
function AnimateStatusMessage() {
    $("#status_msg").animate({ fontSize: "1.5em" }, 400);
}
 
</script>

<script type="text/javascript">
/*
    $(document).ready(function() {
        $("#runnow_form").submit(function() {
            var f = $("#runnow_form");
            var action = f.attr("action");
            var serializedForm = f.serialize();
            $.post(action, serializedForm)
            return false;
        });
    });
    */
</script>

<script type="text/javascript">
    YAHOO.namespace("bdr");

    function init() {

        // Define various event handlers for Dialog
        var handleSubmit = function() {
            //var f = $("#runnow_form");
            //var action = f.attr("action");
            //var serializedForm = f.serialize();
            //$.post(action, serializedForm)
            this.submit();

        };
        var handleCancel = function() {
            this.cancel();
        };
        var handleSuccess = function(o) {
            var response = o.responseText;
            response = response.split("<!")[0];
            document.getElementById("status_msg").innerHTML = response;
            AnimateStatusMessage();
        };
        var handleFailure = function(o) {
            alert("Submission failed: " + o.status);
        };

        // Instantiate the Dialog
        YAHOO.bdr.dialog1 = new YAHOO.widget.Dialog("dialog1",
							{ width: "40em",
							    fixedcenter: true,
							    visible: false,
							    constraintoviewport: true,
							    buttons: [{ text: "Submit", handler: handleSubmit, isDefault: true },
								      { text: "Cancel", handler: handleCancel}]
							});

        // Wire up the success and failure handlers
        YAHOO.bdr.dialog1.callback = { success: handleSuccess,
            failure: handleFailure
        };

        // Render the Dialog
        YAHOO.bdr.dialog1.render();

        YAHOO.util.Event.addListener("showRunJobNow", "click", YAHOO.bdr.dialog1.show, YAHOO.bdr.dialog1, true);
    }

    YAHOO.util.Event.onDOMReady(init);
</script>


</asp:Content>

