function FileSaveAsExcel(filename, fileContent) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(fileContent)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

 async function downloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);

    triggerFileDownload(fileName, url);

    //temporary object URL is revoked,This is an important step to ensure memory isn't leaked on the client.
    URL.revokeObjectURL(url);
}

 function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

function SignIn(url, name, password, redirect) {
   // return new Promise((resolve, reject) => {

            //var url = "/api/auth/signin";
            var xhr = new XMLHttpRequest();

            // Initialization
            xhr.open("POST", url);
            xhr.setRequestHeader("Accept", "application/json");
            xhr.setRequestHeader("Content-Type", "application/json");

            // Catch response
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) // 4=DONE 
                {
                    // console.log("Call '" + url + "'. Status " + xhr.status);
                    if (redirect && xhr.status == 200)
                        location.replace(redirect);

                }
                // console.log("Call '" + url + "'. Status " + xhr.status);
            };

            xhr.onload = function () {
                if (xhr.status == 200) {
                    resolve(xhr.response); //data gets here in correct format !!!!!!!
                }
                else if (xhr.status != 200) {
                    reject("Failed to submit form with status" + xhr.status);
                }
            }

            xhr.onerror = function () {
                reject('Error Occurred ' + xhr.responseText);

            }
                // Data to send
                var data = {
                    Name: name,
                    Password: password
                };

                // Call API
                xhr.send(JSON.stringify(data));
            
   // });

}

function Logout(url, redirect) {
    // return new Promise((resolve, reject) => {

    //var url = "/api/auth/signin";
    var xhr = new XMLHttpRequest();

    // Initialization
    xhr.open("POST", url);
    xhr.setRequestHeader("Accept", "application/json");
    xhr.setRequestHeader("Content-Type", "application/json");

    // Catch response
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) // 4=DONE 
        {
            // console.log("Call '" + url + "'. Status " + xhr.status);
            if (redirect && xhr.status == 200)
                location.replace(redirect);

        }
        // console.log("Call '" + url + "'. Status " + xhr.status);
    };

    xhr.onload = function () {
        if (xhr.status == 200) {
            resolve(xhr.response); //data gets here in correct format !!!!!!!
        }
        else if (xhr.status != 200) {
            reject("Failed to submit form with status" + xhr.status);
        }
    }

    xhr.onerror = function () {
        reject('Error Occurred ' + xhr.responseText);

    }
    // Data to send
    

    // Call API
    xhr.send();

    // });

}


function BlazorFocusElement(element) {
    if (element instanceof HTMLElement) {
        setTimeout(() => { element.focus(); }, 250);/*        element.focus();*/
    }
}


function PlayAudioFile(src) {
    var audio = document.getElementById('player');
    if (audio != null) {
        var audioSource = document.getElementById('playerSource');
        if (audioSource != null) {
            audioSource.src = src;
            audio.load();
            audio.play();
        }
    }
}

function focusInputFromBlazor(selector) {
    var input = document.getElementById(selector); /*querySelector(selector); for class*/
    if (!focusInput(input)) {
        input = input.getElementById("input");    /*default case*/
        focusInput(input);
    }
}

function focusInput(input) {
    if (input && input.focus) {
        setTimeout(() => { input.focus(); }, 250);
        /*input.focus();*/
        return true;
    }
    else {
        return false;
    }
}

function ScrollToRow(selector) {
    var input = document.getElementById(selector); /*querySelector(selector); for class*/
    if (input) {
        input.scrollIntoView(false);
    }
}

function ScrollToTop(selector) {
    var input = document.getElementById(selector); /*querySelector(selector); for class*/
    if (input) {
        input.scrollTo(0,0);
    }
}

function ScrollToRowWithHeader(selector) {
    var input = document.getElementById(selector); /*querySelector(selector); for class*/
    if (input) {
        input.scrollIntoView(true);
        const scrolledY = window.scrollY;

        if (scrolledY) {
            window.scroll(0, scrolledY - yourHeight);
        }
    }
}

window.getOrCreateClientIdStorage = function () {
    let clientId = localStorage.getItem('clientId');
    if (!clientId) {
        clientId = crypto.randomUUID();
        localStorage.setItem('clientId', clientId);
    }
    return clientId;
}

window.getOrCreateClientIdx = function () {
    let clientId = sessionStorage.getItem('clientId');
    if (!clientId) {
        clientId = crypto.randomUUID();
        sessionStorage.setItem('clientId', clientId);
    }
    return clientId;
}