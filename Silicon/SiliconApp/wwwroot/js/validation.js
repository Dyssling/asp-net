const basicInfoSubmit = () => {
    document.getElementById("BasicInfoForm_BasicInfoFormValue").value = "1";
}

const addressSubmit = () => {
    document.getElementById("AddressForm_AddressFormValue").value = "1";
}

const passwordSubmit = () => {
    document.getElementById("PasswordForm_PasswordFormValue").value = "1";
}

const deleteAccountSubmit = () => {
    document.getElementById("DeleteAccountForm_DeleteAccountFormValue").value = "1";
}

const requiredVal = (e) => {
    if (e.target.value.length == 0) {
        return false;
    }

    return true;
};

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
        if (e.target.value !== document.getElementsByName(e.target.dataset.valEqualtoOther.replace("*", e.target.name.substring(0, e.target.name.indexOf('.'))))[0].value) { //Värdet jämförs med värdet i fältet vars namn är lika med DETTA fältets data-val-equal-to-other värde. Stjärn-karaktären ersätts med samma början på namnet innan punkten på DETTA fältet.
            return false;
        }

        return true;
    }

    if (!regex.test(e.target.value)) {
        return false;
    }

    return true;
}

const phoneVal = (e) => {
    const regex = /^[0-9*#+ .()-]{7,}$/;

    if (!regex.test(e.target.value) && e.target.value.length > 0) {
        return false;
    }

    return true;
}

const requiredError = (e, result) => {
    let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`);

    if (result) {
        span.classList.remove("field-validation-error");
        span.classList.add("field-validation-valid");
        span.innerHTML = "";
    }

    else if (!result) {
        span.classList.add("field-validation-error");
        span.classList.remove("field-validation-valid");
        span.innerHTML = e.target.dataset.valRequired;
    }
};

const lengthError = (e, result) => {
    let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`);

    if (result || e.target.value.length == 0) { //Om man inte har skrivit något i fältet så får man inget felmeddelande.
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

    if (result || e.target.value.length == 0) {
        span.classList.remove("field-validation-error");
        span.classList.add("field-validation-valid");
        span.innerHTML = "";
    }

    else if (!result && e.target.value.length > 0){
        span.classList.add("field-validation-error");
        span.classList.remove("field-validation-valid");

        if (e.target.dataset.valEqualtoOther !== undefined) { //Om detta är en confirm password grej, så skriver den ut det meddelandet.
            span.innerHTML = e.target.dataset.valEqualto;
        }

        else {
            span.innerHTML = e.target.dataset.valRegex; //Annars skriver den ut regex meddelandet som vanligt.
        }
    }
};

const initialValidateInput = (e) => { //Denna funktionen används som event listener först när formuläret laddas. Det är alltså en blur först, sen byts det till keyup.

    switch (e.target.type) {
        case "text":
            if (e.target.name.includes("Phone")) {
                regexError(e, phoneVal(e));
            }

            else {
                lengthError(e, lengthVal(e));
            }
            break;

        case "email":
            regexError(e, emailVal(e));
            break;

        case "password":
            if (e.target.dataset.valRegexPattern !== undefined || e.target.dataset.valEqualtoOther !== undefined) {
                regexError(e, passwordVal(e));
            }

            else {
                requiredError(e, requiredVal(e)); //Om det inte finns en regex property så innebär det att man bara ska validera att lösenordet inte är tomt.
            }
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
            if (e.target.name.includes("Phone")) {
                regexError(e, phoneVal(e));
            }

            else {
                lengthError(e, lengthVal(e));
            }
            break;

        case "email":
            regexError(e, emailVal(e));
            break;

        case "password":
            if (e.target.dataset.valRegexPattern !== undefined || e.target.dataset.valEqualtoOther !== undefined) {
                regexError(e, passwordVal(e));
            }

            else {
                requiredError(e, requiredVal(e)); //Om det inte finns en regex property så innebär det att man bara ska validera att lösenordet inte är tomt.
            }
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