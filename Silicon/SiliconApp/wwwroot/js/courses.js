const saveCourseWithFilters = (id) => {
    const categoryId = document.getElementById("category-selection");
    const search = document.getElementById("search-courses");

    fetch(`/courses/index?categoryId=${categoryId.value}&search=${search.value}&currentPage=${currentPage}`)
        .then(response => response.text()) //Responsen konverteras till text format (och detta är ett promise, vilket gör att vi måste göra ännu en "then" för att manipulera den ytterligare)
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html"); //Texten som vi fick ut från responsen omvandlas till HTML-format med hjälp av en DOMParser

            document.getElementById("courses-grid").innerHTML = dom.getElementById("courses-grid").innerHTML; //Innehållet på sidan inuti courses-grid sektionen byts ut mot det som vi nu har fått ut
            document.getElementById("pagnation").innerHTML = dom.getElementById("pagnation").innerHTML;
        });
}