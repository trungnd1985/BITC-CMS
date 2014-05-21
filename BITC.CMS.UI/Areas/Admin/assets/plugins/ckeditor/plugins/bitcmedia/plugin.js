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
                    dialogContent += "<div id='content-container' class='col-md-12'>";
                    dialogContent += "<div class='col-md-12'>";
                    dialogContent += "<div class='col-md-12'>";
                    dialogContent += "<input name='MediaUploadFile' id='MediaUploadFile' type='file' />";
                    dialogContent += "</div>";
                    dialogContent += "<div class='col-md-12'>";
                    dialogContent += "<div class='tab-pane' id='bitc-media-library'>";
                    dialogContent += "<div class='col-md-8' id='bitc-media-container'></div>";
                    dialogContent += "<div class='col-md-4' id='bitc-media-info'></div>";
                    dialogContent += "<div class='clearfix'></div>";
                    dialogContent += "</div>";
                    dialogContent += "</div>";
                    dialogContent += "</div>";
                    dialogContent += "</div>";
                    dialogContent += "<div class='modal-footer'>";
                    dialogContent += "<button type='button' class='btn blue' id='btnInsertMedia'>Insert</button>";
                    dialogContent += "<button type='button' class='btn default' data-dismiss='modal'>Close</button>";
                    dialogContent += "</div></div></div></div>";

                    $("body").append(dialogContent);

                    //media.init(CULTURE);
                }

                $("#bitc-media-dialog").modal("show");



                $("#MediaUploadFile").kendoUpload({
                    async: {
                        saveUrl: "/Admin/" + CULTURE + "/Media/Upload",
                        autoUpload: true
                    },
                    success: function (e) {
                        $("#tabMediaView a:last").tab("show");
                    }
                });

                $(document).on("click", ".media-image", function (e) {
                    if (e.shiftKey) {
                        if ($(this).is(".selected")) {
                            $(this).removeClass("selected");
                        } else {
                            $(this).addClass("selected");
                        }
                    } else {
                        $(".media-image").removeClass("selected");
                        $(this).addClass("selected");
                    }

                    $("#bitc-media-info").empty("");

                    var infoBuilder = "";
                    infoBuilder += "<div class='col-md-12'><label>Url</label><span class='info'>" + $(this).attr("data-url") + "</span></div>";
                    infoBuilder += "<div class='col-md-12'><label class='label'>File size</label><span class='info'>" + $(this).attr("data-file-size") + "</span></div>";
                    //infoBuilder += "<div class='col-md-12'><label>Url</label><span class='info'>" + $(this).attr("data-url") + "</span></div>";
                    //infoBuilder += "<div class='col-md-12'><label>Url</label><span class='info'>" + $(this).attr("data-url") + "</span></div>";

                    $("#bitc-media-info").append(infoBuilder);
                });

                $(document).on("hidden.bs.modal", "#bitc-media-dialog", function (e) {
                    $(".media-image.selected").removeClass("selected");
                });

                $(document).on("shown.bs.modal", "#bitc-media-dialog", function (e) {
                    $.ajax({
                        url: '/Admin/' + CULTURE + '/Media/GetMediaFiles',
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json',
                        success: function (msg) {
                            $("#bitc-media-container").empty();
                            var imgBuilder = "";
                            $.each(msg, function (index, item) {
                                imgBuilder = "<img class='img-responsive img-thumbnail media-image'";
                                imgBuilder += " src='/Areas/Admin/assets/img/ajax-loader.gif'";
                                imgBuilder += " alt='" + item.AlternativeText !== null ? item.AlternativeText : "" + "'";
                                imgBuilder += " data-caption='" + item.Caption !== null ? item.Caption : "" + "'";
                                imgBuilder += " data-description='" + item.Description !== null ? item.Description : "" + "'";
                                imgBuilder += " data-file-size='" + item.FileSize + "'";
                                imgBuilder += " data-url='" + item.Url + "'";
                                imgBuilder += "data-src='" + item.Url + "?height=100&width=100&mode=crop' />";

                                $("#bitc-media-container").append(imgBuilder);
                            });

                            $('#bitc-media-container').loadImages({
                                imgLoadedClb: function () { }, // Triggered when an image is loaded. ('this' is the loaded image)
                                allLoadedClb: function () { }, // Triggered when all images are loaded. ('this' is the wrapper in which all images are loaded, or the image if you ran it on one image)
                                imgErrorClb: function () { }, // Triggered when the image gives an error. Useful when you want to add a placeholder instead or remove it. ('this' is the loaded image)
                                noImgClb: function () { }, // Triggered when there are no image found with data-src attributes, or when all images give an error. ('this' is the wrapper in which all images are loaded, or the image if you ran it on one image)
                                dataAttr: 'src' // The data attribute that contains the source. Default 'src'.
                            });
                        }
                    });
                });

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