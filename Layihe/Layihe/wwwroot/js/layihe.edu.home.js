//Courses Search

$(document).ready(function () {
    let search;
    $(document).on("keyup", "#search-course-input", function () {

        search = $(this).val().trim();
        
        $("#new-courses").empty()

        if (search.length > 0) {
            $.ajax({
                url: '/Course/Search?search=' + search,
                type: "Get",
                success: function (res) {
                    $("#old-courses").css("display", "none")
                    $("#new-courses").append(res)
                }
            });
        }
        else {
            $("#old-courses").css("display", "block")
        }
    });
});

//Events Search

$(document).ready(function () {
    let search;
    $(document).on("keyup", "#search-event-input", function () {

        search = $(this).val().trim();

        $("#new-events").empty()

        if (search.length > 0) {
            $.ajax({
                url: '/Event/Search?search=' + search,
                type: "Get",
                success: function (res) {
                    $("#old-events").css("display", "none")
                    $("#new-events").append(res)
                }
            });
        }
        else {
            $("#old-events").css("display", "block")
        }
    });
});

//Teachers Search

$(document).ready(function () {
    let search;
    $(document).on("keyup", "#search-teacher-input", function () {

        search = $(this).val().trim();

        $("#new-teachers").empty()

        if (search.length > 0) {
            $.ajax({
                url: '/Teacher/Search?search=' + search,
                type: "Get",
                success: function (res) {
                    $("#old-teachers").css("display", "none")
                    $("#new-teachers").append(res)
                }
            });
        }
        else {
            $("#old-teachers").css("display", "block")
        }
    });
});