$('.LinkButton').click(function () {
    if ($("#LinkButtonGroup").css('visibility') == 'hidden') {
        $("#LinkButtonGroup").css({ 'visibility': 'visible', 'max-height': '200px' });
    }

    var selecteditem = document.getElementById('LinkButtonSelected');
    $(selecteditem).removeAttr('id');

    $(this).attr('id', 'LinkButtonSelected');

    var url1 = $(this).attr('data-link-button-1');
    $('#LinkButton1').attr('href', url1);

    var url2 = $(this).attr('data-link-button-2');
    $('#LinkButton2').attr('href', url2);

    var url3 = $(this).attr('data-link-button-3');
    $('#LinkButton3').attr('href', url3);

    var url4 = $(this).attr('data-link-button-4');
    $('#LinkButton4').attr('href', url4);

    var url5 = $(this).attr('data-link-button-5');
    $('#LinkButton5').attr('href', url5);

    var url6 = $(this).attr('data-link-button-6');
    $('#LinkButton6').attr('href', url6);




});