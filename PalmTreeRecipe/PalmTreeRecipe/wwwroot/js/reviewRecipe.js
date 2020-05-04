$(document).ready(function () {

    $("#frmRecipeReview").submit(function () {
        $("#rating").val(getStarCount());
        console.log(getStarCount());
    });

    $(".stars input").change(function () {
        var pivotLabel = $("label." + $(this).attr("id"));
        pivotLabel.css("background-image", "url('../images/transparentStar.png')");
        pivotLabel.prevAll("label").css("background-image", "url('../images/emptyStar.png')");
        pivotLabel.nextAll("label").css("background-image", "url('../images/transparentStar.png')");
    });

    function getStarCount() {
        $(".stars input").each(function (idx, elem) {
            if ($(elem).is(":checked")) {
                return idx + 1;
            }
        });
    }

});