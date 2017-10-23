(function () {
    "use strict";
   app.factory("assetResource",
                ["$resource",
                 assetResource]);
    function assetResource($resource) {
        return $resource("/api/Asset/:action/:id",
        { id: '@id' },
        {
            GetAsset: { method: 'GET', params: { action: 'GetAsset' } },
            CreateAsset: { method: 'POST', params: { action: 'CreateAsset' } },
            UpdateAsset: { method: 'POST', params: { action: 'UpdateAsset' } },
            DeleteAsset: { method: 'POST', params: { action: 'DeleteAsset' } },
            GetAssetByCategoryCode: { method: 'GET', params: { action: 'GetAssetByCategoryCode' } },
            SearchAsset: { method: 'POST', params: { action: 'SearchAsset' } },
            AssetMovementHistory: { method: 'POST', params: { action: 'AssetMovementHistory' } },
            download: {
                method: 'POST', params: { action: 'download' },
                headers: {
                    accept: 'application/octet-stream' //or whatever you need
                },
                responseType: 'arraybuffer',
                cache: false,
                //transformRequest: function (data, headersGetter) {
                //    //modify data and return it 
                //    return angular.toJson(data);
                //}, 
                transformResponse: function (data, headers) {

                    function getFileNameFromHeader(header) {
                        if (!header) return null;

                        var result = header.split(";")[1].trim().split("=")[1];

                        return result.replace(/"/g, '');
                    }
                    var result = null;
                    var file = null;
                    //console.log("transformRequest",data);
                    //server should sent content-disposition header 
                    var fileName = getFileNameFromHeader(headers('content-disposition'));
                    //console.log(fileName);
                    if (fileName != 'null' && fileName != null) {
                        file = new Blob([data], {
                            type: 'application/octet-stream' //or whatever you need, should match the 'accept headers' above
                        });

                        /*
                        var urlCreator = window.URL || window.webkitURL || window.mozURL || window.msURL;
                        var blob = file; // new Blob([data], { type: octetStreamMime });
                        var url = urlCreator.createObjectURL(blob);
                        window.location = url;
                        */
                        /*
                        var blob = file; // new Blob([data], { type: contentType });
                        if (navigator.msSaveBlob)
                            navigator.msSaveBlob(blob, filename);
                        else {
                            // Try using other saveBlob implementations, if available
                            var saveBlob = navigator.webkitSaveBlob || navigator.mozSaveBlob || navigator.saveBlob;
                            if (saveBlob === undefined) throw "Not supported";
                            saveBlob(blob, filename);
                        }
                        console.log("saveBlob succeeded");
                        */


                        result = {
                            blob: file,
                            fileName: fileName
                        };
                    } else {
                        result = JSON.parse(data);
                    }
                    return {
                        response: result
                    };
                }

            }
        });
    }
}());