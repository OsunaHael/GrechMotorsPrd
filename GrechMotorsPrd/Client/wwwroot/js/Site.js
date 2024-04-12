window.downloadFile = function (fileName) {
    fetch(`/api/DownloadFile/download?fileName=${encodeURIComponent(fileName)}`)
        .then(response => response.blob())
        .then(blob => {
            var url = window.URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = fileName;
            a.click();
        });
}