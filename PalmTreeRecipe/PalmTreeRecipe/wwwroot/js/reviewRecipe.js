$(document).ready(function () {

    $("#frmRecipeReview").submit(function () {
        $("#rating").val(getStarCount());
        console.log(getStarCount());
    });

    function getStarCount() {
        $(".stars input").each(function (idx, elem) {
            if ($(elem).is(":checked")) {
                return idx + 1;
            }
        });
    }

});