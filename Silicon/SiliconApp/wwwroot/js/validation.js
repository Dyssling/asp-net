const nameVal = (e) => {
    if (e.target.length < 2) {
        return false;
    }

    return true;
};

const checkedVal = (e) => {
    if (!e.target.checked) {
        return false;
    }

    return true;
};

const emailVal = (e) => {
    const regex = /^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$/;

    if (!regex.test(e.target.value)) {
        return false;
    }

    return true;
}

const passwordVal = (e) => {
    const regex = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/;

    if (!regex.test(e.target.value)) {
        return false;
    }

    return true;
}

const compareVal = (e) => {
    if (e.target.value !== document.getElementsByName(e.target.dataset.valEqualtoOther.replace("*", "Form"))[0].value) {
        return false;
    }

    return true;
};