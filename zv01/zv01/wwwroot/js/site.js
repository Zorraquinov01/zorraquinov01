var s = $('.header-content input'),
    f = $('.header-content form'),
    a = $('.header-content .after'),
    m = $('.header-content h4');

s.focus(function () {
    if (f.hasClass('open')) return;
    f.addClass('in');
    setTimeout(function () {
        f.addClass('open');
        f.removeClass('in');
    }, 1300);
});

a.on('click', function (e) {
    e.preventDefault();
    if (!f.hasClass('open')) return;
    s.val('');
    f.addClass('close');
    f.removeClass('open');
    setTimeout(function () {
        f.removeClass('close');
    }, 1300);
})

f.submit(function (e) {
    e.preventDefault();
    m.html('Thanks, high five!').addClass('show');
    f.addClass('explode');
    setTimeout(function () {
        s.val('');
        f.removeClass('explode');
        m.removeClass('show');
    }, 3000);
})




function Function() {
    document.getElementById("alert").innerHTML = "";
}
$("#generate").on("click", function () {
    var data = $("#codeData").val();

    var size =150;
    if (data == "") {
        $("#alert").append("<p style='color:#fff;font-size:20px'>Please Enter A Url Or Text</p>"); // If Input Is Blank
        return false;
    } else {
        if ($("#image").is(':empty')) {

            //QR Code Image
            $("#image").append("<img src='http://chart.apis.google.com/chart?cht=qr&chl=" + data + "&chs=" + size + "' alt='qr' />");

            //This Provide An Image Download Link
            $("#link").append("<a style='color:#fff;' href='http://chart.apis.google.com/chart?cht=qr&chl=" + data + "&chs=" + size + "'>Download QR Code</a>");

            //This Provide the Image Link Path In Text
            $("#code").append("<p style='color:#fff;'><strong>Image Link:</strong> http://chart.apis.google.com/chart?cht=qr&chl=" + data + "&chs=" + size + "</p>");
            return false;
        } else {
            $("#image").html("");
            $("#link").html("");
            $("#code").html("");

            //QR Code Image
            $("#image").append("<img src='http://chart.apis.google.com/chart?cht=qr&chl=" + data + "&chs=" + size + "' alt='qr' />");

            //This Provide An Image Download Link
            $("#link").append("<a style='color:#fff;' href='http://chart.apis.google.com/chart?cht=qr&chl=" + data + "&chs=" + size + "'>Download QR Code</a>");

            //This Provide the Image Link Path In Text
            $("#code").append("<p style='color:#fff;'><strong>Image Link:</strong> http://chart.apis.google.com/chart?cht=qr&chl=" + data + "&chs=" + size + "</p>");
            return false;
        }
    }
});