$(document).ready(function () {

    sessionStorage.setItem("Project", "0");

    $('.projectselection').click(function () {
        $('#SelectProject').html('' + $(this).attr("data-project-name"));
        sessionStorage.setItem("Project", $(this).attr("data-project-id"));
    })

    $(".workflow-link").click(function () {
        var linktext = $(this).attr("data-workflow-status");
        var linktextproject = sessionStorage.getItem("Project");

        window.location.replace("/DailyReport/WorkflowRedirect?project=" + linktextproject + "&status=" + linktext);

    })



});
