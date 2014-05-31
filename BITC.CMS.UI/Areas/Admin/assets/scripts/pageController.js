/**
Custom module for you to write your own javascript functions
**/
var page = function () {

    // private functions & variables

    var myFunc = function (text) {
        alert(text);
    }

    var getUrl = function (parentId, title) {
        $.ajax({
            type: 'POST',
            url: '/Admin/' + CULTURE + '/Page/GetUrl',
            data: JSON.stringify({ parentId: parentId, title: title }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (msg) {
                $("#Url").val(msg);
            }
        })
    };

    // public functions
    return {
        //main function
        init: function () {
            $("#ParentID").on("change", function (e) {
                var parentId = $(this).select2("val");
                var title = $("#PageTitle").val();

                getUrl(parentId, title);
            });

            $("#PageTitle").on("blur", function (e) {
                var parentId = $("#ParentID").select2("val");
                var title = $(this).val();

                getUrl(parentId, title);
            });

            //$("#Template").on("change", function (e) {
            //    var template = $(this).select2("val");
            //    $.ajax({
            //        type: 'POST',
            //        url: '/Admin/' + CULTURE + '/Page/GetTemplateContent',
            //        data: JSON.stringify({ _template: template }),
            //        contentType: 'application/json',
            //        dataType: 'json',
            //        success: function (msg) {
            //            //alert(msg);
            //            $("#Body").val(msg);
            //            //$("#Body").ckeditor().editor.insertHtml(msg);
            //        }
            //    })
            //});
        },

        getUrl: function (parentId, title) {
            getUrl(parentId, title);
        },
        //some helper function
        doSomeStuff: function () {
            myFunc();
        }

    };

}();

/***
Usage
***/
//Custom.init();
//Custom.doSomeStuff();