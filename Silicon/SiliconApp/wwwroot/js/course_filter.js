const updateFilter = () => {
    const categoryId = document.getElementById("category-selection");

    fetch(`/courses/index?categoryId=${categoryId.value}`)
        .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html");

            document.getElementById("courses-grid").innerHTML = dom.getElementById("courses-grid").innerHTML;
        });
}