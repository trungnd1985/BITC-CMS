/// <reference path="../../ckeditor.js" />
CKEDITOR.plugins.add('bitcmedia', {
    icons: 'bitcmedia',
    init: function (editor) {
        editor.addCommand('insertBitcMedia', {
            exec: function (editor) {
                if ($("#bitc-media-dialog").length === 0) {
                    var dialogContent = "<div class='modal fade' id='bitc-media-dialog' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>";
                    dialogContent += "<div class='modal-dialog modal-lg'>";
                    dialogContent += "<div class='modal-content'>";
                    dialogContent += "<div class='modal-header'>";
                    dialogContent += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'></button>";
                    dialogContent += "<h4 class='modal-title'>Modal title</h4>";
                    dialogContent += "</div>";
                    dialogContent += "<div class='modal-body'>";
                    dialogContent += "<div class='col-md-2'>";
                    dialogContent += "<div class='list-group'>";
                    dialogContent += "<a class='list-group-item active' id='lnkInsertMedia'>Insert media</a>";
                    //dialogContent += "<a class='list-group-item' id='lnkInsertYoutube'>Insert youtube</a>";
                    //dialogContent += "<a class='list-group-item' id='lnkInsertUrl'>Insert url</a>";
                    dialogContent += "</div>";
                    dialogContent += "</div>";
                    dialogContent += "<div id='content-container' class='col-md-10'>";
                    dialogContent += "</div>";
                    dialogContent += "<div class='clearfix'></div>";
                    dialogContent += "</div>";
                    dialogContent += "<div class='modal-footer'>";
                    dialogContent += "<button type='button' class='btn blue' id='btnInsertMedia'>Insert</button>";
                    dialogContent += "<button type='button' class='btn default' data-dismiss='modal'>Close</button>";
                    dialogContent += "</div></div></div></div>";

                    $("body").append(dialogContent);

                    media.init(CULTURE);
                }

                $("#bitc-media-dialog").modal("show");

                $(document).on("click", "#btnInsertMedia", function (e) {
                    var arrSelected = $(".media-image.selected");

                    $.each(arrSelected, function (index, item) {
                        $(item).removeClass("selected");
                        editor.insertHtml("<img src='" + $(item).attr("src") + "' title='" + $(item).attr("title") + "' />")
                    });

                    $("#bitc-media-dialog").modal("hide");
                });
            }
        });
        editor.ui.addButton('BitcMedia', {
            label: 'Insert BITC Media',
            command: 'insertBitcMedia',
            toolbar: 'insert'
        });
    }
});