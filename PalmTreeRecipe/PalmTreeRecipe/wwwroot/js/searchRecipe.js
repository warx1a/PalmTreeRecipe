$(document).ready(function () {

    $("#SearchByUserID").val("");

    $("#SearchByUserID").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "./GetAllUsers",
                type: "GET",
                success: function (data) {
                    console.log(data);
                    response($.map(data, function (user) {
                        return user.firstName + " " + user.lastName;
                    }));
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        minLength: 2
    });
});