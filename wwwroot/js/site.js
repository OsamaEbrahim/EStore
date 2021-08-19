// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function tempAlert(msg, duration) {
    var el = document.createElement("div");
    el.setAttribute("style", "position:absolute;top:40%;left:20%;background-color:white;");
    el.innerHTML = msg;
    setTimeout(function () {
        el.parentNode.removeChild(el);
    }, duration);
    document.body.appendChild(el);
}

function AddToCart(id){
    jQuery.ajax({
        type: 'POST',
        url: '/Cart/AddToCart/' + id,
        dataType: 'json',
        success: function (response) {
            alert(response);
        },
        error: function () {
            alert("Unable to perform this action");
        },

    });
}