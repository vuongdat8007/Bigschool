$(document).ready(function () {
    $.validator.addMethod("phoneVN", function (value, element) {
        // Vietnamese phone number pattern
        var pattern = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        return this.optional(element) || pattern.test(value);
    }, "Please enter a valid VN phone number");

    $.validator.addMethod("emailDomain", function (value, element) {
        // Regular expression to match email with specific domain
        var emailRegEx = /^[\w-]+(\.[\w-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9]+)*(\.[A-Za-z]{2,})$/;
        return this.optional(element) || emailRegEx.test(value);
    }, "Please enter a valid email address with the correct domain.");

    // Initialize the validation
    var validator = $("#cbnvForm").validate({
        onfocusout: function (element) {
            this.element(element);
        },
        errorClass: "invalid-feedback",
        errorElement: "div",
        rules: {
            email: {
                required: true,
                email: true,
                emailDomain: true
            },
            dienThoai: {
                required: true,
                phoneVN: true
            },
            soCMND: {
                required: true,
                minlength: 9,
                maxlength: 12,
                number: true
            }
        },
        messages: {
            email: {
                required: "Please enter an email address",
                email: "Please enter a valid email address",
                emailDomain: "Please enter valid domain name of your email hosting"
            },
            dienThoai: {
                required: "Please enter a phone number",
                phoneVN: "Please enter a valid VN phone number"
            },
            soCMND: {
                required: "Please enter an ID number",
                minlength: "ID number must be between 9 and 12 characters",
                maxlength: "ID number must be between 9 and 12 characters",
                number: "Please enter a valid ID number"
            }
        },
        highlight: function (element, errorClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        errorPlacement: function (error, element) {
            /*error.addClass("invalid-feedback");
            element.closest(".form-group").append(error);*/
            error.addClass("invalid-feedback");
            if (element.is("select")) {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element);
            }
        },
        submitHandler: function (form, event) {
            event.preventDefault(); // Prevent the default form submission behavior
            saveCBNV(); // Call the saveCBNV function when the form is valid
        }
    });


});
