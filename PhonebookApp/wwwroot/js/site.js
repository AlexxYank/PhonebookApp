$(document).ready(function () {
    $('#homeTab').click();
    $("#ContactInfo").hide();

});

$(".nav-tabs a").click(function () {
    $(this).tab('show');
});

function RecoverContact(ID) {
    $.ajax({
        type: "POST",
        url: "/Contact/RecoverContact",
        data: { "id": ID },
        dataType: "json"
    });
}

$(function () {
    $('table').on('click', function (e) {
        var x = $(e.target);
        var ID = x.parents('tr').find('#id').html();
        var PhoneNumber = x.parents('tr').find('#pnumber').html();
        var Name = x.parents('tr').find('#name').html();
        var NameArr = Name.split(' ');
        var FirstName = NameArr[0];
        var LastName = NameArr[1];
        var Address = x.parents('tr').find('#address').html();
        var Email = x.parents('tr').find('#email').html();

        $('#ID').val(ID);
        $('#PhoneNumber').val(PhoneNumber);
        $('#FirstName').val(FirstName);
        $('#LastName').val(LastName);
        $('#Address').val(Address);
        $('#Email').val(Email);

        $("#ContactInfo").show();
    });
});
