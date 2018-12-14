// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var s = $('input'),
    f = $('form'),
    a = $('.after'),
    m = $('h4');

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