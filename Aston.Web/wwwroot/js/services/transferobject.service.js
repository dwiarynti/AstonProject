(function () {
	"use strict";
	app.factory('transferobjectService', function () {
        	var obj = {};
        	var addObj = obj;

        	var getObj = function () {
        		return addObj;
        	}

        	return {
        		addObj: addObj,
        		getObj: getObj()
        	};


        });

})();

