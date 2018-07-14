//fixes dropdown menu issue when container has overflow:auto
$(function () {
    $(document).on("click", ".dataTable .dropdown-toggle", function () {
        dropDownFixPosition($(this));
    });

    $(window).on("load scroll resize", function () {
        $(".dataTable .dropdown-menu").each(function () {
            dropDownFixPosition($(this).siblings('.dropdown-toggle'));
        });
    });

    function dropDownFixPosition($button) {
        var isDropup = $button.parent().hasClass('dropup');

        var $dropdown = $button.siblings(".dropdown-menu");
        if (!$dropdown.is(':visible')) return;

        var dropDownWidth = $dropdown.outerWidth();
        var dropdownHeight = $dropdown.outerHeight();
        var btnBoundingRect = $button[0].getBoundingClientRect();
        var leftPos = btnBoundingRect.right - dropDownWidth;

        var style = {
            position: 'fixed',
            left: leftPos + 'px',
            width: dropDownWidth + 'px'
        };

        var dropDownTop = 0;
        if (!isDropup) {
            dropDownTop = btnBoundingRect.top + $button.outerHeight();
            style.top = dropDownTop + 'px';
        }
        else {
            dropDownTop = btnBoundingRect.top - $button.outerHeight();
            style.bottom = 'auto';
            style.top = (dropDownTop - dropdownHeight) + 'px';
        }
        $dropdown.css(style);
    }
});