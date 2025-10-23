﻿/**
 * Shows loader when AJAX request is in progress
 */
$(function () {
    const loaderBody = $("#loaderbody").addClass("hide");

    $(document).on("ajaxStart", function () {
        loaderBody.removeClass("hide");
    }).on("ajaxStop", function () {
        loaderBody.addClass("hide");
    });
});

/** SweetAlert2 */

const toastTimer = 3000;

const Swal2 = Swal.mixin({
    customClass: {
        input: 'form-control'
    }
});

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: toastTimer,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer);
        toast.addEventListener('mouseleave', Swal.resumeTimer);
    }
});


/**
 * Shows content in a popup modal window
 * @param {string} url - The URL of the content to show
 * @param {string} title - The title of the modal window
 */
function showInPopup(url, title) {
    $.get(url, function (res) {
        const modalBody = $("#form-modal .modal-body");
        const modalTitle = $("#form-modal .modal-title");

        modalBody.html(res);
        modalTitle.html(title);
        $("#form-modal").modal("show");


    });
}

/**
 * Performs an AJAX request for the specified form
 * @param {string} type - The HTTP method to use (e.g. POST)
 * @param {HTMLFormElement} form - The form element to send data from
 * @param {function} successCallback - The callback function to execute on successful AJAX response
 */
function performAjaxRequest(type, form, successCallback) {
    try {
        $.ajax({
            type: type,
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: successCallback,
            error: function (err) {
                console.log(err);
            }
        });
    } catch (ex) {
        console.log(ex);
    }
}

/**
 * Sends a jQuery AJAX POST request for the specified form
 * @param {HTMLFormElement} form - The form element to send data from
 * @returns {boolean} - Returns false to prevent page reload
 */

function jQueryAjaxPost(form) {
    performAjaxRequest("POST", form, function (res) {
        if (res.isValid) {
            //$("#view-all").html(res.html);
            $("#form-modal .modal-body").html(res.html).delay(1000);
            $("#form-modal .modal-body, #form-modal .modal-title").html("");
            $("#form-modal").modal("hide");
            if (res.redirectUrl) {
                setTimeout(function () {
                    window.location.href = res.redirectUrl; // Perform the delayed redirect
                }, toastTimer);
            }
        } else {
            $("#form-modal .modal-body").html(res.html);
        }
    });

    return false;
}

/**
 * Sends a jQuery AJAX DELETE request for the specified form after user confirmation
 * @param {HTMLFormElement} form - The form element to send data from
 * @returns {boolean} - Returns false to prevent page reload
 */
function jQueryAjaxDelete(form) {
    if (confirm("Are you sure to delete this record ?")) {
        performAjaxRequest("POST", form, function (res) {
            $("#view-all").html(res.html);
        });
    }

    return false;
}

/**
 * Validates a string of coordinates with the format of "-xx.xxxx, -xx.xxxx"
 * @param {string} coordString - The string containing coordinates to validate
 * @returns {boolean} - Returns true if the string contains valid coordinates, false otherwise.
 */
function validateCoordinates(coordString) {
    const regex = /^[-]?[0-9]+(\.[0-9]+)?,\s*[-]?[0-9]+(\.[0-9]+)?$/;
    return regex.test(coordString);
}
// Chat Bubble Logic
document.addEventListener('DOMContentLoaded', () => {
    const chatBubble = document.querySelector('.chat-bubble');
    const chatWindow = document.querySelector('.chat-window');
    const chatClose = document.querySelector('.chat-close');
    const chatSend = document.getElementById('chat-send');
    const chatInput = document.getElementById('chat-input');
    const chatBody = document.querySelector('.chat-body');

    chatBubble.addEventListener('click', () => {
        chatWindow.style.display = 'flex';
    });

    chatClose.addEventListener('click', () => {
        chatWindow.style.display = 'none';
    });

    chatSend.addEventListener('click', sendMessage);
    chatInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {
            sendMessage();
        }
    });

    function sendMessage() {
        const message = chatInput.value;
        if (!message) return;

        appendMessage(message, 'user-message');
        chatInput.value = '';

        fetch('/api/chat', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ message: message }),
        })
        .then(response => response.json())
        .then(data => {
            appendMessage(data.response, 'bot-message');
        })
        .catch(error => {
            console.error('Error:', error);
            appendMessage('Sorry, something went wrong.', 'bot-message');
        });
    }

    function appendMessage(message, className) {
        const messageElement = document.createElement('div');
        messageElement.className = `chat-message ${className}`;
        messageElement.textContent = message;
        chatBody.appendChild(messageElement);
        chatBody.scrollTop = chatBody.scrollHeight;
    }
});
