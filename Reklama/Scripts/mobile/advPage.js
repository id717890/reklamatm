$(function() {
    /*
        slideMenu('#categories-menu');
        slideMenu('#regions-menu');*/
    if (!window.rk) {
        window.rk = {};
    }

    rk.helpers = {
        loading: {
            start: function() {
                $("#loading-dsctp").fadeIn(100);
            },
            finish: function() {
                $("#loading-dsctp").fadeOut(300);
            }
        }
    };

    function config() {
        $(document)
            .ajaxError(function(event, request) {
                switch (request.status) {
                case 403:
                    break;
                default:
                    if (console) {
                        console.log('Error:');
                        console.log(request);
                        console.log(request.fail());
                    }
                    break;
                }
            })
            .ajaxStart(function(event, request, settings) {
                rk.helpers.loading.start();
            })
            .ajaxComplete(function(event, request, settings) {
                rk.helpers.loading.finish();
            });

    }

    function initPageModel() {
        var m = this;
        var cssClass = {
            sectionBtn: 'section-btn-js',
            regionBtn: 'region-btn-js',
            active: 'active',
            announcementItm: 'announcement-itm',
            announcementsContainer: 'announcements-container'
        };
        var s = {
            currentSection: "#categories-menu .section-btn-js.active",
            currentRegion: "#regions-menu .region-btn-js.active",
            searchInput: "#search-inpt",
            announcmentItems: "div.announcement-itm",
            categoryWrap: "#categories-menu",
            regionWrap: "#regions-menu"
        };

        m.section = {
            get: function(isActual) {
                return $(s.currentSection).data("section") || null;
            },
            set: function(section) {
                $(s.currentSection).removeClass(cssClass.active);
                $("." + cssClass.sectionBtn + "[data-section=" + section + "]").addClass(cssClass.active);
            }
        };

        m.region = {
            get: function() {
                return $(s.currentRegion).data("region") || null;
            },
            set: function(region) {
                $(s.currentRegion).removeClass(cssClass.active);
                $("." + cssClass.regionBtn + "[data-region=" + region + "]").addClass(cssClass.active);
            }
        };

        m.searchStr = {
            get: function() {
                return $(s.searchInput).val() || '';
            },
            set: function(newSearchStr) {
                $(s.searchInput).val(newSearchStr);
            }
        };
        m.renderAddedAnn = function(annId) {
            $.ajax({
                url: rk.urls.GetAnn + '?id=' + annId,
                type: 'GET',
                success: function(data) {
                    if (data) {
                        var $html = getHtmlForSingleAnn(data);
                        $html.prependTo('div.' + cssClass.announcementsContainer);
                    }
                }
            });
        };

        function getHtmlForSingleAnn(annJS) {
            var rItm = annJS;
            var obj = {
                id: rItm.Id,
                description: rItm.Description,
                images: (function() {
                    var imgArr = [];
                    $.each(rItm.Images, function(i) {
                        var aImg = this;
                        var cssclass = "";
                        if (i < 3) {
                            cssclass = "col-xs-4 col-md-2 col-lg-2 th-itm";
                        } else if (i == 3) {
                            cssclass = "hidden-xs hidden-sm col-xs-4 col-md-2 col-lg-2 th-itm ";
                        } else if (i <= 5) {
                            cssclass = "hidden-xs hidden-sm col-md-2 col-lg-2 th-itm";
                        } else {
                            cssclass = "hidden-xs hidden-sm hidden-md hidden-lg col-md-2 col-lg-2 th-itm ";
                        }
                        imgArr.push({
                            cssClass: cssclass,
                            link: aImg.Link
                        });
                    });
                    return imgArr;
                })(),
                price: rItm.Price,
                currency: { name: rItm.Currency.Name },
                phoneNumber: rItm.PhoneNumber,
                publishDate: $.timeago(new Date(rItm.PublishDate)),
                viewsCount: rItm.ViewsCount
            };
            return $('#ann-item-tmpl').tmpl(obj);
        };

        function renderAnns(respArr) {
            //var resArr = [];
            if (respArr.constructor === Array && respArr.length > 0) {
                $.each(respArr, function() {
                    var rItm = this;
                    var $html = getHtmlForSingleAnn(rItm);
                    // respArr.push(obj);
                    $html.appendTo('div.' + cssClass.announcementsContainer);


                });

            }
        }

        function getSkipCount() {
            var domAnns = $(s.announcmentItems);
            if (domAnns && domAnns.length > 0) {
                return domAnns.length;
            } else {
                return null;
            }
        };

        function createFilterModel(isNewSearch) {
            return {
                searchStr: m.searchStr.get(),
                region: m.region.get() || 0,
                sectionId: m.section.get() || 0,
                isNewSearch: isNewSearch,
                skipCount: !isNewSearch ? getSkipCount() : null
            };
        }

        function initHandlers() {

            m.searchChangeTimeOut = null;
            $(s.searchInput).bind("keyup change paste", function() {
                changeSearchString();
            });

            $(s.categoryWrap).bind('mousedown', function(e) {
                if (e && e.target && $(e.target).hasClass(cssClass.sectionBtn)) {
                    $(this).data("initcoords", { x: event.x, y: event.y });
                }
            });

            $(s.categoryWrap).bind('click', function(e) {
                if (e && e.target && $(e.target).hasClass(cssClass.sectionBtn)) {
                    var initCoords = $(this).data("initcoords") || { x: 0, y: 0 };
                    if (event.x === initCoords.x && event.y === initCoords.y) {
                        var old = m.section.get();
                        var newS = $(e.target).data('section');
                        m.section.set(newS);
                        changeSection(old);
                    }
                }
            });

            $(s.regionWrap).bind('mousedown', function(e) {
                if (e && e.target && $(e.target).hasClass(cssClass.regionBtn)) {
                    $(this).data("initcoords", { x: event.x, y: event.y });
                }
            });

            $(s.regionWrap).bind('click', function(e) {
                if (e && e.target && $(e.target).hasClass(cssClass.regionBtn)) {
                    var initCoords = $(this).data("initcoords") || { x: 0, y: 0 };
                    if (event.x === initCoords.x && event.y === initCoords.y) {
                        var old = m.region.get();
                        var newR = $(e.target).data('region');
                        m.region.set(newR);
                        changeRegion(old);
                    }
                }
            });

            $(window).scroll(function() {
                if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
                    getLatestItems(false, addOldestAnns);
                }
            });
        }

        function rewriteAllAnns(newAnnsArr, callBack) {
            if (newAnnsArr != null) {
                $('.' + cssClass.announcementItm, '.' + cssClass.announcementsContainer).remove();
                renderAnns(newAnnsArr);
            }
            if (callBack != null && callBack.constructor === Function) {
                callBack();
            }
        };

        function addOldestAnns(oldestAnnsArr, callBack) {
            if (oldestAnnsArr != null) {
                renderAnns(oldestAnnsArr);
            }
            if (callBack != null && callBack.constructor === Function) {
                callBack();
            }
        };

        function changeSearchString() {
            getLatestItems(true, rewriteAllAnns);
        };

        function changeRegion(oldRegion) {
            if (oldRegion !== m.region.get()) {
                getLatestItems(true, rewriteAllAnns);
            }
        };

        function changeSection(oldSection) {
            if (oldSection !== m.section.get()) {
                getLatestItems(true, rewriteAllAnns);
            }
        };

        //ajax
        function getLatestItems(isNewSearch, callBack) {
            if (m.searchChangeTimeOut) {
                clearTimeout(m.searchChangeTimeOut);
            }
            m.searchChangeTimeOut = setTimeout(
                function() {
                    var requestData = createFilterModel(isNewSearch);

                    $.ajax({
                        url: rk.urls.GetLatestItems,
                        type: 'post',
                        cash: false,
                        success: function(resp) {

                            if (callBack && callBack.constructor === Function) {
                                callBack(resp);
                            }

                        },
                        data: requestData
                    });
                }, 600);

        }


        //all init
        renderAnns(rk.announcementList);
        initHandlers();
    };

    ////config();
   //// var page = new initPageModel();
    var regionCarusel = $("#regions-menu").touchCarousel({
        pagingNav: false,
        snapToItems: false,
        itemsPerMove: 1,
        scrollToLast: false,
        loopItems: false,
        scrollbar: false
    }).data('touchCarousel');

    var items = $(".touchcarousel-item", "#regions-menu");
    if (items && items.length) {
        $.each(items, function(index) {
            if ($(this).find('button.active').length == 1) {
                regionCarusel.goTo(index);
                return false;
            }
        });
    }

    var sectionCarusel = $("#categories-menu").touchCarousel({
        pagingNav: false,
        snapToItems: false,
        itemsPerMove: 1,
        scrollToLast: false,
        loopItems: false,
        scrollbar: false
    }).data('touchCarousel');

    items = $(".touchcarousel-item", "#categories-menu");
    if (items && items.length) {
        $.each(items, function(index) {
            if ($(this).find('button.active').length == 1) {
                sectionCarusel.goTo(index);
                return false;
            }
        });
    }


///add new ann 
    function initAddNew() {
        var self = this;
        var currencySelector = "#add-new-currency";
        self.files = [];
        var fileList = $(".pi_medias");
        var thambHtml = $("#add-ann-thumb-tmpl").html();

        $("#adv-add-img-btn").change(function () {
            handleFiles(this.files);
        });

        $("#new-adv-publich").click(function () {
            sendModel();
        });

        $("textarea").unbind('keyup');
        $("textarea").bind('keyup paste change', function (e) {
            if (parseInt(this.offsetHeight) < 200) {
                this.style.height = "1px";
                this.style.height = (15 + this.scrollHeight) + "px";
            }
        });
        $("textarea").bind('blur', function() {
            validateDescription();
        });
        
        $("#add-new-phone-number").bind('blur', function () {
            validatePhone();
        });

        $("#add-new-price").bind('blur', function () {
            validatePrice();
        });

        $(fileList).on('click', '.glyphicon.glyphicon-remove', function () {
            $(this).closest('.img-col').remove();
            $("#adv-add-img-btn").val(null);
        });
        
        function validateDescription() {
            var valid = false;
            var descriptionArea = $('#add-new-description:first');
            descriptionArea.closest('.form-group').removeClass('has-success');
            descriptionArea.closest('.form-group').removeClass('has-error');
            if (!descriptionArea.val()) {
                descriptionArea.closest('.form-group').addClass('has-error');
            }
            else if (!descriptionArea.val().length > 450) {
                descriptionArea.closest('.form-group').addClass('has-error');
            } else {
                descriptionArea.closest('.form-group').addClass('has-success');
                valid = true;
            }
            return valid;
        };

        function validatePhone() {
            var valid = false;
            var phoneInput = $('#add-new-phone-number');
            phoneInput.closest('.form-group').parent('div').removeClass('has-success');
            phoneInput.closest('.form-group').parent('div').removeClass('has-error');
            if (!phoneInput.val()) {
                phoneInput.closest('.form-group').parent('div').addClass('has-error');
            }
            else if (phoneInput.val().indexOf('X')!==-1) {
                phoneInput.closest('.form-group').parent('div').addClass('has-error');
            }
            else {
                phoneInput.closest('.form-group').parent('div').addClass('has-success');
                valid = true;
            }
            return valid;
        };
        function validatePrice() {
            var valid = false;
            var priceInput = $('#add-new-price');
            priceInput.closest('.form-group').parent('div').removeClass('has-success');
            priceInput.closest('.form-group').parent('div').removeClass('has-error');
            if (!!priceInput.val() && isNaN(Number(priceInput.val()))) {
                priceInput.closest('.form-group').parent('div').addClass('has-error');
            }
            else {
                priceInput.closest('.form-group').parent('div').addClass('has-success');
                valid = true;
            }
            return valid;
        };

        function validate() {
             return validateDescription() && validatePhone();
        };

        function initControls() {
            $(currencySelector).selectpicker();
            $("#add-new-phone-number").mask("+999 (99) 999-99-99", { placeholder: "+XXX (XX) XXX-XX-XX" });
        };


        function sendModel() {
            var formData = new FormData();
            var currency = $('#add-new-currency').val();
            var description = $('#add-new-description').val();
            var phone = $('#add-new-phone-number').val();
            var price = $('#add-new-price').val();
            var images = (function() {
                var files = [];
                $('.pi_medias .thumbnail img').each(function() {
                    files.push(this.file);
                });
                return files;
            }());

            $.each(self.files, function(indx) {
                formData.append('modelAnnouncementEdit.Images[' + indx + ']', this);
            });


            formData.append('modelAnnouncementEdit.description', description);
            formData.append('modelAnnouncementEdit.currency', currency);
            formData.append('modelAnnouncementEdit.phoneNumber', phone);
            formData.append('modelAnnouncementEdit.price', price);
            formData.append('modelAnnouncementEdit.currencyId', currency);

            formData.append('modelAnnouncementEdit.sectionId', page.section.get());
            formData.append('modelAnnouncementEdit.region', page.region.get());


            $.ajax({
                url: rk.urls.AddNewAnn,
                data: formData,
                processData: false,
                contentType: false,
                type: 'POST',
                success: function(data) {
                    if (data && data.AnnouncementId) {
                        page.renderAddedAnn(data.AnnouncementId);
                    }
                }
            });
        }

        var vMessages = {
            maxNumberOfFiles: 'Максимальное каличество картинок 10.',
            acceptFileTypes: 'Тип файла {0} не поддерживается.',
            maxFileSize: 'Слишком большой размер файла {0}.',
            minFileSize: 'Слишком маленький размер файла {0}.'
        };

        function validateFile(file) {
            
            var v = {
                acceptFileTypes: /(\.|\/)(gif|jpe?g|png|bmp)$/i,
                minFileSize: 1, // 1 B,
                maxFileSize: 6000000 // 5 MB
            };
            var message = null;

            if (v.acceptFileTypes && !(v.acceptFileTypes.test(file.type) || v.acceptFileTypes.test(file.name))) {
                message = vMessages.acceptFileTypes.format(file.name);
            } else if (file.size > v.maxFileSize) {
                message = vMessages.maxFileSize.format(file.name);
            } else if (file.size < v.minFileSize) {
                message = (vMessages.minFileSize.format(file.name));
            }
            return {
                isValid: !message,
                error: message
            }
        };

        function handleFiles(files) {
            var errorsHtml = '';
            if (!files.length) {
                fileList.innerHTML = "<p>No files selected!</p>";
            } else {
                var filesCount = files.length;
                 if (files.length > 10) {
                     filesCount = 10;
                     errorsHtml +='<p>'+ vMessages.maxNumberOfFiles +'</p>';
                 }

                 for (var i = 0; i < filesCount; i++) {
                     var validationResult = validateFile(files[i]);
                     if (validationResult.isValid) {
                         var img = document.createElement("img");
                         img.src = window.URL.createObjectURL(files[i]);
                         img.className = 'img-responsive center-block';
                         self.files.push(files[i]);
                         img.onload = function() {
                         };
                         var thumb = $(thambHtml);
                         thumb.find("span.image").append(img);
                         fileList.append(thumb);
                    } else {
                        errorsHtml += '<p>' + validationResult.error + '</p>';
                    }
                 }
                 if (errorsHtml.length > 0) {
                     $('#images-errors .erros-wrap').html(errorsHtml);
                     $('#images-errors').show();
                     setTimeout(function() {
                         $('#images-errors').fadeOut('1000' ,function() {
                             $('#images-errors .erros-wrap').html('');
                         });
                         
                     }, 5000);
                 }
            }
        }

        initControls();
    };

    initAddNew();

});