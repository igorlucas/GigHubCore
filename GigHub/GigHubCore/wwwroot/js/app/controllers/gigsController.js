var GigsController = function (attendanceService)
{
    var button;

    var init = function (container)
    {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e)
    {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };
    
    var done = function ()
    {
        button
            .toggleClass("btn-info")
            .toggleClass("btn-default");

        //var text = (button.text() == "Indo") ? "Comparecer" : "Indo";
        var text = (button.hasClass("btn-default") ? "Comparecer" : "Indo");

        button.text(text);
    };

    var fail = function ()
    {
        bootbox.alert("Algo deu errado");
    };

    return {
        init: init
    }

}(AttendanceService);