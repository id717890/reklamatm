//for Array
Array.prototype.toObject = function () {
    if (this.constructor === Array) {
        var arr = this;
        var rv = {};
        for (var i = 0; i < arr.length; ++i) {
            rv[i] = arr[i];
        }
        return rv;
    } else {
        throw new Error("Function is undefined");
    }
};

Array.prototype.mapJsonArrayToObjectArray = function (objectConstructor, argumentsArray) {
    if (!objectConstructor) {
        throw new Error("objectConstructor connot be found");
    }

    if (this && this.constructor === Array) {
        return $.map(this, function (item) {
            var params = [item];
            if (argumentsArray && argumentsArray.constructor === Array) {
                params = params.concat(argumentsArray);
            }
            return Utils.applyConstructor(objectConstructor, params);
        });
    }
    return [];
};

Array.prototype.showErrors = function showErrors() {
    var array = this;
    if (array.constructor === Array) {
        $.each(array, function () {
            if (this.errors && this.errors.showAllMessages) {
                this.errors.showAllMessages(true);
            }
        });
    }
};

Array.prototype.swapItems = function (a, b) {
    this[a] = this.splice(b, 1, this[a])[0];
    return this;
};

//String 
String.prototype.htmlEncode = function() {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(this).html();
};

String.prototype.htmlDecode = function() {
    return $('<div/>').html(this).text();
};

String.prototype.format = function() {
        var args = arguments;
        var result =  this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[Number(number)] != 'undefined' ? args[Number(number)] : match;
        });
    return result;
};

//Number
Number.prototype.format = function (count, point, separator) {
    ///<param name="count" >count after point</param>
    ///<param name="point">poin replacer</param>
    ///<param name="separator"> separator for thousand</param>
    var n = this;
    var c = isNaN(count = Math.abs(count)) ? 2 : count;
    var d = point === undefined ? "." : point;
    var t = separator === undefined ? "," : separator;
    var s = n < 0 ? "-" : "";
    n = Math.abs(+n || 0).toFixed(c);
    var i = parseInt(n) + "";
    c = Number(i) === Number(n) ? 0 : c;
    j = i.length;
    var j = (j) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

Number.prototype.round = function(percision) {
    var mn = 1;
    var perc = percision || 0;
    for (var i = 1; i <= perc; i++) {
        mn = mn * 10;
    }
    if (!isNaN(this)) {
        return Math.round(this * mn) / mn;
    }
    return this;
};

//window
window.numberOrNullOrZero = function(value) {
    if (isNaN(parseFloat(value))) {
        return null;
    }

    return parseFloat(value);
};

//Date

Date.prototype.addDays = function(num) {
    var value = this.valueOf();
    value += 86400000 * num;
    return new Date(value);
};

Date.prototype.addSeconds = function(num) {
    var value = this.valueOf();
    value += 1000 * num;
    return new Date(value);
};

Date.prototype.addMinutes = function(num) {
    var value = this.valueOf();
    value += 60000 * num;
    return new Date(value);
};

Date.prototype.addHours = function(num) {
    var value = this.valueOf();
    value += 3600000 * num;
    return new Date(value);
};

Date.prototype.addMonths = function(num) {
    var value = new Date(this.valueOf());
    var mo = this.getMonth();
    var yr = this.getYear();
    mo = (mo + num) % 12;
    if (0 > mo) {
        yr += (this.getMonth() + num - mo - 12) / 12;
        mo += 12;
    } else {
        yr += ((this.getMonth() + num - mo) / 12);
    }
    value.setMonth(mo);
    value.setYear(yr);
    return value;
};

Date.prototype.getDateWithFirstTime = function() {
    var value = new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0, 0);
    return value;
};