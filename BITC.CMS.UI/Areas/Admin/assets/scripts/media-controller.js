/**
Custom module for you to write your own javascript functions
**/
var media = function () {

    // private functions & variables

    var myFunc = function (text) {
        alert(text);
    }
    var insertEventFunc;

    // public functions
    return {
        //main function
        init: function (culture, target, insertCompleteFunc) {
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
                                $("#media-container").append("<img class='img-responsive img-thumbnail media-image lazy' src='" + item.Url + "?height=100&width=100&mode=crop' data-src='" + item.Url + "' />");
                            });
                        }
                    });
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
            });

            $(document).on('click', '#btnInsertMedia', function (e) {
                media.insertSelected(target);
                insertEventFunc();
                media.close();
            });
        },
        close: function () {
            $("#media-popup").modal("hide");
        },
        insert: function (func) { insertEventFunc = func; },
        uploadSuccess: function (e) {
            $("#tabMediaView a:last").tab("show");
        },
        insertSelected: function (target) {
            var arrSelected = $(".selected");

            $.each(arrSelected, function (index, item) {
                $(target).append("<div class='project-image'><img class='img-responsive lazy' src='" + $(item).attr("data-src") + "?width=200&mode=crop' title='" + $(item).attr("title") + "' /></div>");
            });
        },
        getSelectedItems: function () {
            return $(".selected");
        }
    };
}();

/***
Usage
***/
//Custom.init();
//Custom.doSomeStuff();