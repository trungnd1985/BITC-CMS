/**
Custom module for you to write your own javascript functions
**/
var blog = function () {

    // private functions & variables

    var myFunc = function (text) {
        alert(text);
    }

    // public functions
    return {

        //main function
        init: function () {
            //initialize here something. 
            var autoCompleteUrl = $("#SelectedTags").attr("autocompleteurl");

            $("#SelectedTags").tagsInput({
                autocomplete_url: autoCompleteUrl,
                autocomplete: { selectFirst: true, width: '100%', autoFill: true },
                width: '100%'
            });

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