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

//Blog Search

$(document).ready(function () {
    let search;
    $(document).on("keyup", "#search-blog-input", function () {

        search = $(this).val().trim();

        $("#new-blogs").empty()

        if (search.length > 0) {
            $.ajax({
                url: '/Blog/Search?search=' + search,
                type: "Get",
                success: function (res) {
                    $("#old-blogs").css("display", "none")
                    $("#new-blogs").append(res)
                }
            });
        }
        else {
            $("#old-blogs").css("display", "block")
        }
    });
});

