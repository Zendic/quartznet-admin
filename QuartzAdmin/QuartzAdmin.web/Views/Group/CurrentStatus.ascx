<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="triggerStatuses"></div> 

<script type="text/javascript">

    YAHOO.namespace("bdr");
    YAHOO.util.Event.addListener(window, "load", function() {
        YAHOO.bdr.loadStatuses = new function() {
            this.formatUrl = function(elCell, oRecord, oColumn, sData) {
                elCell.innerHTML = "<a href='<%=ResolveUrl("~/Job/Details/" + ViewData["groupName"] + "/")%>" + sData + "'>" + sData + "</a>";
            };

            var myColumnDefs = [
            { key: "JobName", label: "Job", sortable: true, formatter: this.formatUrl },
            { key: "TriggerName" },
            { key: "StateDesc" },
            { key: "NextFireTime" },
            { key: "LastFireTime" }
            /*{key:"Rating.AverageRating", label:"Rating", formatter:YAHOO.widget.DataTable.formatNumber, sortable:true}*/
        ];

            var myDataSource = new YAHOO.util.DataSource("<%=ResolveUrl("~/JobExecution/GetCurrentTriggerStatusList/") %>");
            myDataSource.responseType = YAHOO.util.DataSource.TYPE_JSARRAY;
            myDataSource.connXhrMode = "queueRequests";
            //[{GroupName => big.report.delivery, TriggerName => big.NewApplicationTrigger, JobName => big.NewApplicationJob, State => 0, StateDesc => Normal}]
            myDataSource.responseSchema = {
                //resultsList: "ResultSet.Result",
                //fields: ["Title", "Phone", "City", { key: "Rating.AverageRating", parser: "number" }, "ClickUrl"]
                fields: ["GroupName", "TriggerName", "JobName", "State", "StateDesc", "NextFireTime", "LastFireTime"]
            };

            var myDataTable = new YAHOO.widget.DataTable("triggerStatuses", myColumnDefs,
                myDataSource, { initialRequest: "<%=ViewData["groupName"]%>" });

            var myCallback = function() {
                this.set("sortedBy", null);
                this.onDataReturnAppendRows.apply(this, arguments);
            };
            
// Set up polling
        var pollingCallback = {
            success: myDataTable.onDataReturnInitializeTable,
            failure: function() {
                YAHOO.log("Polling failure", "error");
            },
            scope: myDataTable
        }
        myDataSource.setInterval(5000, "<%=ViewData["groupName"]%>", pollingCallback)
        return { 
	            oDS: myDataSource, 
	            oDT: myDataTable 
	        };                
            /*
            var callback1 = {
                success: myCallback,
                failure: myCallback,
                scope: this.myDataTable
            };
            this.myDataSource.sendRequest("query=mexican&zip=94089&results=10&output=json",
                callback1);

            var callback2 = {
                success: myCallback,
                failure: myCallback,
                scope: this.myDataTable
            };
            this.myDataSource.sendRequest("query=chinese&zip=94089&results=10&output=json",
                callback2);
                */
        };
    });
</script>


