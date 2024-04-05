const lengthVal = (e) => {
    if (e.target.value.length < 2) {
        return false;
    }

    return true;
};

const emailVal = (e) => {
    const regex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,})+$/;

    if (!regex.test(e.target.value)) {
        return false;
    }

    return true;
}

const passwordVal = (e) => {
    const regex = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/;

    if (e.target.dataset.valEqualtoOther !== undefined) {
        if (e.target.value !== document.getElementsByName(e.target.dataset.valEqualtoOther.replace("*", "Form"))[0].value) {
            return false;
        }

        return true;
    }

    if (!regex.test(e.target.value)) {
        return false;
    }

    return true;
}

const lengthError = (e, result) => {
    let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`);

    if (result) {
        span.classList.remove("field-validation-error");
        span.classList.add("field-validation-valid");
        span.innerHTML = "";
    }

    else if (!result && e.target.value.length > 0) { //Den validerar endast om man har skrivit något i fältet.
        span.classList.add("field-validation-error");
        span.classList.remove("field-validation-valid");
        span.innerHTML = e.target.dataset.valMinlength;
    }
};

const regexError = (e, result) => {
    let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`);

    if (result) {
        span.classList.remove("field-validation-error");
        span.classList.add("field-validation-valid");
        span.innerHTML = "";
    }

    else if (!result && e.target.value.length > 0){
        span.classList.add("field-validation-error");
        span.classList.remove("field-validation-valid");

        if (e.target.dataset.valEqualtoOther !== undefined) {
            span.innerHTML = e.target.dataset.valEqualto;
        }

        else {
            span.innerHTML = e.target.dataset.valRegex;
        }
    }
};

const initialValidateInput = (e) => { //Denna funktionen används som event listener först när formuläret laddas. Det är alltså en blur först, sen byts det till keyup.

    switch (e.target.type) {
        case "text":
            lengthError(e, lengthVal(e));
            break;

        case "email":
            regexError(e, emailVal(e));
            break;

        case "password":
            regexError(e, passwordVal(e));
            break;
    }

    if (e.target.value.length > 0) {
        e.target.removeEventListener("blur", e => initialValidateInput(e));
        e.target.addEventListener("keyup", e => validateInput(e));
    }
}

const validateInput = (e) => {

    switch (e.target.type) {
        case "text":
            lengthError(e, lengthVal(e));
            break;

        case "email":
            regexError(e, emailVal(e));
            break;

        case "password":
            regexError(e, passwordVal(e));
            break;
    }
}

let inputs = document.querySelectorAll("input");

inputs.forEach(input => {

    if (input.dataset.val === "true") {

        if (document.querySelector(`[data-valmsg-for="${input.name}"]`).innerHTML.length == 0) {
            input.addEventListener("blur", e => initialValidateInput(e));
        }

        else {
            input.addEventListener("keyup", e => validateInput(e)); //Om det redan står något i felmeddelandet när formuläret laddas in, så går den direkt till den andra event listenern.
        }
    }
});