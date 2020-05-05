$(document).ready(function () {

    var rating = 0;

    $("#frmRecipeReview").submit(function () {
        $("#rating").val(rating);
    });

    $(".stars input").change(function () {
        rating = $(this).val();
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