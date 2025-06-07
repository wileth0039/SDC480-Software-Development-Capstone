// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Filter the application cards based on the search input
$(document).on('input', '#appSearch', function () {
    var search = $(this).val().toLowerCase();
    $('[id^="card_"]').each(function () {
        var cardValue = $(this).attr('data-value').toLowerCase();
        if (cardValue.indexOf(search) === -1) {
            $(this).slideUp();
        } else {
            $(this).slideDown();
        }
    });
});

//Submit a form to add a new grade for an application form factor
$(document).on('click', '[data-custom-formSubmit]', function () {
    var submitType = $(this).attr('data-custom-formSubmit');
    if (submitType === "UserRatingSubmit") {
        var formFactorId = $(this).attr('data-custom-formfactor');

        // Find the selected radio input with the same form factor id
        var selectedRadio = $('input[type="radio"][data-custom-formfactor="' + formFactorId + '"]:checked');
        var selectedGrade = selectedRadio.val();

        // Find the textbox with the same form factor id
        var commentsBox = $('input[type="text"][data-custom-formfactor="' + formFactorId + '"]');
        var comments = commentsBox.val();

        // Validate that a grade is selected before submitting
        if (!selectedGrade) {
            alert("Please select a usability grade before submitting.");
            return;
        }

        console.log("Form Factor ID: " + formFactorId);
        console.log("Selected Grade: " + selectedGrade);
        console.log("Comments: " + comments);
        // Update the hidden form inputs with the selected grade and comments
        $('#UR_FormFactorTypeId').val(formFactorId);
        $('#UR_GradeOption').val(selectedGrade);
        $('#UR_Comments').val(comments);

        // Submit the form
        $('#UserRatingSubmit').submit();
    }
});