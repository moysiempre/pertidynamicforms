(function ($) {
    'use strict';

    $.fn.uivalidate = function () {
        var forms = this;
        // loop over them and prevent submission
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);

            $(form).each(function () {
                
                $(this).find(':input.numericonly').on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^\d].+/, ""));
                    if ((event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });

                $(this).find(':input.numericwithdecimal').on("keypress keyup blur", function (event) {
                    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                        event.preventDefault();
                    }
                });

                $(this).find(':input[type="email"]').on("keypress keyup blur", function () {
                    var re = /[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}/g;
                    if ($(this).val() == '' || !re.test($(this).val())) {
                        this.setCustomValidity('email must match');
                    } else {
                        this.setCustomValidity(''); 
                    }
                })

                $(this).find(':input[type="tel"]').on("keypress keyup blur", function () {
                    var re = /[0-9]{10}/;
                    if ($(this).val() == '' || !re.test($(this).val())) {
                        this.setCustomValidity('teléfono must match');
                    } else {
                        this.setCustomValidity(''); 
                    }
                })

                $(this).find(':input[type="password"]').on("keypress keyup blur", function () {
                    var re = /(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,}).*$/g;
                    if ($(this).val() == '' || !re.test($(this).val())) {
                        this.setCustomValidity('password must match');
                    } else {
                        this.setCustomValidity(''); 
                    }
                })

            });

         

            form.addEventListener('reset', function (event) {
                form.classList.remove('was-validated');
            });

        });
    };
}(jQuery));