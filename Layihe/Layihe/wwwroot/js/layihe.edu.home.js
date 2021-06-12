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
                    $("#pagination1").css("display", "none")
                    $("#new-courses").append(res)

                }
            });
        }
        else {
            $("#old-courses").css("display", "block")
            $("#pagination1").css("display", "block")

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
                    $("#pagination1").css("display", "none")
                    $("#new-events").append(res)
                }
            });
        }
        else {
            $("#old-events").css("display", "block")
            $("#pagination1").css("display", "block")
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
                    $("#pagination1").css("display", "none")
                    $("#new-blogs").append(res)
                }
            });
        }
        else {
            $("#old-blogs").css("display", "block")
            $("#pagination1").css("display", "block")
        }
    });
});

//Teacher Search

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
                    $("#pagination1").css("display", "none")
                    $("#new-teachers").append(res)
                }
            });
        }
        else {
            $("#old-teachers").css("display", "block")
            $("#pagination1").css("display", "block")
        }
    });
});

// Home Page Search

$(document).ready(function () {
    let search;

    $(document).on("keyup", "#search-home-input", function () {
        search = $(this).val().trim();

        $(`#home-search #global-search`).remove();

        if (search.length > 0) {
            $.ajax({
                url: '/Home/Search?search=' + search,
                type: "Get",
                success: function (res) {

                    $(`#home-search`).append(res)
                }
            });
        }
    })
})

//Subscriber

$(document).ready(function () {
    let subscriber;
    $(document).on("click", `#btn-subs`, function () {
        subscriber = $("#inp-subs").val();

        $("#span-subs").empty();

        $.ajax({
            url: "Home/Subsriber?email=" + subscriber,
            type: "Post",
            success: function (res) {
                console.log("ok")

                $("#span-subs").append(res);
            }
        });
    });
});
