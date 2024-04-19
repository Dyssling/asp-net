const joinCourse = (id) => {
    fetch(`/singlecourse/joincourse?id=${id}`)
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html");

            document.getElementById("card").innerHTML = dom.getElementById("card").innerHTML;
        });
}

const leaveCourse = (id) => {
    fetch(`/singlecourse/leavecourse?id=${id}`)
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html");

            document.getElementById("card").innerHTML = dom.getElementById("card").innerHTML;
        });
}
