app.factory('commonService',
[
    '$timeout',
    function($timeout) {
        var convertdate = function (stringdate) {
            var a = Date.parse(stringdate.replace(/^(\d\d)(\d\d)(\d\d\d\d)$/, "$2-$1-$3"));
            var myDate = new Date(parseInt(a));
            var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
            var day = ("0" + myDate.getDate()).slice(-2);
            var year = myDate.getFullYear();
            var date = day + "/" + month + "/" + year;
            return date;
        }

        var validationform = function (model, data) {
            var validationstatus = true;
            var keys = Object.keys(model);
            for (var i = 0; i < keys.length; i++) {
                var key = keys[i];
                var value = data[key];
                var datatype = typeof value;
                if (datatype != "boolean" && validationstatus) {
                    if (value == null || value == "") {
                        validationstatus = false;
                        break;
                    }
                } else if (!validationstatus) {
                    validationstatus = false;
                    break;
                }

            }
            return validationstatus;
        }


        return {
            convertdate: convertdate,
            validationform: validationform
        }
    }
]);
