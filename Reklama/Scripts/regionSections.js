$(function () {
    $("#region-top-selector input[type='radio'][name='region']").click(function () {
        var selected = $("input[type='radio'][name='region']:checked");
        if (selected.length > 0) {
            window.document.location.href = "http://" + selected.val();
        }
    });
})