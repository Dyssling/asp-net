const removeOneCourse = (id) => {
    fetch(`/account/removeonecourse?id=${id}`)
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html");

            document.getElementById("saved-courses").innerHTML = dom.getElementById("saved-courses").innerHTML;
        });
}

const removeAllCourses = () => {
    fetch(`/account/removeallcourses`)
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html");

            document.getElementById("saved-courses").innerHTML = dom.getElementById("saved-courses").innerHTML;
        });
}