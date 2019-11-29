angular.module('app', ['angularFileUpload'])

    // The example of the full functionality
    .controller('TestController', function ($scope, $fileUploader) {
        'use strict';


        var xsrf = $.param({ cAction: currentAction }, { cController: currentController });
        //var sendingData = {
        //    'cAction' 				: currentAction,
        //    'cController' 	: currentController
        //};

        // create a uploader with options
        var uploader = $scope.uploader = $fileUploader.create({
            scope: $scope,                          // to automatically update the html. Default: $rootScope
            autoUpload: true,
            formData: [{'cAction': currentAction }, { 'cController': currentController }],
            url: '/AjaxServices/angularUploadFile'
        });

        // ADDING FILTERS

        uploader.filters.push(function(item /*{File|HTMLInput}*/) { // user filter
            if (item.type.indexOf("image") !== -1) {
                return true;
            } else {
                return false;
            }
        });

        // REGISTER HANDLERS

        uploader.bind('afteraddingfile', function (event, item) {
            console.info('After adding a file', item);
        });

        uploader.bind('whenaddingfilefailed', function (event, item) {
            console.info('When adding a file failed', item);
        });

        uploader.bind('afteraddingall', function (event, items) {
            console.info('After adding all files', items);
        });

        uploader.bind('beforeupload', function (event, item) {
            console.info('Before upload', item);
        });

        uploader.bind('progress', function (event, item, progress) {
            console.info('Progress: ' + progress, item);
        });

        uploader.bind('success', function (event, xhr, item, response) {
            $('#imageContainer').append('<input type="hidden" name="imagesNames[]" class="uploadedImage" data-filename="' + response.filename + '" value="' + response.newFilename + '" />');
            $("#mycontainer").append(`
                <div class="col-lg-4 col-md-4 col-6 thumb img-block-gallery" data-filename="`+ item.file.name +`">
                    <a href="#" class="button remove remove-times" title="" data-filename="` + item.file.name + `" onclick="javascript: removeImage('` + item.file.name + `'); return false;"ng-click="item.remove()">
                        <i class="fa fa-times fa-2x text-danger"></i>
                    </a>
                    <a data-fancybox="gallery" style="float: right;" href="/Images/Realty/`+ response.newFilename + `">
                        <img class="img-fluid" src="/Images/Thumbnails/Realty/`+ response.newFilename + `">
                    </a>
                </div>`);
        });

        uploader.bind('cancel', function (event, xhr, item) {
            console.info('Cancel', xhr, item);
        });

        uploader.bind('error', function (event, xhr, item, response) {
            console.info('Error', xhr, item, response);
        });

        uploader.bind('complete', function (event, xhr, item, response) {
            //var selector = "section[data-loadingfilename='" + response.filename + "']";
            //var obj = $(selector).hide();
        });

        uploader.bind('progressall', function (event, progress) {
            console.info('Total progress: ' + progress);
        });

        uploader.bind('completeall', function (event, items) {
            console.info('Complete all', items);
        });


        // -------------------------------


        var controller = $scope.controller = {
            isImage: function(item) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        };

    });