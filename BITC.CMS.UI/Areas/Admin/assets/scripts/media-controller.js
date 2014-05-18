/**
Custom module for you to write your own javascript functions
**/
var media = function () {

    // private functions & variables

    var myFunc = function (text) {
        alert(text);
    }

    // public functions
    return {
        //main function
        init: function (culture) {
            //initialize here something.         
            $("#content-container").load("/Admin/" + culture + "/Media/MediaView");

            $(document).on('shown.bs.tab', 'a[data-toggle="tab"]', function (e) {
                if ($(e.target).attr("href") === "#media-library") {
                    $.ajax({
                        url: '/Admin/' + culture + '/Media/GetMediaFiles',
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json',
                        success: function (msg) {
                            $("#media-container").empty();

                            $.each(msg, function (index, item) {
                                $("#media-container").append("<img class='img-responsive img-thumbnail media-image' src='/Areas/Admin/assets/img/ajax-loader.gif' data-src='" + item.Url + "?height=100&width=100&mode=crop' />");
                            });

                            $('#media-container').loadImages({
                                imgLoadedClb: function () { }, // Triggered when an image is loaded. ('this' is the loaded image)
                                allLoadedClb: function () { }, // Triggered when all images are loaded. ('this' is the wrapper in which all images are loaded, or the image if you ran it on one image)
                                imgErrorClb: function () { }, // Triggered when the image gives an error. Useful when you want to add a placeholder instead or remove it. ('this' is the loaded image)
                                noImgClb: function () { }, // Triggered when there are no image found with data-src attributes, or when all images give an error. ('this' is the wrapper in which all images are loaded, or the image if you ran it on one image)
                                dataAttr: 'src' // The data attribute that contains the source. Default 'src'.
                            });
                        }
                    });
                }
            });

            $(document).on("click", ".media-image", function (e) {
                if ($(this).is(".selected")) {
                    $(this).removeClass("selected");
                } else {
                    $(this).addClass("selected");
                }

            });
        },
        uploadSuccess: function (e) {
            $("#tabMediaView a:last").tab("show");
        },
        insertSelected: function (target) {
            var arrSelected = $(".selected");

            $.each(arrSelected, function (index, item) {
                if ($(target).ckeditor() !== undefined && $(target).ckeditor().editor) {
                    var editor = $(target).ckeditor().editor;
                    var selectionText = editor.getSelection().getSelectedText();

                    editor.insertHtml("<img src='" + $(item).attr("src") + "' title='" + $(item).attr("title") + "' />")
                }
            });
        }
    };
}();

/***
Usage
***/
//Custom.init();
//Custom.doSomeStuff();