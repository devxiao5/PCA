$(document).ready(function () {
    $(".workflow-link").click(function () {
        var linktextstatus = $(this).attr("data-workflow-status");
        window.location.replace("/DailyReport/WorkflowRedirect?project=0" + "&status=" + linktextstatus);

    })
});

$(document).ready(function () {
    $(".list-group-item").click(function () {
        alert($(this).attr("class"));
        var workflowListItemType = $(this).attr("data-workflow-type");
        var status = $(this).text;
        
        switch (workflowListItemType) {
            case "dr":
                function GetDailyReports(status) {
                    $.ajax({
                        url: '@Url.Action("DailyReport", "Dashboard")',
                        data: { status: workflowListItemStatus },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            $.each($.parseJSON(result), function () {
                                alert(this.id);
                            })
                        }
                    })
                }
                break;
        }
    })
})
