// Write your JavaScript code.
$(".cardClick").on("click", function () {
    UIkit.lightbox(this).show(1);
});

$("#checkbox").on("click", function () {
    if ($(this).is(':checked')) {
        $("#hiddenCheckbox").val(true);
    }
    else {
        $("#hiddenCheckbox").val(false);
    }
});

var editor = CKEDITOR.replace('editor1');

CKEDITOR.stylesSet.add( 'default', [
    {
        name: 'Price name',
        element: 'div',
        attributes: {
            class: 'uk-width-expand',
            'uk-leader': ''
        }
    },
    {
        name: 'Price value',
        element: 'div'
    }
] );

// CKEDITOR.stylesSet.add( 'pp1', [
//     {
//         name: 'Price value',
//         element: 'div'
//     }
// ] );

editor.on('change', function (evt) {
    var dataText = evt.editor.getData();
    $.ajax({
        type: "POST",
        url: "/AdminArt/HtmlEdit",
        data: {
            data: dataText,
            target: $('textarea[name=editor1]').attr('id')
        },
        success: function (data) {
            console.log(data);
        }
    });
});