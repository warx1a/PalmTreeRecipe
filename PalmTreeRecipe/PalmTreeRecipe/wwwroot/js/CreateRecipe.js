$(document).ready(function () {

    var recipeSteps = [];
    var recipeIngredients = [];

    $("#ingredientDialog").hide();
    $("#stepDialog").hide();

    //add the modal for adding an ingredient to a recipe
    $("#btnAddIngredient").click(function () {
        $("#ingredientDialog").dialog({
            buttons: [
                {
                    text: "Add",
                    click: function () {
                        var ingredient = $("#txtIngredientName").val();
                        var quantity = $("#txtQuantity").val();
                        if (ingredient == "" || quantity == "") {
                            $(".errorMessages").append("<p class='errorMessage'>Ingredient name and quantity must be filled out</p>");
                            return;
                        }
                        //build out the table row to add to the current ingredients table
                        var ingredientTR = "<tr>";
                        ingredientTR += "<td>" + ingredient + "</td>";
                        ingredientTR += "<td>" + quantity + "</td>";
                        ingredientTR += "<td><button type='button' class='removeIngredient'>X</button></td>";
                        ingredientTR += "</tr>";
                        $("#tblCurrentIngredients tbody").append(ingredientTR);
                        $("#txtIngredientName").val("");
                        $("#txtQuantity").val("");
                        $(this).dialog("close");
                    },
                    id: "postbackAddIngredient"
                },
                {
                    text: "Cancel",
                    click: function () {
                        $(this).dialog("close");
                    },
                    class: "cancelBtn"
                }
            ],
            modal: true
        });
        $(".button.ui-dialog-titlebar-close").remove();
    });

    //if they click to add a step we want to show the add step modal
    $("#btnAddStep").click(function () {
        $("#stepDialog").dialog({
            buttons: [
                {
                    text: "Add",
                    click: function () {
                        //build out the table row, add it, and reset the step fields
                        var stepText = $("#txtStepValue").val();
                        //if they don't enter anything for the text don't add the step
                        if (stepText == "") {
                            $(".errorMessages").append("<p class='errorMessage'>Step cannot be empty</p>");
                            return;
                        }
                        var stepTR = "<tr>";
                        stepTR += "<td>" + stepText + "</td>";
                        stepTR += "<td><button type='button' class='removeStep'>X</button></td>";
                        stepTR += "</tr>";
                        $("#tblCurrentSteps tbody").append(stepTR);
                        $("#txtStepValue").val("");
                        $(this).dialog("close");
                    },
                    id: "postbackAddStep"
                },
                {
                    text: "Cancel",
                    click: function () {
                        $(this).dialog("close");
                    },
                    class: "cancelBtn"
                }
            ],
            modal: true
        });
        $(".ui-dialog-titlebar-close").remove();
    });

    //if they want to remove a step remove the TR
    $(document).on("click", ".removeStep, .removeIngredient", function () {
        var parentTR = $(this).closest("tr");
        $(parentTR).remove();
    })

    //if they click the create recipe button
    $("#btnCreateRecipe").click(function () {
        $("#tblCurrentIngredients tr").each(function (idx, elem) {
            var ingredientName = $(elem).find("td")[0].innerHTML;
            var ingredientQty = $(elem).find("td")[1].innerHTML;
            var ingredientJSON = {
                name: ingredientName,
                quantity: ingredientQty
            };
            recipeIngredients.push(ingredientJSON);
        });
        $("#tblCurrentSteps tr").each(function (idx, elem) {
            var stepText = $(elem).find("td")[0].innerHTML;
            recipeSteps.push(stepText);
        });
        //go through each ingredient and add it to the hidden
        var hdnIngredientVal = "[";
        for (var i = 0; i < recipeIngredients.length; i++) {
            var ingredient = recipeIngredients[i];
            hdnIngredientVal += '{"name":"' + ingredient.name + '", "quantity":"' + ingredient.quantity + '"},';
        }
        hdnIngredientVal = hdnIngredientVal.substring(0, hdnIngredientVal.length - 1);
        hdnIngredientVal += "]";
        $("#EnteredIngredients").val(hdnIngredientVal);
        //go through each step and add it to the hidden
        var stepVal = "[";
        for (var i = 0; i < recipeSteps.length; i++) {
            var step = recipeSteps[i];
            stepVal += '{"text": "' + step + '"},';
        }
        stepVal = stepVal.substring(0, stepVal.length - 1);
        stepVal += "]";
        $("#EnteredSteps").val(stepVal);
    });
});