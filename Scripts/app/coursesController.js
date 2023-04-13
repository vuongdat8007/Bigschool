﻿var CoursesController = function () {
    var button;
    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
        $(".js-toggle-follow").click(toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) {
            doFollowing();
        }
        else {
            unFollowing();
        }
    }

    var doFollowing = function () {
        $.post("/api/followings", { followeeId: button.attr("data-user-id") })
            .done(function () {
                button.text("Following").removeClass("btn-default")
                    .addClass("btn-success");
            })
            .fail(function () {
                alert("Something failed!");
            });
    };

    var unFollowing = function () {
        $.ajax({
            url: "/api/followings/" + button.attr("data-user-id"),
            method: "DELETE"
        })
            .done(function () {
                button.removeClass("btn-success").addClass("btn-default").text("Following?")
            })
            .fail(function () {
                alert("Something went wrong!");
            });
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) {
            createAttendance();
        }
        else {
            deleteAttendance();
        }
    };

    var createAttendance = function () {
        $.post("/api/attendances", { courseId: button.attr("data-course-id") })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function () {
        $.ajax({
            url: "/api/attendances/" + button.attr("data-course-id"),
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something went wrong!");
    };

    return {
        init: init
    }
}();