//Course Search

$(document).ready(function () {
    let search;
    $(document).on("keyup", "#search-input", function () {

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